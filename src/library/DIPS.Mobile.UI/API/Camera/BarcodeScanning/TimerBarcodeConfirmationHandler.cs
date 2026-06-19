using DIPS.Mobile.UI.Internal.Logging;

namespace DIPS.Mobile.UI.API.Camera.BarcodeScanning;

/// <summary>
/// Timer-based barcode confirmation used with <see cref="TimerBarcodeScanStrategy"/>.
/// After the first detection, waits for <see cref="TimerBarcodeScanStrategy.DetectionTime"/>
/// then reports the most-detected barcode.
/// </summary>
internal sealed class TimerBarcodeConfirmationHandler : IBarcodeConfirmationHandler
{
    private Timer? m_timer;
    private readonly TimeSpan m_detectionTime;
    private readonly Func<int> m_getCurrentScanRunId;
    private readonly Action<int> m_reportMostDetected;

    public TimerBarcodeConfirmationHandler(
        TimeSpan detectionTime,
        Func<int> getCurrentScanRunId,
        Action<int> reportMostDetected)
    {
        m_detectionTime = detectionTime;
        m_getCurrentScanRunId = getCurrentScanRunId;
        m_reportMostDetected = reportMostDetected;
    }

    public void OnBarcodeDetected(RectF? overlayBounds)
    {
        if (m_timer != null)
            return;

        var scanRunId = m_getCurrentScanRunId();
        DUILogService.LogDebug<BarcodeScanner>(
            $"First bar code found, observing for {m_detectionTime.TotalMilliseconds}ms.");
        m_timer = new Timer(_ => m_reportMostDetected(scanRunId),
            null, m_detectionTime, Timeout.InfiniteTimeSpan);
    }

    public void ResetTrackingState()
    {
        // No transient tracking state in timer mode.
    }

    public void Reset()
    {
        m_timer?.Change(Timeout.Infinite, Timeout.Infinite);
        m_timer?.Dispose();
        m_timer = null;
    }

    public void Dispose() => Reset();
}
