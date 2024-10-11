using System.Diagnostics;

namespace DIPS.Mobile.UI.API.Camera.ImageCapturing;

[DebuggerDisplay("{Width}x{Height} (width x height). Size: {SizeInMegaBytesWithTwoDecimals} MB")]
public class ImageSize(byte[] imageAsBytes, int width, int height)
{
    public int Width => width;
    public int Height => height;
    public decimal SizeInBytes => InBytes(imageAsBytes);
    public decimal SizeInMegaBytes => InMegaBytes(imageAsBytes);
    public decimal SizeInMegaBytesWithTwoDecimals => InMegaBytesWithTwoDecimals(imageAsBytes);

    public override string ToString()
    {
        return $"{Width}x{Height}. Size: {SizeInMegaBytesWithTwoDecimals} MB";
    }

    public static decimal InBytes(byte[] imageAsBytes) => imageAsBytes.Length;
    public static decimal InMegaBytes(byte[] imageAsBytes) => (InBytes(imageAsBytes) / 1024) / 1024;
    public static decimal InMegaBytesWithTwoDecimals(byte[] imageAsBytes) => Math.Round(InMegaBytes(imageAsBytes), 2);
}