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
    /// Gets an optional consumer-defined reason code for rejected barcodes.
    /// </summary>
    public string? ReasonCode { get; init; }

    /// <summary>
    /// Gets optional consumer state associated with a valid barcode.
    /// </summary>
    public object? State { get; init; }

    /// <summary>
    /// Creates a validation result that accepts the barcode.
    /// </summary>
    public static BarcodeScanValidationResult Valid() => new() { IsValid = true };

    /// <summary>
    /// Creates a validation result that accepts the barcode and carries consumer state for later callbacks.
    /// </summary>
    /// <typeparam name="TState">The type of state associated with the accepted barcode.</typeparam>
    /// <param name="state">The state associated with the accepted barcode.</param>
    public static BarcodeScanValidationResult Valid<TState>(TState state) => new()
    {
        IsValid = true,
        State = state
    };

    /// <summary>
    /// Creates a validation result that rejects the barcode.
    /// </summary>
    /// <param name="errorMessage">An optional error message explaining why the barcode was rejected.</param>
    /// <param name="reasonCode">An optional consumer-defined reason code explaining why the barcode was rejected.</param>
    public static BarcodeScanValidationResult Invalid(string? errorMessage = null, string? reasonCode = null) => new()
    {
        IsValid = false,
        ErrorMessage = errorMessage,
        ReasonCode = reasonCode
    };

    /// <summary>
    /// Tries to get the consumer state carried by a valid validation result.
    /// </summary>
    /// <typeparam name="TState">The expected state type.</typeparam>
    /// <param name="state">The typed state when present.</param>
    public bool TryGetState<TState>(out TState? state)
    {
        if (State is TState typedState)
        {
            state = typedState;
            return true;
        }

        state = default;
        return false;
    }
}