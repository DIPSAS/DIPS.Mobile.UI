namespace DIPS.Mobile.UI.API.Camera.BarcodeScanning;

/// <summary>
/// Defines the visible scan rectangle used to focus barcode scanning.
/// </summary>
public class BarcodeScanRectangleOptions
{
    /// <summary>
    /// Gets or sets whether the scan rectangle should be displayed and used to filter barcode detections.
    /// </summary>
    public bool IsVisible { get; set; } = true;

    /// <summary>
    /// Gets or sets the scan rectangle width as a fraction of the preview area width.
    /// </summary>
    public float WidthFraction { get; set; } = 0.8f;

    /// <summary>
    /// Gets or sets the scan rectangle height as a fraction of the preview area height.
    /// </summary>
    public float HeightFraction { get; set; } = 0.3f;
}
