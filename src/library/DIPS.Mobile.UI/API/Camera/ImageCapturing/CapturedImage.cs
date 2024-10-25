




using DIPS.Mobile.UI.API.Library;
using IImage = Microsoft.Maui.IImage;
using Image = Microsoft.Maui.Controls.Image;
#if __IOS__
using ImageIO;
using DIPS.Mobile.UI.Extensions.iOS;
using CGImageProperties = ImageIO.CGImageProperties;
using Foundation;
using CoreGraphics;
using UIKit;
using AVFoundation;
using DIPS.Mobile.UI.API.Library;
#endif

#if __ANDROID__
using Android.Graphics;
using Android.Media;
using AndroidX.Camera.Core;
using DIPS.Mobile.UI.API.Library;
#endif



namespace DIPS.Mobile.UI.API.Camera.ImageCapturing;

public class CapturedImage
{
    public byte[] AsByteArray { get; }
#if __ANDROID__
    public Bitmap ImageBitmap { get; }
    public Bitmap ThumbnailBitmap { get; }
    public IImageProxy ImageProxy { get; }
#elif __IOS__
    private CGImage m_cgImage;
    private readonly CGImage m_thumbnailCgImage;
#endif
    
    public byte[]? ThumbnailAsByteArray { get; }

    public string AsByte64String => Convert.ToBase64String(AsByteArray);
    public string ThumbnailAs64String => ThumbnailAsByteArray == null ? string.Empty : Convert.ToBase64String(ThumbnailAsByteArray);

    public ImageSize Size { get; }
    
    public ImageTransformation Transformation { get; }

#if !__MOBILE__
    public Task<CapturedImage> Rotate(bool clockwise)
    {
        return Task.FromResult(new CapturedImage());
    }
#endif

    internal static async Task RotateImage(bool clockwise, Image image, CapturedImage rotatedImage, double startingWidth, double startingHeight, OrientationDegree startingOrientationDegree)
    {
        var rotation = clockwise ? image.Rotation + 90 : image.Rotation - 90;

        if (startingOrientationDegree is OrientationDegree.Orientation0 or OrientationDegree.Orientation180)
        {
            if (rotatedImage.Transformation.OrientationDegree is OrientationDegree.Orientation0 or OrientationDegree.Orientation180)
            {
                InvertWidthAndHeight();
            }
            else
            {
                ResetHeightAndWidth();
            }
        }
        else
        {
            if (rotatedImage.Transformation.OrientationDegree is OrientationDegree.Orientation90 or OrientationDegree.Orientation270)
            {
                InvertWidthAndHeight();
            }
            else
            {
                ResetHeightAndWidth();
            }
        }

        await image.RotateTo(rotation, 500, easing: Easing.CubicInOut);
        switch (rotation)
        {
            case > 270:
                image.Rotation = 0;
                break;
            case < 0:
                image.Rotation += 360;
                break;
        }

        return;

        void InvertWidthAndHeight()
        {
            _ = image.WidthTo(image.Height, easing: Easing.CubicInOut);
            _ = image.HeightTo(image.Width, easing: Easing.CubicInOut);
        }

        void ResetHeightAndWidth()
        {
            _ = image.WidthTo(startingWidth, easing: Easing.CubicInOut);
            _ = image.HeightTo(startingHeight, easing: Easing.CubicInOut);
        }
    }    

#if __ANDROID__
    public CapturedImage(byte[] asByteArray, Bitmap imageBitmap, byte[]? thumbnailAsByteArray, Bitmap thumbnailBitmap, IImageProxy imageProxy, ImageTransformation imageTransformation)
    {
        AsByteArray = asByteArray;
        ImageBitmap = imageBitmap;
        ThumbnailAsByteArray = thumbnailAsByteArray;
        ThumbnailBitmap = thumbnailBitmap;
        ImageProxy = imageProxy;
        ImageInfo = imageProxy.ImageInfo;
        Size = new ImageSize(asByteArray, imageProxy.Width, imageProxy.Height);
        Transformation = imageTransformation;
    }

