using DIPS.Mobile.UI.API.Camera.Preview;

namespace DIPS.Mobile.UI.API.Camera.BarcodeScanning;

/// <summary>
/// The result from a barcode scan. Contains the most detected barcode observed in the <see cref="CameraPreview"/> during a <see cref="BarcodeScanner"/> session.
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
    /// Gets the validation result used for this scan result.
    /// </summary>
    public BarcodeScanValidationResult ValidationResult { get; private set; } = BarcodeScanValidationResult.Valid();

    /// <summary>
    /// Determines if there was more than one barcode observation during scanning.
    /// </summary>
    public bool HasMultipleObservations => Observations.Count > 1;

    /// <summary>
    /// Tries to get the consumer state carried by the validation result.
    /// </summary>
    /// <typeparam name="TState">The expected state type.</typeparam>
    /// <param name="state">The typed state when present.</param>
    public bool TryGetValidationState<TState>(out TState? state) => ValidationResult.TryGetState(out state);

    internal void SetValidationResult(BarcodeScanValidationResult validationResult)
    {
        ValidationResult = validationResult;
    }
}