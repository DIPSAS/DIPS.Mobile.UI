namespace DIPS.Mobile.UI.API.Camera.BarcodeScanning;

public partial class BarcodeScanner
{
    /// <summary>
    /// Determines how long (milliseconds) the <see cref="BarcodeScanner"/> will count barcodes from the first barcode was detected.
    /// </summary>
    /// <remarks>This has effect on the result, a long time makes it feel slow for the user, but will force the barcode scanner to spend more time on precision, especially for barcodes with low quality.</remarks>
    public int BarcodeDetectionTime { get; set; } = 500;
}