    /// <summary>
    /// Rotates the image 90 degrees clockwise
    /// </summary>
    /// <returns>A new <see cref="CapturedImage"/> object</returns>
    internal async Task<CapturedImage> Rotate(bool clockwise)
    {
        var (rotatedImageBitmap, rotatedImageBytes) = await RotateBitmap(ImageBitmap, clockwise ? 90 : -90);
        var (rotatedThumbnailBitmap, rotatedThumbnailBytes) = await RotateBitmap(ThumbnailBitmap, clockwise ? 90 : -90);

        var newOrientationDegree = (OrientationDegree)((int)(Transformation.OrientationDegree) + 1);
        if(newOrientationDegree > OrientationDegree.Orientation270)
        {
            newOrientationDegree = OrientationDegree.Orientation0;
        }
        
        return new CapturedImage(rotatedImageBytes, rotatedImageBitmap, rotatedThumbnailBytes, rotatedThumbnailBitmap, ImageProxy, new ImageTransformation(newOrientationDegree, newOrientationDegree.ToString()));
    }

    private static async Task<(Bitmap rotatedImageBitmap, byte[] rotatedImageBytes)> RotateBitmap(Bitmap bitmap, float degrees)
    {
        var matrix = new Matrix();
        matrix.PostRotate(degrees);
    
        var rotatedImageBitmap =
            Bitmap.CreateBitmap(bitmap, 0, 0, bitmap.Width, bitmap.Height, matrix, true);
    
        using var rotatedMemoryStream = new MemoryStream();
        await rotatedImageBitmap.CompressAsync(Bitmap.CompressFormat.Jpeg!, 95, rotatedMemoryStream);
        return (rotatedImageBitmap, rotatedMemoryStream.ToArray());
    }

    internal static async Task<(byte[], Bitmap)> RotateBitmapImageBasedOnOrientationAsByteArray(ImageTransformation transformation, Bitmap bitmapImage)
    {
        var rotationDegrees = transformation.OrientationDegree switch
        {
            OrientationDegree.Orientation90 => 180,
            OrientationDegree.Orientation180 => 270,
            OrientationDegree.Orientation270 => 0,
            _ => 90
        };
        
        var (rotatedBitmap, rotatedBytes) = await RotateBitmap(bitmapImage, rotationDegrees);
        return (rotatedBytes, rotatedBitmap);
    }
    
    internal static async Task<(byte[], Bitmap)> RotateBitmapImageBasedOnOrientation(ImageTransformation transformation, Bitmap bitmapImage)
    {
        var matrix = new Matrix();
        var rotationDegrees = transformation.OrientationDegree switch
        {
            OrientationDegree.Orientation90 => 180,
            OrientationDegree.Orientation180 => 270,
            OrientationDegree.Orientation270 => 0,
            _ => 90
        };
        
        matrix.PostRotate(rotationDegrees);
        
        var rotatedBitmap =
            Bitmap.CreateBitmap(bitmapImage, 0, 0, bitmapImage.Width, bitmapImage.Height, matrix, true);
        
        using var rotatedMemoryStream = new MemoryStream();
        await rotatedBitmap.CompressAsync(Bitmap.CompressFormat.Jpeg!, 95, rotatedMemoryStream);
        return (rotatedMemoryStream.ToArray(), rotatedBitmap);
    }
    
    public AndroidX.Camera.Core.IImageInfo ImageInfo { get; }
#elif __IOS__
    public CapturedImage(byte[] asByteArray, byte[]? thumbnailAsByteArray, CGImage cgImage, CGImage thumbnailCgImage, AVCapturePhoto photo, ImageTransformation imageTransformation)
    {
        m_cgImage = cgImage;
        m_thumbnailCgImage = thumbnailCgImage;

        AsByteArray = asByteArray;
        ThumbnailAsByteArray = thumbnailAsByteArray;
        Photo = photo;
        Size = new ImageSize(asByteArray, photo.ResolvedSettings.PhotoDimensions.Width, photo.ResolvedSettings.PhotoDimensions.Height);
        Transformation = imageTransformation;
    }

    public Task<CapturedImage> Rotate(bool clockwise)
    {
        var modifierValue = clockwise ? 1 : -1;
        
        var newOrientationDegree = (OrientationDegree)((int)(Transformation.OrientationDegree) + modifierValue);
        newOrientationDegree = newOrientationDegree switch
        {
            > OrientationDegree.Orientation270 => OrientationDegree.Orientation0,
            < OrientationDegree.Orientation0 => OrientationDegree.Orientation270,
            _ => newOrientationDegree
        };

        var rotatedCgImage = Rotate90Degrees(m_cgImage, clockwise);
        var rotatedThumbnailCgImage = Rotate90Degrees(m_thumbnailCgImage, clockwise);
        
        var rotatedUiImage = new UIImage(rotatedCgImage);
        var rotatedThumbnailUiImage = new UIImage(rotatedThumbnailCgImage);
        
        return Task.FromResult(new CapturedImage(rotatedUiImage.AsJPEG(0.8f)?.ToArray() ?? [], rotatedThumbnailUiImage.AsJPEG(.8f)?.ToArray(), rotatedCgImage, rotatedThumbnailCgImage, Photo, new ImageTransformation(newOrientationDegree, newOrientationDegree.ToString())));
    }

