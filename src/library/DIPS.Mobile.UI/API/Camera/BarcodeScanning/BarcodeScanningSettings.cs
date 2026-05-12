namespace DIPS.Mobile.UI.API.Camera.BarcodeScanning;

public class BarcodeScanningSettings
{
    private int? m_requiredScanCount;
    private int m_currentScanCount;

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

    /// <summary>
    /// Determines how long (milliseconds) the <see cref="BarcodeScanner"/> will count barcodes from the first barcode was detected.
    /// </summary>
    /// <remarks>This has effect on the result, a long time makes it feel slow for the user, but will force the barcode scanner to spend more time on precision, especially for barcodes with low quality.</remarks>
    public int BarcodeDetectionTime { get; set; } = 500;

    /// <summary>
    /// Gets or sets the optional number of accepted barcodes required to complete the scanning session.
    /// </summary>
    /// <remarks>Values less than or equal to zero hide the built-in progress counter.</remarks>
    public int? RequiredScanCount
    {
        get => m_requiredScanCount;
        set
        {
            m_requiredScanCount = value;
            ProgressChanged?.Invoke();
        }
    }

    /// <summary>
    /// Gets or sets the current number of accepted barcodes.
    /// </summary>
    /// <remarks>The scanner increments this value after a barcode is accepted. Consumers can set it to reset or restore progress.</remarks>
    public int CurrentScanCount
    {
        get => m_currentScanCount;
        set
        {
            m_currentScanCount = Math.Max(0, value);
            ProgressChanged?.Invoke();
        }
    }

    /// <summary>
    /// Gets or sets a callback invoked after a barcode has been accepted.
    /// </summary>
    public DidFindBarcodeCallback? OnValidBarcodeScanned { get; set; }

    /// <summary>
    /// Gets or sets an async callback invoked after a barcode has been accepted.
    /// </summary>
    public Func<BarcodeScanResult, Task>? OnValidBarcodeScannedAsync { get; set; }

    /// <summary>
    /// Gets or sets a callback that validates a detected barcode before the scanner accepts it and plays the success animation.
    /// </summary>
    public Func<string, Task<BarcodeScanValidationResult>>? ValidateBarcodeAsync { get; set; }

    /// <summary>
    /// Gets or sets a callback invoked after a barcode has been rejected.
    /// </summary>
    public Func<BarcodeScanResult, BarcodeScanValidationResult, Task>? OnInvalidBarcodeScannedAsync { get; set; }

    /// <summary>
    /// Gets or sets a callback invoked when <see cref="CurrentScanCount"/> reaches <see cref="RequiredScanCount"/>.
    /// </summary>
    public Func<Task>? OnRequiredScanCountCompletedAsync { get; set; }

    /// <summary>
    /// Gets or sets a value indicating whether camera scanning should stop when the required scan count has been reached.
    /// </summary>
    public bool StopScanningWhenCompleted { get; set; } = true;

    /// <summary>
    /// Gets or sets the cooldown applied after a scan has been accepted or rejected.
    /// </summary>
    public TimeSpan DuplicateScanCooldown { get; set; } = TimeSpan.FromMilliseconds(800);

    internal event Action? ProgressChanged;
}