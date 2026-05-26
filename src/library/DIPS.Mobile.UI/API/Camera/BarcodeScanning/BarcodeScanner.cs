using DIPS.Mobile.UI.API.Camera.Permissions;
using DIPS.Mobile.UI.API.Camera.Preview;
using DIPS.Mobile.UI.API.Camera.Shared;
using DIPS.Mobile.UI.Internal.Logging;

namespace DIPS.Mobile.UI.API.Camera.BarcodeScanning;

public partial class BarcodeScanner : ICameraUseCase
{
    private const int CooldownTimeout = 500;

    // Session lifecycle
    private BarcodeScannerStartOptions m_currentStartOptions = new();
    private BarcodeScanSession? m_scanSession;
    private CancellationTokenSource? m_sessionCts;
    private int m_scanRunId;
    private bool m_isDisposed;
    private bool m_isPlatformStarted;

    // Detection pipeline & cooldown
    private readonly BarcodeDetectionAggregator m_detectionAggregator = new();
    private readonly HashSet<string> m_confirmedBarcodeValues = new(StringComparer.Ordinal);
    private bool m_isCoolingDown;
    private Timer? m_cooldownTimer;

    // Confirmation handler & overlay
    private IBarcodeConfirmationHandler? m_confirmationHandler;
    private BarcodeScanRectangleOverlay? m_scanRectangleOverlay;

    internal int CurrentScanRunId => Volatile.Read(ref m_scanRunId);

    /// <summary>
    /// Returns true when the scanner session is active and ready to accept barcode detections.
    /// Consolidates the disposed and session-state checks used across the detection pipeline.
    /// </summary>
    private bool IsSessionActive => !m_isDisposed && m_scanSession?.CanProcessBarcode == true;

    /// <summary>
    /// Returns true when the scanner session can be resumed (not disposed, session exists, not completed).
    /// </summary>
    private bool CanResumeSession => !m_isDisposed && m_scanSession is not null &&
                                     m_scanSession.State != BarcodeScannerState.Completed;

    private void Log(string message)
    {
        DUILogService.LogDebug<BarcodeScanner>(message);
    }

    /// <summary>
    /// Starts barcode scanning using the provided <paramref name="startOptions"/> instance.
    /// </summary>
    public async Task Start(BarcodeScannerStartOptions startOptions)
    {
        var (cameraPreview, onCameraFailed) = GetRequiredStartDependencies(startOptions);

        m_isDisposed = false;
        CancelSession();
        m_sessionCts = new CancellationTokenSource();
        var ct = m_sessionCts.Token;
        BeginNewScanRun();
        await StopPlatformIfNeededAsync();
        RemoveOverlayViews();
        m_currentStartOptions = startOptions;
        m_cameraPreview = cameraPreview;
        m_cameraPreview.AddUseCase(this);
        m_confirmationHandler?.Dispose();
        m_confirmationHandler = null;
        ResetScanState();
        DisposeCooldownTimer();
        m_confirmedBarcodeValues.Clear();

        m_scanSession?.Dispose();
        m_scanSession = new BarcodeScanSession(m_currentStartOptions, StopPlatformIfNeededAsync);

        if (await CameraPermissions.CanUseCamera())
        {
            if (ct.IsCancellationRequested)
                return;

            Log("Permitted to use camera");
            await m_cameraPreview.HasLoaded();

            if (ct.IsCancellationRequested)
                return;

            ConstructOverlayViews(m_currentStartOptions);
            m_confirmationHandler = CreateConfirmationHandler();
            m_scanSession.AttachProgressCounter(m_cameraPreview);
            await PlatformStart(m_currentStartOptions, m_currentStartOptions.OnCameraFailed);

            // The overlay must match the PreviewView's translation so the dim and
            // scan rectangle cover the same area as the camera feed.
            if (m_scanRectangleOverlay != null)
            {
                m_scanRectangleOverlay.TranslationY = m_cameraPreview!.PreviewView.TranslationY;
            }

            if (ct.IsCancellationRequested)
            {
                await PlatformStop();
                return;
            }

            m_isPlatformStarted = true;
            m_scanSession.Start();

            if (m_cameraPreview?.CameraZoomView is not null)
            {
                m_cameraPreview.CameraZoomView.Opacity = 1;
            }
        }
        else
        {
            Log("Not permitted to use camera");
        }
    }

    private void ConstructOverlayViews(BarcodeScannerStartOptions startOptions)
    {
        if (startOptions.Strategy is ScanRectangleBarcodeScanStrategy scanRectangleStrategy)
        {
            m_scanRectangleOverlay = new BarcodeScanRectangleOverlay(
                scanRectangleStrategy.WidthFraction,
                scanRectangleStrategy.HeightFraction,
                scanRectangleStrategy.BracketsTravelDuration,
                scanRectangleStrategy.FormingDuration);

            m_cameraPreview?.AddViewToRoot(m_scanRectangleOverlay);
        }
    }

