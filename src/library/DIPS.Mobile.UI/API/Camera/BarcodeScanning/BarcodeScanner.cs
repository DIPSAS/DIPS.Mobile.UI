using DIPS.Mobile.UI.API.Camera.Permissions;
using DIPS.Mobile.UI.API.Camera.Preview;
using DIPS.Mobile.UI.API.Camera.Shared;
using DIPS.Mobile.UI.Internal.Logging;

namespace DIPS.Mobile.UI.API.Camera.BarcodeScanning;

public partial class BarcodeScanner : ICameraUseCase
{
    private Timer? m_barCodesFoundTimer;
    private Timer? m_barcodeLostTimer;
    private List<BarcodeObservation> m_barcodeObservations = new();
    private DidFindBarcodeCallback? m_barCodeCallback;
    private CameraFailed? m_cameraFailedDelegate;
    private BarcodeScanRectangleOverlay? m_scanRectangleOverlay;
    private bool m_isTrackingBarcode;
    private bool m_bracketsArrived;
    private bool m_isForming;
    private bool m_isCoolingDown;
    private Timer? m_cooldownTimer;
    private HashSet<Barcode> m_confirmedBarcodes = new();
    
    /// <summary>
    /// How long (ms) without a barcode detection before we consider the barcode lost
    /// and cancel the bracket animation.
    /// </summary>
    private const int BarcodeLostTimeout = 250;
    
    /// <summary>
    /// How long (ms) after a successful scan before the scanner accepts new detections.
    /// Gives the overlay time to reset and avoids immediately re-scanning the same barcode.
    /// </summary>
    private const int CooldownTimeout = 500;

    private void Log(string message)
    {
        DUILogService.LogDebug<BarcodeScanner>(message);
    }

