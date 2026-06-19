using DIPS.Mobile.UI.Internal.Logging;

namespace DIPS.Mobile.UI.API.Camera.BarcodeScanning;

/// <summary>
/// Overlay-based barcode confirmation used with <see cref="ScanRectangleBarcodeScanStrategy"/>.
/// Animates corner brackets to the detected barcode; confirms when the forming animation completes.
/// Cancels if the barcode is lost (no detection for <see cref="BarcodeLostTimeout"/> ms).
/// </summary>
internal sealed class OverlayBarcodeConfirmationHandler : IBarcodeConfirmationHandler
{
    private const int BarcodeLostTimeout = 650;

    private readonly BarcodeScanRectangleOverlay m_overlay;
    private readonly Func<int> m_getCurrentScanRunId;
    private readonly Action m_reportMostDetected;
    private readonly Action m_onBarcodeLost;

    private Timer? m_barcodeLostTimer;
    private bool m_isTrackingBarcode;
    private bool m_bracketsArrived;
    private bool m_isForming;

    public OverlayBarcodeConfirmationHandler(
        BarcodeScanRectangleOverlay overlay,
        Func<int> getCurrentScanRunId,
        Action reportMostDetected,
        Action onBarcodeLost)
    {
        m_overlay = overlay;
        m_getCurrentScanRunId = getCurrentScanRunId;
        m_reportMostDetected = reportMostDetected;
        m_onBarcodeLost = onBarcodeLost;
    }

    public void OnBarcodeDetected(RectF? overlayBounds)
    {
        if (overlayBounds is null)
            return;

        ResetBarcodeLostTimer();

        if (!m_isTrackingBarcode)
        {
            m_isTrackingBarcode = true;
            m_bracketsArrived = false;
            m_isForming = false;
            DUILogService.LogDebug<BarcodeScanner>("Barcode detected, animating brackets...");

            MainThread.BeginInvokeOnMainThread(() =>
            {
                m_overlay.SetBarcodeDetected();
                m_overlay.AnimateBracketsToBarcode(overlayBounds.Value, onArrived: () =>
                {
                    m_bracketsArrived = true;

                    if (!m_isTrackingBarcode)
                        return;

                    m_isForming = true;
                    DUILogService.LogDebug<BarcodeScanner>("Brackets arrived, forming rectangle...");
                    m_overlay.StartFormingAnimation(onFormed: () =>
                    {
                        if (!m_isTrackingBarcode)
                            return;

                        DUILogService.LogDebug<BarcodeScanner>("Rectangle formed — barcode confirmed.");
                        m_reportMostDetected();
                    });
                });
            });
        }
        else if (!m_bracketsArrived || m_isForming)
        {
            MainThread.BeginInvokeOnMainThread(() =>
                m_overlay.UpdateBracketTarget(overlayBounds.Value));
        }
    }

    public void ResetTrackingState()
    {
        m_isTrackingBarcode = false;
        m_bracketsArrived = false;
        m_isForming = false;
    }

    public void Reset()
    {
        DisposeBarcodeLostTimer();
        ResetTrackingState();
    }

    public void Dispose() => Reset();

    private void ResetBarcodeLostTimer()
    {
        var scanRunId = m_getCurrentScanRunId();
        if (m_barcodeLostTimer is not null)
        {
            m_barcodeLostTimer.Change(BarcodeLostTimeout, Timeout.Infinite);
        }
        else
        {
            m_barcodeLostTimer = new Timer(_ => HandleBarcodeLost(scanRunId),
                null, BarcodeLostTimeout, Timeout.Infinite);
        }
    }

    private void HandleBarcodeLost(int scanRunId)
    {
        if (!MainThread.IsMainThread)
        {
            MainThread.BeginInvokeOnMainThread(() => HandleBarcodeLost(scanRunId));
            return;
        }

        if (scanRunId != m_getCurrentScanRunId())
            return;

        DUILogService.LogDebug<BarcodeScanner>("Barcode lost — cancelling.");
        DisposeBarcodeLostTimer();
        ResetTrackingState();
        m_onBarcodeLost();
    }

    private void DisposeBarcodeLostTimer()
    {
        m_barcodeLostTimer?.Change(Timeout.Infinite, Timeout.Infinite);
        m_barcodeLostTimer?.Dispose();
        m_barcodeLostTimer = null;
    }
}
