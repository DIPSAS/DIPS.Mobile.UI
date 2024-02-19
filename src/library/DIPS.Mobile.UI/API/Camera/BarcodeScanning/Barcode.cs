namespace DIPS.Mobile.UI.API.Camera.BarcodeScanning;

public class Barcode(string rawValue, string format)
{
    public string RawValue { get; } = rawValue;
    public string Format { get; } = format;

    public static Barcode Empty => new(string.Empty, "-1");
}