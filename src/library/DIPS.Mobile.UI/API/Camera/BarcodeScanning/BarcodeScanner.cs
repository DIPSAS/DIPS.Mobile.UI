using DIPS.Mobile.UI.API.Camera.Permissions;
using DIPS.Mobile.UI.API.Camera.Preview;
using DIPS.Mobile.UI.API.Camera.Shared;
using DIPS.Mobile.UI.Internal.Logging;

namespace DIPS.Mobile.UI.API.Camera.BarcodeScanning;

public partial class BarcodeScanner : ICameraUseCase
{
    private const int BarcodeLostTimeout = 650;
    private const int CooldownTimeout = 500;

    private Timer? m_barCodesFoundTimer;
    private Timer? m_barcodeLostTimer;
    private List<BarcodeObservation> m_barcodeObservations = new();
    private CameraFailed? m_cameraFailedDelegate;
    private BarcodeScanRectangleOverlay? m_scanRectangleOverlay;
    private bool m_isTrackingBarcode;
    private bool m_bracketsArrived;
    private bool m_isForming;
    private bool m_isCoolingDown;
    private bool m_isDisposed;
    private bool m_isPlatformStarted;
    private Timer? m_cooldownTimer;
    private readonly HashSet<string> m_confirmedBarcodeValues = new(StringComparer.Ordinal);
    private BarcodeScannerStartOptions m_currentStartOptions = new();
    private BarcodeScanSession? m_scanSession;
    private int m_scanRunId;

    internal int CurrentScanRunId => Volatile.Read(ref m_scanRunId);

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
        BeginNewScanRun();
        await StopPlatformIfNeededAsync();
        RemoveOverlayViews();
        m_currentStartOptions = startOptions;
        m_cameraPreview = cameraPreview;
        m_cameraPreview.AddUseCase(this);
        m_cameraFailedDelegate = onCameraFailed;
        ResetScanState();
        DisposeBarcodeLostTimer();
        DisposeCooldownTimer();
        m_confirmedBarcodeValues.Clear();

        m_scanSession?.Dispose();
        m_scanSession = new BarcodeScanSession(m_currentStartOptions, StopPlatformIfNeededAsync);

        if (await CameraPermissions.CanUseCamera())
        {
            if (m_isDisposed)
                return;

            Log("Permitted to use camera");
            await m_cameraPreview.HasLoaded();

            if (m_isDisposed)
                return;

            ConstructOverlayViews(m_currentStartOptions);
            m_scanSession.AttachProgressCounter(m_cameraPreview);
            await PlatformStart(m_currentStartOptions, m_cameraFailedDelegate);

            if (m_isDisposed)
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
        if (startOptions.ScanRectangle is { IsVisible: true } scanRectangleOptions)
        {
            m_scanRectangleOverlay = new BarcodeScanRectangleOverlay(
                scanRectangleOptions.WidthFraction,
                scanRectangleOptions.HeightFraction);
            
            m_cameraPreview?.AddViewToRoot(m_scanRectangleOverlay);
        }
    }

    /// <summary>
    /// Sets a tooltip view above the scan rectangle (e.g., instructional text).
    /// Must be called after <see cref="Start(BarcodeScannerStartOptions)"/> when <see cref="BarcodeScannerStartOptions.ScanRectangle"/> is visible.
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
            StopScanningCore(resetOverlay: false);
            RemoveOverlayViews();
            m_cameraPreview = null!;
            m_cameraFailedDelegate = null;
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
            if (m_isDisposed || m_scanSession is null || m_scanSession.State == BarcodeScannerState.Completed)
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

        if (m_isDisposed || m_scanSession?.CanProcessBarcode != true)
            return;

        var barcodeKey = GetBarcodeKey(barcode);
        if (string.IsNullOrWhiteSpace(barcodeKey))
            return;

        if (m_confirmedBarcodeValues.Contains(barcodeKey))
        {
            ResetBarcodeLostTimer();
            return;
        }
        
        if (m_isCoolingDown)
            return;
        
        AddBarcodeObservation(barcode);

