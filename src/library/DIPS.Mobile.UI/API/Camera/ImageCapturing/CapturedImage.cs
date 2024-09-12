using System.Diagnostics;

namespace DIPS.Mobile.UI.API.Camera.ImageCapturing;

public class CapturedImage
{
    public byte[] AsByteArray { get; }

    public ImageSize Size { get; }

#if __ANDROID__
    public CapturedImage(byte[] asByteArray, AndroidX.Camera.Core.IImageInfo imageInfo, int width, int height)
    {
        AsByteArray = asByteArray;
        ImageInfo = imageInfo;
        Size = new ImageSize(asByteArray, width, height);
    }

    public AndroidX.Camera.Core.IImageInfo ImageInfo { get; }
#elif __IOS__
    public CapturedImage(byte[] asByteArray, AVFoundation.AVCapturePhoto photo)
    {
        AsByteArray = asByteArray;
        Photo = photo;
        Size = new ImageSize(asByteArray, photo.ResolvedSettings.PhotoDimensions.Width, photo.ResolvedSettings.PhotoDimensions.Height);
    }

    public AVFoundation.AVCapturePhoto Photo { get; }
#endif
}

[DebuggerDisplay("{Width}x{Height}. Size: {SizeInMegaBytesWithTwoDecimals} MB")]
public class ImageSize(byte[] imageAsBytes, int width, int height)
{
    public int Width => width;
    public int Height => height;
    public decimal SizeInBytes => imageAsBytes.Length;
    public decimal SizeInMegaBytes => (SizeInBytes / 1024) / 1024;
    public decimal SizeInMegaBytesWithTwoDecimals => Math.Round(SizeInMegaBytes, 2);

    public override string ToString()
    {
        return $"{Width}x{Height}. Size: {SizeInMegaBytesWithTwoDecimals} MB";
    }
}