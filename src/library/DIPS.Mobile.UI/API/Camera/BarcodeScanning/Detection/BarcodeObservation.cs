namespace DIPS.Mobile.UI.API.Camera.BarcodeScanning;

/// <summary>
/// A barcode observation gives you information such as of how many times a barcode was detected from the first barcode was detected to you got an result.
/// </summary>
public class BarcodeObservation(Barcode barcode, int detections)
{
    /// <summary>
    /// The barcode
    /// </summary>
    public Barcode Barcode { get; } = barcode;

    /// <summary>
    /// Number of times it was detected during the detection phase of the barcode scanner.
    /// </summary>
    public int Detections { get; } = detections;

    /// <summary>
    /// Determines if the barcode was the one with the most detections during detection phase.
    /// </summary>
    public bool HasMostDetections { get; set; }
}