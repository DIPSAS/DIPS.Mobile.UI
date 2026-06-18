namespace DIPS.Mobile.UI.API.Camera.BarcodeScanning;

/// <summary>
/// Defines automatic barcode scanner zoom-tip behavior.
/// </summary>
public class BarcodeScannerHintOptions
{
    /// <summary>
    /// Gets or sets a value indicating whether the scanner should automatically show <see cref="HintText"/> using the camera zoom tip when scanning takes longer than <see cref="Delay"/>.
    /// </summary>
    /// <remarks>The scanner attempts the automatic hint once per scanner session and does not show it after people have used camera zoom.</remarks>
    public bool ShowAutomaticHint { get; set; }

    /// <summary>
    /// Gets or sets the hint text shown by the camera zoom tip when <see cref="ShowAutomaticHint"/> is enabled.
    /// </summary>
    public string? HintText { get; set; }

    /// <summary>
    /// Gets or sets how long the scanner waits before showing <see cref="HintText"/>.
    /// </summary>
    public TimeSpan Delay { get; set; } = TimeSpan.FromSeconds(15);

    /// <summary>
    /// Gets or sets an optional callback that decides whether the automatic hint can be shown.
    /// </summary>
    /// <remarks>The scanner invokes this callback on the main thread.</remarks>
    public Func<Task<bool>>? CanShowHintAsync { get; set; }

    /// <summary>
    /// Gets or sets an optional callback invoked after the automatic hint has been shown.
    /// </summary>
    /// <remarks>The scanner invokes this callback on the main thread.</remarks>
    public Func<Task>? OnHintShownAsync { get; set; }
}