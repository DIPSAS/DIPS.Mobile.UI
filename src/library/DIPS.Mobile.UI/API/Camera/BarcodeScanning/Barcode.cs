namespace DIPS.Mobile.UI.API.Camera.BarcodeScanning;

public class Barcode(string rawValue)
{
    public string RawValue { get; } = rawValue;

    public static Barcode Empty => new(string.Empty);
}