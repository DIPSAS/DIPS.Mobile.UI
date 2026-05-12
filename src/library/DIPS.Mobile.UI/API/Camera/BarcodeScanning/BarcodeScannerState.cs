namespace DIPS.Mobile.UI.API.Camera.BarcodeScanning;

/// <summary>
/// Describes the current barcode scanner processing state.
/// </summary>
public enum BarcodeScannerState
{
    /// <summary>
    /// The scanner has not started yet.
    /// </summary>
    Idle,

    /// <summary>
    /// The scanner is accepting barcode detections.
    /// </summary>
    Scanning,

    /// <summary>
    /// The scanner is temporarily ignoring barcode detections.
    /// </summary>
    Paused,

    /// <summary>
    /// The scanner is waiting for consumer validation.
    /// </summary>
    Validating,

    /// <summary>
    /// The scanner is playing the accepted barcode animation.
    /// </summary>
    SuccessAnimating,

    /// <summary>
    /// The scanner is playing the rejected barcode animation.
    /// </summary>
    FailureAnimating,

    /// <summary>
    /// The scanner has reached the required scan count.
    /// </summary>
    Completed
}