using DIPS.Mobile.UI.API.Camera.Preview;

namespace DIPS.Mobile.UI.API.Camera.BarcodeScanning;

/// <summary>
/// The result from a barcode scan. The most detected barcode is returned from the barcode scanner detected the first barcode in the <see cref="CameraPreview"/> until <see cref="BarcodeScanner."/>
/// </summary>
public class BarcodeScanResult(Barcode barcode, List<BarcodeObservation> observations)
{
    /// <summary>
    /// The barcode from the scan.
    /// </summary>
    public Barcode Barcode { get; } = barcode;

    /// <summary>
    /// A list of barcodes with information of why it was/was not the <see cref="Barcode"/> returned to you.
    /// </summary>
    public List<BarcodeObservation> Observations { get; } = observations;

    /// <summary>
    /// Determines if there was more than one barcode observation during scanning.
    /// </summary>
    public bool HasMultipleObservations => Observations.Count > 1;
}