namespace DIPS.Mobile.UI.API.Camera.BarcodeScanning;

/// <summary>
/// Encapsulates the barcode confirmation logic that varies by <see cref="BarcodeScanStrategy"/>.
/// <see cref="TimerBarcodeConfirmationHandler"/> confirms via a simple timer.
/// <see cref="OverlayBarcodeConfirmationHandler"/> confirms via animated bracket overlay.
/// </summary>
internal interface IBarcodeConfirmationHandler : IDisposable
{
    /// <summary>
    /// Called when a new (unconfirmed, non-cooldown) barcode observation has been recorded.
    /// </summary>
    void OnBarcodeDetected(RectF? overlayBounds);

    /// <summary>
    /// Called when an already-confirmed barcode is detected again.
    /// Keeps the overlay barcode-lost timer alive in overlay mode; no-op in timer mode.
    /// </summary>
    void OnConfirmedBarcodeRedetected();

    /// <summary>
    /// Resets transient tracking state (e.g. bracket animation flags) without disposing timers.
    /// </summary>
    void ResetTrackingState();

    /// <summary>
    /// Fully resets all state — disposes timers and resets tracking.
    /// </summary>
    void Reset();
}