    /// <summary>
    /// Sets a tooltip view above the scan rectangle (e.g., instructional text).
    /// Must be called after <see cref="Start(BarcodeScannerStartOptions)"/> when using <see cref="ScanRectangleBarcodeScanStrategy"/>.
    /// </summary>
    public void SetTooltipView(View tooltipView)
    {
        m_scanRectangleOverlay?.SetTooltipView(tooltipView);
    }

    private void RemoveOverlayViews()
    {
        m_scanRectangleOverlay?.Cleanup();
        m_cameraPreview?.RemoveViewFromRoot(m_scanRectangleOverlay);
        m_scanRectangleOverlay = null;
    }

    internal partial Task PlatformStart(BarcodeScannerStartOptions startOptions, CameraFailed cameraFailedDelegate);

    public void StopAndDispose()
    {
        try
        {
            m_isDisposed = true;
            CancelSession();
            StopScanningCore(resetOverlay: false);
            RemoveOverlayViews();
            m_cameraPreview = null!;
            m_currentStartOptions = new BarcodeScannerStartOptions();
        }
        catch (Exception e)
        {
            Log(e.Message);
        }
    }

    /// <summary>
    /// Pauses barcode processing while keeping the current camera preview and scanner overlay attached.
    /// </summary>
    public void PauseScanning(bool resetOverlay = true)
    {
        try
        {
            EndCurrentScanRun();
            m_scanSession?.PauseScanning();
            ResetBarcodeConfirmationState(resetOverlay);
            DisposeCooldownTimer();
            m_confirmedBarcodeValues.Clear();
        }
        catch (Exception e)
        {
            Log(e.Message);
        }
    }

    /// <summary>
    /// Resumes barcode processing after <see cref="PauseScanning(bool)"/>.
    /// </summary>
    public void ResumeScanning()
    {
        try
        {
            if (!CanResumeSession)
                return;

            BeginNewScanRun();
            ResetScanState();
            m_confirmationHandler?.Reset();
            DisposeCooldownTimer();
            m_confirmedBarcodeValues.Clear();
            m_scanSession.ResumeScanning();
        }
        catch (Exception e)
        {
            Log(e.Message);
        }
    }

    /// <summary>
    /// Stops the current scanner session while keeping scanner overlay views attached to the camera preview.
    /// Call <see cref="Start(BarcodeScannerStartOptions)"/> to begin a new scanner session.
    /// </summary>
    public void StopScanning(bool resetOverlay = true)
    {
        try
        {
            StopScanningCore(resetOverlay);
        }
        catch (Exception e)
        {
            Log(e.Message);
        }
    }

    private void StopScanningCore(bool resetOverlay)
    {
        EndCurrentScanRun();
        StopPlatformIfNeeded();
        ResetBarcodeConfirmationState(resetOverlay);
        DisposeCooldownTimer();
        m_confirmationHandler?.Dispose();
        m_confirmationHandler = null;
        m_scanSession?.Dispose();
        m_scanSession = null;
    }

    internal partial Task PlatformStop();

    internal void InvokeBarcodeFound(Barcode barcode, RectF? overlayBounds = null, int scanRunId = 0)
    {
        if (!MainThread.IsMainThread)
        {
            MainThread.BeginInvokeOnMainThread(() => InvokeBarcodeFound(barcode, overlayBounds, scanRunId));
            return;
        }

        if (scanRunId is not 0 && scanRunId != CurrentScanRunId)
            return;

        if (!IsSessionActive)
            return;

        var barcodeKey = BarcodeDetectionAggregator.GetBarcodeKey(barcode);
        if (string.IsNullOrWhiteSpace(barcodeKey))
            return;

        if (m_confirmedBarcodeValues.Contains(barcodeKey))
        {
            m_confirmationHandler?.OnConfirmedBarcodeRedetected();
            return;
        }

        if (m_isCoolingDown)
            return;

        m_detectionAggregator.AddObservation(barcode);

        m_confirmationHandler?.OnBarcodeDetected(overlayBounds);
    }

    private void ReportMostDetectedBarcode()
    {
        ReportMostDetectedBarcode(CurrentScanRunId);
    }

