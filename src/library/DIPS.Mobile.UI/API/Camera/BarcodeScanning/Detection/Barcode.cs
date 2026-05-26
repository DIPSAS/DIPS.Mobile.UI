namespace DIPS.Mobile.UI.API.Camera.BarcodeScanning;

public class Barcode(string rawValue, string format)
{
    public string RawValue { get; } = rawValue;
    public string Format { get; } = format;

    public static Barcode Empty => new(string.Empty, "-1");

    public override string ToString() => RawValue;

    public override bool Equals(object? obj)
    {
        if (obj is Barcode other)
        {
            return RawValue == other.RawValue && Format == other.Format;
        }
        return false;
    }

    public override int GetHashCode()
    {
        return HashCode.Combine(RawValue, Format);
    }
}