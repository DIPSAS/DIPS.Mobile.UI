using System.Diagnostics;

#if __IOS__
using AVFoundation;
using CoreGraphics;
using UIKit;
#endif


#if __ANDROID__
using Android.Graphics;
using AndroidX.Camera.Core;
#endif



namespace DIPS.Mobile.UI.API.Camera.ImageCapturing;

public class CapturedImage
{
    private readonly Task<byte[]?> m_asRotatedByteArrayTask;
    public byte[] AsByteArray { get; }
    public byte[]? ThumbnailAsByteArray { get; }

    public string AsByte64String => Convert.ToBase64String(AsByteArray);

    public ImageSize Size { get; }
    
    public ImageTransformation Transformation { get; }

    public Task<byte[]?> AsRotatedByteArray()
    {
        return m_asRotatedByteArrayTask;
    }

#if __ANDROID__
    public CapturedImage(byte[] asByteArray, Bitmap bitmap, byte[]? thumbnailAsByteArray, IImageInfo imageInfo,
        int width, int height, ImageTransformation imageTransformation)
    {
        AsByteArray = asByteArray;
        ThumbnailAsByteArray = thumbnailAsByteArray;
        ImageInfo = imageInfo;
        Size = new ImageSize(asByteArray, width, height);
        Transformation = imageTransformation;
        m_asRotatedByteArrayTask = RotateBitmapImageBasedOnOrientation(imageTransformation, bitmap);
    }
    
    internal static async Task<byte[]?> RotateBitmapImageBasedOnOrientation(ImageTransformation transformation, Bitmap bitmapImage)
    {
        var matrix = new Matrix();
        var rotationDegrees = transformation.OrientationConstant switch
        {
            AndroidX.ExifInterface.Media.ExifInterface.OrientationRotate90 => 90,
            AndroidX.ExifInterface.Media.ExifInterface.OrientationRotate180 => 180,
            AndroidX.ExifInterface.Media.ExifInterface.OrientationRotate270 => 270,
            _ => 0
        };
        
        matrix.PostRotate(rotationDegrees);
        
        var rotatedBitmap =
            Bitmap.CreateBitmap(bitmapImage, 0, 0, bitmapImage.Width, bitmapImage.Height, matrix, true);
        
        using var rotatedMemoryStream = new MemoryStream();
        await rotatedBitmap.CompressAsync(Bitmap.CompressFormat.Jpeg!, 100, rotatedMemoryStream);
        rotatedBitmap.Dispose();
        return rotatedMemoryStream.ToArray();
    }

    public AndroidX.Camera.Core.IImageInfo ImageInfo { get; }
#elif __IOS__
    public CapturedImage(byte[] asByteArray, byte[]? thumbnailAsByteArray, AVFoundation.AVCapturePhoto photo, ImageTransformation imageTransformation)
    {
        AsByteArray = asByteArray;
        ThumbnailAsByteArray = thumbnailAsByteArray;
        Photo = photo;
        Size = new ImageSize(asByteArray, photo.ResolvedSettings.PhotoDimensions.Width, photo.ResolvedSettings.PhotoDimensions.Height);
        Transformation = imageTransformation;
        m_asRotatedByteArrayTask = RotatePhoto(photo);
    }

    private Task<byte[]?>? RotatePhoto(AVCapturePhoto photo)
    {
        if (photo.CGImageRepresentation == null) return null;
        
        var uiImage = UIImage.FromImage(photo.CGImageRepresentation);
        var width = photo.CGImageRepresentation.Width;
        var height = photo.CGImageRepresentation.Height;
        UIGraphics.BeginImageContext(new CGSize(width, height));
        uiImage.Draw(new CGRect(0, 0, width, height));
        var resultImage = UIGraphics.GetImageFromCurrentImageContext();
        return Task.FromResult(resultImage.AsJPEG()?.ToArray());
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