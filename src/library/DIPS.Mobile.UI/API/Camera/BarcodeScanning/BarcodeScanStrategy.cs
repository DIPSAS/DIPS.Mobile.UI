namespace DIPS.Mobile.UI.API.Camera.BarcodeScanning;

/// <summary>
/// Defines how the barcode scanner confirms a detected barcode.
/// Use <see cref="TimerBarcodeScanStrategy"/> for timer-based confirmation without a visible overlay,
/// or <see cref="ScanRectangleBarcodeScanStrategy"/> for animated bracket overlay confirmation.
/// </summary>
public abstract class BarcodeScanStrategy
{
}

/// <summary>
/// Timer-based barcode confirmation without a visible scan rectangle.
/// After the first barcode is detected, the scanner collects observations for <see cref="DetectionTime"/>
/// and then picks the most-detected value.
/// </summary>
public class TimerBarcodeScanStrategy : BarcodeScanStrategy
{
    /// <summary>
    /// Gets or sets how long the scanner collects observations after the first barcode is detected
    /// before picking the most-detected value.
    /// </summary>
    /// <remarks>A longer time can improve precision for low quality barcodes, but makes scanning feel slower.</remarks>
    public TimeSpan DetectionTime { get; set; } = TimeSpan.FromMilliseconds(500);
}

/// <summary>
/// Animated scan rectangle overlay confirmation.
/// Displays a visible scan rectangle that filters barcode detections to a focused area.
/// When a barcode is detected, corner brackets animate to the barcode and form a rectangle
/// over <see cref="FormingDuration"/>, during which the scanner keeps collecting observations.
/// </summary>
public class ScanRectangleBarcodeScanStrategy : BarcodeScanStrategy
{
    /// <summary>
    /// Gets or sets the scan rectangle width as a fraction of the preview area width.
    /// </summary>
    public float WidthFraction { get; set; } = 0.8f;

    /// <summary>
    /// Gets or sets the scan rectangle height as a fraction of the preview area height.
    /// </summary>
    public float HeightFraction { get; set; } = 0.3f;

    /// <summary>
    /// Gets or sets how long the brackets take to travel from the scan rectangle to the detected barcode.
    /// </summary>
    public TimeSpan BracketsTravelDuration { get; set; } = TimeSpan.FromMilliseconds(400);

    /// <summary>
    /// Gets or sets how long the forming animation takes after brackets arrive at the barcode.
    /// This is the observation window during which the scanner keeps collecting detections before accepting.
    /// </summary>
    public TimeSpan FormingDuration { get; set; } = TimeSpan.FromMilliseconds(1000);

}
