namespace DIPS.Mobile.UI.API.Camera.BarcodeScanning;

/// <summary>
/// Defines required scan count progress and completion behavior for a barcode scanner session.
/// </summary>
public class BarcodeScanCompletionOptions
{
    /// <summary>
    /// Gets or sets the optional number of accepted barcodes required to complete the scanning session.
    /// </summary>
    /// <remarks>Values less than or equal to zero hide the built-in progress counter.</remarks>
    public int? RequiredCount { get; set; }

    /// <summary>
    /// Gets or sets the number of accepted barcodes already completed before this scanner session starts.
    /// </summary>
    /// <remarks>The scanner owns and increments the active count during the session. Values above <see cref="RequiredCount"/> are clamped when the session starts.</remarks>
    public int InitialCount { get; set; }

    /// <summary>
    /// Gets or sets a callback invoked when the active scan count reaches <see cref="RequiredCount"/>.
    /// </summary>
    /// <remarks>Scanner analysis is stopped before this callback is invoked.</remarks>
    public Func<Task>? OnCompletedAsync { get; set; }
}