    public async Task Start(CameraPreview cameraPreview, DidFindBarcodeCallback didFindBarcodeCallback,CameraFailed cameraFailedDelegate, Action<BarcodeScanningSettings>? configure = null)
    {
        var barcodeScanningSettings = new BarcodeScanningSettings();
        configure?.Invoke(barcodeScanningSettings);

        m_cameraPreview = cameraPreview;
        m_cameraPreview.AddUseCase(this);
        m_barCodeCallback = didFindBarcodeCallback;
        m_cameraFailedDelegate = cameraFailedDelegate;
        if (await CameraPermissions.CanUseCamera())
        {
            Log("Permitted to use camera");
            await m_cameraPreview.HasLoaded();
            ConstructOverlayViews(barcodeScanningSettings);
            await PlatformStart(barcodeScanningSettings, m_cameraFailedDelegate);
            
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

    private void ConstructOverlayViews(BarcodeScanningSettings settings)
    {
        if (settings.ShowScanRectangle)
        {
            m_scanRectangleOverlay = new BarcodeScanRectangleOverlay(
                settings.ScanRectangleWidthFraction,
                settings.ScanRectangleHeightFraction);
            
            m_cameraPreview?.AddViewToRoot(m_scanRectangleOverlay, usePreviewViewTranslation: true);
        }
    }

    /// <summary>
    /// Sets a tooltip view above the scan rectangle (e.g., instructional text).
    /// Must be called after <see cref="Start"/> when <see cref="BarcodeScanningSettings.ShowScanRectangle"/> is <c>true</c>.
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
    
    internal partial Task PlatformStart(BarcodeScanningSettings barcodeScanningSettings, CameraFailed cameraFailedDelegate);

    public void StopAndDispose()
    {
        try
        {
            PlatformStop();
            RemoveOverlayViews();
            m_cameraPreview = null!;
            m_barCodeCallback = null;
            StopAndDisposeTimerAndResults();
            DisposeCooldownTimer();
        }
        catch (Exception e)
        {
            Log(e.Message);
        }
    }

    internal partial Task PlatformStop();

    internal void InvokeBarcodeFound(Barcode barcode, RectF? overlayBounds = null)
    {
        if (m_confirmedBarcodes.Contains(barcode))
        {
            // Barcode already confirmed — keep the lost timer alive so it only
            // fires when the barcode actually leaves the camera view.
            ResetBarcodeLostTimer();
            return;
        }
        
        if (m_isCoolingDown)
            return;
        
        // Accumulate observations regardless of mode
        var barcodeObservation =
            m_barcodeObservations.FirstOrDefault(observation => Equals(observation.Barcode, barcode));
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

        if (m_scanRectangleOverlay is not null && overlayBounds is not null)
        {
            // Overlay mode: animation-driven confirmation
            InvokeBarcodeFoundWithOverlay(overlayBounds.Value);
        }
        else
        {
            // No overlay: use the existing timer-based approach
            InvokeBarcodeFoundWithTimer();
        }
    }

    /// <summary>
    /// Overlay mode: brackets animate to barcode, confirmation happens when animation completes.
    /// If barcode is lost (no detection for <see cref="BarcodeLostTimeout"/>ms), cancel.
    /// </summary>
    private void InvokeBarcodeFoundWithOverlay(RectF overlayBounds)
    {
        // Reset "lost" timer — barcode is still visible
        ResetBarcodeLostTimer();

        if (!m_isTrackingBarcode)
        {
            // First detection — start tracking
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
                        return; // Lost while animating

                    // Brackets arrived — start forming the rectangle (~2 seconds)
                    m_isForming = true;
                    Log("Brackets arrived, forming rectangle...");
                    m_scanRectangleOverlay?.StartFormingAnimation(onFormed: () =>
                    {
                        if (!m_isTrackingBarcode)
                            return; // Lost while forming
                        
                        Log("Rectangle formed — barcode confirmed.");
                        ReportMostDetectedBarcode();
                    });
                });
            });
        }
        else if (!m_bracketsArrived)
        {
            // Animation still in progress — update bracket target to track the barcode
            MainThread.BeginInvokeOnMainThread(() =>
                m_scanRectangleOverlay?.UpdateBracketTarget(overlayBounds));
        }
        else if (m_isForming)
        {
            // Forming in progress — keep tracking the barcode position
            MainThread.BeginInvokeOnMainThread(() =>
                m_scanRectangleOverlay?.UpdateBracketTarget(overlayBounds));
        }
    }

    private void ResetBarcodeLostTimer()
    {
        if (m_barcodeLostTimer is not null)
        {
            m_barcodeLostTimer.Change(BarcodeLostTimeout, Timeout.Infinite);
        }
        else
        {
            m_barcodeLostTimer = new Timer(_ => OnBarcodeLost(), null, BarcodeLostTimeout, Timeout.Infinite);
        }
    }

    private void OnBarcodeLost()
    {
        Log("Barcode lost — cancelling.");
        m_isTrackingBarcode = false;
        m_bracketsArrived = false;
        m_isForming = false;
        m_barcodeObservations = [];
        m_confirmedBarcodes.Clear();
        DisposeBarcodeLostTimer();
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
        var allBarCodesOrderedByDetections =
            m_barcodeObservations.OrderByDescending(observation => observation.Detections).ToList();
        var mostDetectedBarcodeObservation =
            allBarCodesOrderedByDetections.FirstOrDefault();

        if (mostDetectedBarcodeObservation == null)
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
            m_confirmedBarcodes.Add(obs.Barcode);
        }
        StopAndDisposeTimerAndResults(playSuccessAnimation: true);
        StartCooldown();
        MainThread.BeginInvokeOnMainThread(() =>
            m_barCodeCallback?.Invoke(new BarcodeScanResult(mostDetectedBarcodeObservation.Barcode,
                barCodeResults)));
    }

    /// <summary>
    /// Timer-based confirmation for when there is no scan rectangle overlay.
    /// </summary>
    private void InvokeBarcodeFoundWithTimer()
    {
        if (m_barCodesFoundTimer == null)
        {
            Log($"First bar code found, observing for {BarcodeDetectionTime}ms.");
            m_barCodesFoundTimer = new Timer(_ => ReportMostDetectedBarcode(),
                null, (long)BarcodeDetectionTime, Timeout.Infinite);
        }
    }

    private void StartCooldown()
    {
        m_isCoolingDown = true;
        m_cooldownTimer?.Dispose();
        m_cooldownTimer = new Timer(_ =>
        {
            m_isCoolingDown = false;
            m_cooldownTimer?.Dispose();
            m_cooldownTimer = null;
        }, null, CooldownTimeout, Timeout.Infinite);
    }

    private void DisposeCooldownTimer()
    {
        m_cooldownTimer?.Dispose();
        m_cooldownTimer = null;
        m_isCoolingDown = false;
    }

    private void StopAndDisposeTimerAndResults(bool playSuccessAnimation = false)
    {
        m_barCodesFoundTimer?.Change(Timeout.Infinite, Timeout.Infinite);
        m_barCodesFoundTimer = null;
        m_barcodeObservations = [];
        m_isTrackingBarcode = false;
        m_bracketsArrived = false;
        m_isForming = false;
        
        // Don't dispose the barcode lost timer here — keep it running so that
        // OnBarcodeLost fires when the barcode leaves the view, clearing confirmed barcodes.
        
        if (playSuccessAnimation && m_scanRectangleOverlay is not null)
        {
            MainThread.BeginInvokeOnMainThread(() => m_scanRectangleOverlay?.PlaySuccessAndReset());
        }
        else
        {
            MainThread.BeginInvokeOnMainThread(() => m_scanRectangleOverlay?.ResetBarcodeDetection());
        }
    }
}