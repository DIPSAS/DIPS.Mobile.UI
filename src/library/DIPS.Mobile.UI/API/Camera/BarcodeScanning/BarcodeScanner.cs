using DIPS.Mobile.UI.API.Camera.Permissions;
using DIPS.Mobile.UI.API.Camera.Preview;
using DIPS.Mobile.UI.API.Camera.Shared;
using DIPS.Mobile.UI.Internal.Logging;

namespace DIPS.Mobile.UI.API.Camera.BarcodeScanning;

public partial class BarcodeScanner : ICameraUseCase
{
    // Session lifecycle
    private BarcodeScannerStartOptions m_currentStartOptions = new();
    private BarcodeScanSession? m_scanSession;
    private CancellationTokenSource? m_sessionCts;
    private Task m_platformStopTask = Task.CompletedTask;
    private int m_scanRunId;
    private bool m_isDisposed;
    private bool m_isPlatformStarted;

    // Detection pipeline
    private readonly BarcodeDetectionAggregator m_detectionAggregator = new();
    private CancellationTokenSource? m_automaticHintCts;
    private bool m_hasCompletedAutomaticHint;

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
        StopAutomaticHint();
        RemoveOverlayViews();
        m_currentStartOptions = startOptions;
        m_cameraPreview = cameraPreview;
        m_cameraPreview.AddUseCase(this);
        m_confirmationHandler?.Dispose();
        m_confirmationHandler = null;
        ResetScanState();
        m_hasCompletedAutomaticHint = false;

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
            await PlatformStart(m_currentStartOptions, onCameraFailed);

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
            RestartAutomaticHintTimer();

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

            // Insert after PreviewView (index 0) but before the toolbar containers
            // so that toolbars render on top of the overlay dim layer.
            m_cameraPreview?.AddViewToRoot(m_scanRectangleOverlay, index: 1);
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
            StopAutomaticHint();
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
            m_scanSession?.ResumeScanning();
            RestartAutomaticHintTimer();
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
        StopAutomaticHint();
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

        StopAutomaticHint();

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

        ResetScanState();
        m_scanRectangleOverlay?.ResetBarcodeDetection();
        RestartAutomaticHintTimer();
    }

    private void RestartAutomaticHintTimer()
    {
        StopAutomaticHint();

        if (!TryGetAutomaticHint(out var hintText, out var delay))
            return;

        var scanRunId = CurrentScanRunId;
        m_automaticHintCts = new CancellationTokenSource();
        _ = ShowAutomaticHintAfterDelayAsync(hintText, delay, scanRunId, m_automaticHintCts.Token);
    }

    private async Task ShowAutomaticHintAfterDelayAsync(string hintText, TimeSpan delay, int scanRunId, CancellationToken cancellationToken)
    {
        try
        {
            if (delay > TimeSpan.Zero)
            {
                await Task.Delay(delay, cancellationToken);
            }

            if (cancellationToken.IsCancellationRequested || scanRunId != CurrentScanRunId || !IsSessionActive)
                return;

            await MainThread.InvokeOnMainThreadAsync(() => TryShowAutomaticHintAsync(hintText, scanRunId, cancellationToken));
        }
        catch (TaskCanceledException)
        {
        }
        catch (Exception exception)
        {
            DUILogService.LogError<BarcodeScanner>($"Barcode scanner automatic hint failed: {exception.Message}");
        }
    }

    private bool TryGetAutomaticHint(out string hintText, out TimeSpan delay)
    {
        hintText = string.Empty;
        delay = TimeSpan.Zero;

        var hint = m_currentStartOptions.Hint;
        if (m_cameraPreview is null || m_hasCompletedAutomaticHint || !hint.ShowAutomaticHint || string.IsNullOrWhiteSpace(hint.HintText) || m_cameraPreview.HasZoomed)
            return false;

        hintText = hint.HintText;
        delay = hint.Delay < TimeSpan.Zero ? TimeSpan.Zero : hint.Delay;
        return true;
    }

    private async Task TryShowAutomaticHintAsync(string hintText, int scanRunId, CancellationToken cancellationToken)
    {
        if (cancellationToken.IsCancellationRequested || scanRunId != CurrentScanRunId || !IsSessionActive)
            return;

        if (m_hasCompletedAutomaticHint || m_cameraPreview?.HasZoomed is not false)
            return;

        m_hasCompletedAutomaticHint = true;

        if (!await CanShowAutomaticHintAsync())
            return;

        if (cancellationToken.IsCancellationRequested || scanRunId != CurrentScanRunId || !IsSessionActive || m_cameraPreview?.HasZoomed is not false)
            return;

        try
        {
            m_cameraPreview.ShowZoomSliderTip(hintText);
        }
        catch (Exception exception)
        {
            DUILogService.LogError<BarcodeScanner>($"Barcode scanner zoom tip failed: {exception.Message}");
            return;
        }

        await NotifyAutomaticHintShownAsync();
    }

    private async Task<bool> CanShowAutomaticHintAsync()
    {
        if (m_cameraPreview?.HasZoomed is not false)
            return false;

        try
        {
            return m_currentStartOptions.Hint.CanShowHintAsync is null || await m_currentStartOptions.Hint.CanShowHintAsync.Invoke();
        }
        catch (Exception exception)
        {
            DUILogService.LogError<BarcodeScanner>($"Barcode scanner hint visibility callback failed: {exception.Message}");
            return false;
        }
    }

    private async Task NotifyAutomaticHintShownAsync()
    {
        try
        {
            if (m_currentStartOptions.Hint.OnHintShownAsync is not null)
            {
                await m_currentStartOptions.Hint.OnHintShownAsync.Invoke();
            }
        }
        catch (Exception exception)
        {
            DUILogService.LogError<BarcodeScanner>($"Barcode scanner hint shown callback failed: {exception.Message}");
        }
    }

    private void StopAutomaticHint()
    {
        DisposeAutomaticHintTimer();
    }

    private void DisposeAutomaticHintTimer()
    {
        m_automaticHintCts?.Cancel();
        m_automaticHintCts?.Dispose();
        m_automaticHintCts = null;
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

        m_scanSession?.ResumeScanning();
        RestartAutomaticHintTimer();
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
        {
            await m_platformStopTask;
            return;
        }

        m_isPlatformStarted = false;
        m_platformStopTask = StopPlatformCoreAsync();
        await m_platformStopTask;
    }

    private async Task StopPlatformCoreAsync()
    {
        try
        {
            await PlatformStop();
        }
        catch (Exception e)
        {
            Log(e.Message);
        }
    }

    private void CancelSession()
    {
        m_sessionCts?.Cancel();
        m_sessionCts?.Dispose();
        m_sessionCts = null;
    }
}
