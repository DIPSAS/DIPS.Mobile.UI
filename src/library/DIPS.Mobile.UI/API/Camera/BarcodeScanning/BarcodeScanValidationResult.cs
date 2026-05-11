namespace DIPS.Mobile.UI.API.Camera.BarcodeScanning;

/// <summary>
/// Represents the consumer's validation decision for a detected barcode.
/// </summary>
public sealed class BarcodeScanValidationResult
{
    /// <summary>
    /// Gets a value indicating whether the barcode should be accepted by the scanner.
    /// </summary>
    public bool IsValid { get; init; }

    /// <summary>
    /// Gets an optional error message that can be shown when the barcode is rejected.
    /// </summary>
    public string? ErrorMessage { get; init; }

    /// <summary>
    /// Creates a validation result that accepts the barcode.
    /// </summary>
    public static BarcodeScanValidationResult Valid() => new() { IsValid = true };

    /// <summary>
    /// Creates a validation result that rejects the barcode.
    /// </summary>
    /// <param name="errorMessage">An optional error message explaining why the barcode was rejected.</param>
    public static BarcodeScanValidationResult Invalid(string? errorMessage = null) => new()
    {
        IsValid = false,
        ErrorMessage = errorMessage
    };
}