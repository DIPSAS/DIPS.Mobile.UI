using System.Diagnostics;

namespace DIPS.Mobile.UI.API.Camera.ImageCapturing;

public class CapturedImage
{
    public byte[] AsByteArray { get; }
    
    public string AsByte64String => Convert.ToBase64String(AsByteArray);

    public ImageSize Size { get; }
    
    public ImageTransformation Transformation { get; }

#if __ANDROID__
    public CapturedImage(byte[] asByteArray, AndroidX.Camera.Core.IImageInfo imageInfo, int width, int height, ImageTransformation imageTransformation)
    {
        AsByteArray = asByteArray;
        ImageInfo = imageInfo;
        Size = new ImageSize(asByteArray, width, height);
        Transformation = imageTransformation;
    }

    public AndroidX.Camera.Core.IImageInfo ImageInfo { get; }
#elif __IOS__
    public CapturedImage(byte[] asByteArray, AVFoundation.AVCapturePhoto photo, ImageTransformation imageTransformation)
    {
        AsByteArray = asByteArray;
        Photo = photo;
        Size = new ImageSize(asByteArray, photo.ResolvedSettings.PhotoDimensions.Width, photo.ResolvedSettings.PhotoDimensions.Height);
        Transformation = imageTransformation;
    }

    public AVFoundation.AVCapturePhoto Photo { get; }
#endif
}

public class ImageTransformation(int orientationConstant, string orientationDisplayName)
{
    public int OrientationConstant { get; } = orientationConstant;
    public string OrientationDisplayName { get; } = orientationDisplayName;
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