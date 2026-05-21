using DIPS.Mobile.UI.API.Camera.Permissions;
using DIPS.Mobile.UI.API.Camera.Preview;
using DIPS.Mobile.UI.API.Camera.Shared;
using DIPS.Mobile.UI.Internal.Logging;

namespace DIPS.Mobile.UI.API.Camera.BarcodeScanning;

public partial class BarcodeScanner : ICameraUseCase
{
    private const int BarcodeLostTimeout = 650;
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
    private Timer? m_barCodesFoundTimer;
    private readonly HashSet<string> m_confirmedBarcodeValues = new(StringComparer.Ordinal);
    private bool m_isCoolingDown;
    private Timer? m_cooldownTimer;

    // Overlay bracket animation
    private BarcodeScanRectangleOverlay? m_scanRectangleOverlay;
    private Timer? m_barcodeLostTimer;
    private bool m_isTrackingBarcode;
    private bool m_bracketsArrived;
    private bool m_isForming;

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
        ResetScanState();
        DisposeBarcodeLostTimer();
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
            m_scanSession.AttachProgressCounter(m_cameraPreview);
            await PlatformStart(m_currentStartOptions, m_currentStartOptions.OnCameraFailed);

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
            DisposeBarcodeLostTimer();
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
            DisposeBarcodeLostTimer();
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
        DisposeBarcodeLostTimer();
        DisposeCooldownTimer();
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
            ResetBarcodeLostTimer();
            return;
        }
        
        if (m_isCoolingDown)
            return;
        
        m_detectionAggregator.AddObservation(barcode);

        if (m_scanRectangleOverlay is not null && overlayBounds is not null)
        {
            InvokeBarcodeFoundWithOverlay(overlayBounds.Value);
        }
        else
        {
            InvokeBarcodeFoundWithTimer();
        }
    }

    /// <summary>
    /// Overlay mode: brackets animate to barcode, confirmation happens when animation completes.
    /// If barcode is lost (no detection for <see cref="BarcodeLostTimeout"/>ms), cancel.
    /// </summary>
    private void InvokeBarcodeFoundWithOverlay(RectF overlayBounds)
    {
        ResetBarcodeLostTimer();

        if (!m_isTrackingBarcode)
        {
            m_isTrackingBarcode = true;
            m_bracketsArrived = false;
            m_isForming = false;
            Log("Barcode detected, animating brackets...");

            MainThread.BeginInvokeOnMainThread(() =>
            {
                m_scanRectangleOverlay?.SetBarcodeDetected();
                m_scanRectangleOverlay?.AnimateBracketsToBarcode(overlayBounds, onArrived: () =>
                {
                    m_bracketsArrived = true;
                    
                    if (!m_isTrackingBarcode)
                        return;

                    m_isForming = true;
                    Log("Brackets arrived, forming rectangle...");
                    m_scanRectangleOverlay?.StartFormingAnimation(onFormed: () =>
                    {
                        if (!m_isTrackingBarcode)
                            return;
                        
                        Log("Rectangle formed — barcode confirmed.");
                        ReportMostDetectedBarcode();
                    });
                });
            });
        }
        else if (!m_bracketsArrived || m_isForming)
        {
            MainThread.BeginInvokeOnMainThread(() =>
                m_scanRectangleOverlay?.UpdateBracketTarget(overlayBounds));
        }
    }

    private void ResetBarcodeLostTimer()
    {
        var scanRunId = CurrentScanRunId;
        if (m_barcodeLostTimer is not null)
        {
            m_barcodeLostTimer.Change(BarcodeLostTimeout, Timeout.Infinite);
        }
        else
        {
            m_barcodeLostTimer = new Timer(_ => OnBarcodeLost(scanRunId), null, BarcodeLostTimeout, Timeout.Infinite);
        }
    }

    private void OnBarcodeLost(int scanRunId)
    {
        if (!MainThread.IsMainThread)
        {
            MainThread.BeginInvokeOnMainThread(() => OnBarcodeLost(scanRunId));
            return;
        }

        if (scanRunId != CurrentScanRunId)
            return;

        Log("Barcode lost — cancelling.");
        DisposeBarcodeLostTimer();

        if (!IsSessionActive)
            return;

        m_confirmedBarcodeValues.Clear();
        ResetScanState();
        MainThread.BeginInvokeOnMainThread(() => m_scanRectangleOverlay?.ResetBarcodeDetection());
    }

    private void DisposeBarcodeLostTimer()
    {
        m_barcodeLostTimer?.Change(Timeout.Infinite, Timeout.Infinite);
        m_barcodeLostTimer?.Dispose();
        m_barcodeLostTimer = null;
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

        var orderedObservations = m_detectionAggregator.ResolveOrderedObservations();
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
        DisposeBarcodeLostTimer();

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

    /// <summary>
    /// Timer-based confirmation for when there is no scan rectangle overlay.
    /// </summary>
    private void InvokeBarcodeFoundWithTimer()
    {
        if (m_barCodesFoundTimer == null)
        {
            var detectionTime = (m_currentStartOptions.Strategy as TimerBarcodeScanStrategy)?.DetectionTime 
                                ?? TimeSpan.FromMilliseconds(500);
            var scanRunId = CurrentScanRunId;
            Log($"First bar code found, observing for {detectionTime.TotalMilliseconds}ms.");
            m_barCodesFoundTimer = new Timer(_ => ReportMostDetectedBarcode(scanRunId),
                null, detectionTime, Timeout.InfiniteTimeSpan);
        }
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
        m_barCodesFoundTimer?.Change(Timeout.Infinite, Timeout.Infinite);
        m_barCodesFoundTimer?.Dispose();
        m_barCodesFoundTimer = null;
        ResetScanState();
        
        if (resetOverlay)
        {
            MainThread.BeginInvokeOnMainThread(() => m_scanRectangleOverlay?.ResetBarcodeDetection());
        }
    }

    private void ResetScanState()
    {
        m_detectionAggregator.Reset();
        m_isTrackingBarcode = false;
        m_bracketsArrived = false;
        m_isForming = false;
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