    private CGImage Rotate90Degrees(CGImage cgImage, bool clockwise)
    {
        var modifierValue = clockwise ? -1 : 1;

        nfloat width = m_cgImage.Width;
        nfloat height = m_cgImage.Height;
        
        CGSize originalSize = new CGSize(width, height);
        CGSize rotatedSize = new CGSize(height, width);

        // Create a new CGContext for the rotated image
        using var context = new CGBitmapContext(IntPtr.Zero,
            (int)rotatedSize.Width,
            (int)rotatedSize.Height,
            m_cgImage.BitsPerComponent,
            m_cgImage.BytesPerRow,
            m_cgImage.ColorSpace,
            m_cgImage.BitmapInfo);

        // Set the origin to the middle of the context
        context.TranslateCTM(rotatedSize.Width / 2, rotatedSize.Height / 2);
        context.RotateCTM((float)(90 * modifierValue * Math.PI / 180)); // Convert degrees to radians
        context.TranslateCTM(-originalSize.Width / 2, -originalSize.Height / 2);

        // Draw the original image into the context
        context.DrawImage(new CGRect(0, 0, originalSize.Width, originalSize.Height), m_cgImage);
        
        // Get the rotated CGImage
        var rotatedCgImage = context.ToImage();
        
        // Convert the rotated CGImage to a UIImage for further use
        return rotatedCgImage!;
    }
    
    public static (UIImage, CGImage) RotateCgImageToPortrait(CGImage cgImage, UIImageOrientation photoOrientation)
    {
        nfloat width = cgImage.Width;
        nfloat height = cgImage.Height;

        // Set the correct transform based on the photo's orientation
        CGAffineTransform transform;

        Console.WriteLine($"Orientation {photoOrientation}");
        switch (photoOrientation)
        {
            case UIImageOrientation.Right:
                // Rotate 90 degrees counterclockwise
                transform = CGAffineTransform.MakeTranslation(0, height);
                transform = CGAffineTransform.Rotate(transform, (nfloat)(-Math.PI / 2));
                (width, height) = (height, width); // Swap width and height
                break;
            case UIImageOrientation.Left:
                // Rotate 90 degrees clockwise
                transform = CGAffineTransform.MakeTranslation(width, 0);
                transform = CGAffineTransform.Rotate(transform, (nfloat)(Math.PI / 2));
                (width, height) = (height, width); // Swap width and height
                break;
            case UIImageOrientation.Down:
                // Rotate 180 degrees
                transform = CGAffineTransform.MakeTranslation(width, height);
                transform = CGAffineTransform.Rotate(transform, (nfloat)Math.PI);
                break;
            case UIImageOrientation.Up:
            default:
                // No rotation needed for portrait mode
                return (new UIImage(cgImage), cgImage);
        }

        // Create a new CGContext for the rotated image
        using var context = new CGBitmapContext(IntPtr.Zero,
            (int)width,
            (int)height,
            cgImage.BitsPerComponent,
            cgImage.BytesPerRow,
            cgImage.ColorSpace,
            cgImage.BitmapInfo);
        // Apply the transformation to the context
        context.ConcatCTM(transform);

        // Draw the CGImage with the correct dimensions
        var x = 0;
        var y = 0;
        switch (photoOrientation)
        {
            case UIImageOrientation.Right:
                x = (int) (width - cgImage.Width); //Move X (which is Y when it needs rotation = 90 degrees) to the correct position    
                break;
            case UIImageOrientation.Left:
                y = (int) (height - cgImage.Height);
                break;
        }

        context.DrawImage(new CGRect(x, y, cgImage.Width, cgImage.Height), cgImage);

        // Get the rotated CGImage
        var rotatedCgImage = context.ToImage();

        // Convert the rotated CGImage to a UIImage for further use
        return (new UIImage(rotatedCgImage!), rotatedCgImage!);
    }
    
    public AVCapturePhoto Photo { get; }
    
#endif
}