        if (m_scanRectangleOverlay is not null && overlayBounds is not null)
        {
            InvokeBarcodeFoundWithOverlay(overlayBounds.Value);
        }
        else
        {
            InvokeBarcodeFoundWithTimer();
        }
    }

    private void AddBarcodeObservation(Barcode barcode)
    {
        var barcodeKey = GetBarcodeKey(barcode);
        var barcodeObservation =
            m_barcodeObservations.FirstOrDefault(observation => GetBarcodeKey(observation.Barcode) == barcodeKey);
        if (barcodeObservation == null)
        {
            m_barcodeObservations.Add(new BarcodeObservation(barcode, 1));
        }
        else
        {
            var numberOfDetections = barcodeObservation.Detections + 1;
            m_barcodeObservations.Remove(barcodeObservation);
            m_barcodeObservations.Add(new BarcodeObservation(barcode, numberOfDetections));
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

        if (m_scanSession?.CanProcessBarcode != true)
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

        if (m_isDisposed || m_scanSession?.CanProcessBarcode != true)
            return;

        var allBarCodesOrderedByDetections =
            m_barcodeObservations.OrderByDescending(observation => observation.Detections).ToList();
        var mostDetectedBarcodeObservation = allBarCodesOrderedByDetections.FirstOrDefault();

        if (mostDetectedBarcodeObservation == null)
            return;

        if (m_scanSession?.TryBeginProcessing() != true)
            return;

        mostDetectedBarcodeObservation.HasMostDetections = true;

        if (allBarCodesOrderedByDetections.Count > 1)
        {
            Log("-- Observations --:");
            foreach (var observation in allBarCodesOrderedByDetections)
            {
                Log($"{observation.Barcode}, detected {observation.Detections} times");
            }
        }

        Log($"The most detected bar code: {mostDetectedBarcodeObservation.Barcode}");
        var barCodeResults = allBarCodesOrderedByDetections.ToList();
        foreach (var obs in allBarCodesOrderedByDetections)
        {
            m_confirmedBarcodeValues.Add(GetBarcodeKey(obs.Barcode));
        }

        ResetBarcodeConfirmationState(resetOverlay: false);
        DisposeBarcodeLostTimer();

        var barcodeScanResult = new BarcodeScanResult(mostDetectedBarcodeObservation.Barcode,
            barCodeResults);
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
            var scanRunId = CurrentScanRunId;
            Log($"First bar code found, observing for {m_currentStartOptions.BarcodeDetectionTime}ms.");
            m_barCodesFoundTimer = new Timer(_ => ReportMostDetectedBarcode(scanRunId),
                null, m_currentStartOptions.BarcodeDetectionTime, Timeout.Infinite);
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
        m_barcodeObservations = [];
        m_isTrackingBarcode = false;
        m_bracketsArrived = false;
        m_isForming = false;
    }

    private void ResumeScanningAfterAnimation()
    {
        if (m_isDisposed || m_scanSession is null || m_scanSession.State == BarcodeScannerState.Completed)
            return;

        StartCooldown();
        m_scanSession.ResumeScanning();
    }

    private int BeginNewScanRun() => Interlocked.Increment(ref m_scanRunId);

    private int EndCurrentScanRun() => Interlocked.Increment(ref m_scanRunId);

    private static (CameraPreview Preview, CameraFailed OnCameraFailed) GetRequiredStartDependencies(BarcodeScannerStartOptions startOptions)
    {
        ArgumentNullException.ThrowIfNull(startOptions);

        var cameraPreview = startOptions.Preview ?? throw new ArgumentException($"{nameof(BarcodeScannerStartOptions.Preview)} must be set.", nameof(startOptions));
        var onCameraFailed = startOptions.OnCameraFailed ?? throw new ArgumentException($"{nameof(BarcodeScannerStartOptions.OnCameraFailed)} must be set.", nameof(startOptions));

        return (cameraPreview, onCameraFailed);
    }

    private static string GetBarcodeKey(Barcode barcode) => barcode.RawValue ?? string.Empty;

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
}