    private void ReportMostDetectedBarcode(int scanRunId)
    {
        if (!MainThread.IsMainThread)
        {
            MainThread.BeginInvokeOnMainThread(() => ReportMostDetectedBarcode(scanRunId));
            return;
        }

        if (scanRunId != CurrentScanRunId)
            return;

        if (!IsSessionActive)
            return;

        var orderedObservations = m_detectionAggregator.GetObservationsOrderedByDetectionCount();
        if (orderedObservations is null)
            return;

        var mostDetectedBarcodeObservation = orderedObservations.First();

        if (m_scanSession?.TryBeginProcessing() != true)
            return;

        if (orderedObservations.Count > 1)
        {
            Log("-- Observations --:");
            foreach (var observation in orderedObservations)
            {
                Log($"{observation.Barcode}, detected {observation.Detections} times");
            }
        }

        Log($"The most detected bar code: {mostDetectedBarcodeObservation.Barcode}");
        foreach (var obs in orderedObservations)
        {
            m_confirmedBarcodeValues.Add(BarcodeDetectionAggregator.GetBarcodeKey(obs.Barcode));
        }

        ResetBarcodeConfirmationState(resetOverlay: false);

        var barcodeScanResult = new BarcodeScanResult(mostDetectedBarcodeObservation.Barcode,
            orderedObservations);
        var scanSession = m_scanSession;
        MainThread.BeginInvokeOnMainThread(async () =>
        {
            try
            {
                if (scanSession is null)
                    return;

                var shouldResumeScanning = await scanSession.ProcessBarcodeScanResultAsync(barcodeScanResult, m_scanRectangleOverlay);
                if (shouldResumeScanning)
                {
                    ResumeScanningAfterAnimation();
                }
            }
            catch (Exception exception)
            {
                DUILogService.LogError<BarcodeScanner>(exception.Message);
                ResumeScanningAfterAnimation();
            }
        });
    }

    private IBarcodeConfirmationHandler CreateConfirmationHandler()
    {
        return m_currentStartOptions.Strategy switch
        {
            ScanRectangleBarcodeScanStrategy when m_scanRectangleOverlay is not null =>
                new OverlayBarcodeConfirmationHandler(
                    m_scanRectangleOverlay,
                    () => CurrentScanRunId,
                    ReportMostDetectedBarcode,
                    OnBarcodeLostByHandler),
            TimerBarcodeScanStrategy timer =>
                new TimerBarcodeConfirmationHandler(
                    timer.DetectionTime,
                    () => CurrentScanRunId,
                    ReportMostDetectedBarcode),
            _ => throw new InvalidOperationException(
                $"Unsupported barcode scan strategy: {m_currentStartOptions.Strategy.GetType().Name}")
        };
    }

    private void OnBarcodeLostByHandler()
    {
        if (!IsSessionActive)
            return;

        m_confirmedBarcodeValues.Clear();
        ResetScanState();
        m_scanRectangleOverlay?.ResetBarcodeDetection();
    }

    private void StartCooldown()
    {
        m_isCoolingDown = true;
        m_cooldownTimer?.Dispose();
        var scanRunId = CurrentScanRunId;
        m_cooldownTimer = new Timer(_ => MainThread.BeginInvokeOnMainThread(() => FinishCooldown(scanRunId)),
            null, (int)Math.Max(m_currentStartOptions.DuplicateScanCooldown.TotalMilliseconds, CooldownTimeout), Timeout.Infinite);
    }

    private void FinishCooldown(int scanRunId)
    {
        if (scanRunId != CurrentScanRunId)
            return;

        m_isCoolingDown = false;
        if (m_scanRectangleOverlay is null)
        {
            m_confirmedBarcodeValues.Clear();
        }
        m_cooldownTimer?.Dispose();
        m_cooldownTimer = null;
    }

    private void DisposeCooldownTimer()
    {
        m_cooldownTimer?.Dispose();
        m_cooldownTimer = null;
        m_isCoolingDown = false;
    }

    private void ResetBarcodeConfirmationState(bool resetOverlay = true)
    {
        m_confirmationHandler?.Reset();
        ResetScanState();

        if (resetOverlay)
        {
            MainThread.BeginInvokeOnMainThread(() => m_scanRectangleOverlay?.ResetBarcodeDetection());
        }
    }

    private void ResetScanState()
    {
        m_detectionAggregator.Reset();
    }

    private void ResumeScanningAfterAnimation()
    {
        if (!CanResumeSession)
            return;

        StartCooldown();
        m_scanSession.ResumeScanning();
    }

    private int BeginNewScanRun() => Interlocked.Increment(ref m_scanRunId);

    private int EndCurrentScanRun() => Interlocked.Increment(ref m_scanRunId);

    private static (CameraPreview Preview, CameraFailed OnCameraFailed) GetRequiredStartDependencies(BarcodeScannerStartOptions startOptions)
    {
        ArgumentNullException.ThrowIfNull(startOptions);

        var cameraPreview = startOptions.Preview;
        ArgumentNullException.ThrowIfNull(cameraPreview, nameof(startOptions.Preview));

        var onCameraFailed = startOptions.OnCameraFailed;
        ArgumentNullException.ThrowIfNull(onCameraFailed, nameof(startOptions.OnCameraFailed));

        return (cameraPreview, onCameraFailed);
    }

    private void StopPlatformIfNeeded()
    {
        _ = StopPlatformIfNeededAsync();
    }

    private async Task StopPlatformIfNeededAsync()
    {
        if (!m_isPlatformStarted)
            return;

        m_isPlatformStarted = false;
        await PlatformStop();
    }

    private void CancelSession()
    {
        m_sessionCts?.Cancel();
        m_sessionCts?.Dispose();
        m_sessionCts = null;
    }
}
