namespace DIPS.Mobile.UI.API.Camera.BarcodeScanning;

public class BarcodeScanningSettings
{
    /// <summary>
    /// Whether to display a focused scan rectangle overlay on the camera preview.
    /// When enabled, a rounded-corner rectangle is drawn on the camera feed to visually indicate the scan area.
    /// On iOS, the rectangle corresponds to <c>AVCaptureMetadataOutput.RectOfInterest</c>.
    /// On Android, barcode results outside the rectangle are filtered out.
    /// </summary>
    public bool ShowScanRectangle { get; set; }

    /// <summary>
    /// Width of the scan rectangle as a fraction of the preview area width (0.0 to 1.0).
    /// </summary>
    public float ScanRectangleWidthFraction { get; set; } = 0.8f;

    /// <summary>
    /// Height of the scan rectangle as a fraction of the preview area height (0.0 to 1.0).
    /// </summary>
    public float ScanRectangleHeightFraction { get; set; } = 0.3f;
}