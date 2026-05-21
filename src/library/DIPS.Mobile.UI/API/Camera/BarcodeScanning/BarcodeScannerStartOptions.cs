using DIPS.Mobile.UI.API.Camera.Preview;

namespace DIPS.Mobile.UI.API.Camera.BarcodeScanning;

/// <summary>
/// Defines the complete contract for one barcode scanner session.
/// </summary>
public class BarcodeScannerStartOptions
{
    /// <summary>
    /// Gets or sets the camera preview that should host the scanner session.
    /// </summary>
    /// <remarks>This property is required when calling <see cref="BarcodeScanner.Start(BarcodeScannerStartOptions)"/> directly.</remarks>
    public CameraPreview? Preview { get; set; }

    /// <summary>
    /// Gets or sets the callback invoked when camera startup or analysis fails.
    /// </summary>
    /// <remarks>This property is required for barcode scanner sessions. The scanner throws if it is not set.</remarks>
    public CameraFailed? OnCameraFailed { get; set; }

    /// <summary>
    /// Gets or sets an async callback invoked after a barcode has been accepted.
    /// </summary>
    public Func<BarcodeScanResult, Task>? OnBarcodeAcceptedAsync { get; set; }

    /// <summary>
    /// Gets or sets a callback that validates a detected barcode before the scanner accepts it and plays the success animation.
    /// </summary>
    public Func<string, Task<BarcodeScanValidationResult>>? ValidateBarcodeAsync { get; set; }

    /// <summary>
    /// Gets or sets a callback invoked after a barcode has been rejected.
    /// </summary>
    public Func<BarcodeScanResult, BarcodeScanValidationResult, Task>? OnBarcodeRejectedAsync { get; set; }

    /// <summary>
    /// Gets or sets options for the visible scan rectangle.
    /// </summary>
    public BarcodeScanRectangleOptions? ScanRectangle { get; set; }

    /// <summary>
    /// Gets or sets options for required scan count progress and completion.
    /// </summary>
    public BarcodeScanCompletionOptions? Completion { get; set; }

    /// <summary>
    /// Gets or sets how long, in milliseconds, the scanner counts observations after the first barcode was detected.
    /// </summary>
    /// <remarks>A longer time can improve precision for low quality barcodes, but makes scanning feel slower.</remarks>
    public int BarcodeDetectionTime { get; set; } = 500;

    /// <summary>
    /// Gets or sets the cooldown applied after a scan has been accepted or rejected.
    /// </summary>
    public TimeSpan DuplicateScanCooldown { get; set; } = TimeSpan.FromMilliseconds(800);
}
