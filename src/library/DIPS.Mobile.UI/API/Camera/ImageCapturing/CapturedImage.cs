



#if __IOS__
using DIPS.Mobile.UI.Extensions.iOS;
using Foundation;
using CoreGraphics;
using UIKit;
using AVFoundation;
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
    public string ThumbnailAs64String => ThumbnailAsByteArray == null ? string.Empty : Convert.ToBase64String(ThumbnailAsByteArray);

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
        await rotatedBitmap.CompressAsync(Bitmap.CompressFormat.Jpeg!, 95, rotatedMemoryStream);
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

    private Task<byte[]?> RotatePhoto(AVCapturePhoto photo)
    {
        if (photo.CGImageRepresentation == null) return Task.FromResult<byte[]?>(null);
        // var image = UIImage.FromImage(photo.CGImageRepresentation, 1f, photo.Properties.Orientation.ToUIImageOrientation());
        // var transform = CGAffineTransform.MakeIdentity();
        // var shouldTransform = true;
        // var imageOrientation = image.Orientation;
        // Console.WriteLine($"orientation: {imageOrientation}");
        // switch (imageOrientation)
        // {
        //     case UIImageOrientation.Up:
        //     case UIImageOrientation.UpMirrored:
        //         shouldTransform = false;
        //         //Do nothing
        //         break;
        //     case UIImageOrientation.Down:
        //     case UIImageOrientation.DownMirrored:
        //         transform.Translate(image.Size.Width, image.Size.Height);
        //         transform.Rotate((nfloat)Math.PI);
        //         break;
        //     case UIImageOrientation.Left:
        //     case UIImageOrientation.LeftMirrored:
        //         transform.Translate(image.Size.Width, 0);
        //         transform.Rotate((nfloat)(Math.PI) / 2.0f);
        //         break;
        //     case UIImageOrientation.Right:
        //     case UIImageOrientation.RightMirrored:
        //         transform.Translate(0, image.Size.Height);
        //         transform.Rotate((nfloat)(Math.PI) / -2.0f);
        //         break;
        //     default:
        //         throw new ArgumentOutOfRangeException();
        // }
        //
        // var drawRect = new CGRect(0, 0, image.Size.Width, image.Size.Height);
        // switch (imageOrientation)
        // {
        //     case UIImageOrientation.Left:
        //     case UIImageOrientation.LeftMirrored:
        //     case UIImageOrientation.Right:
        //     case UIImageOrientation.RightMirrored:
        //         drawRect = new CGRect(0, 0, image.Size.Height, image.Size.Width);
        //         break;
        //     case UIImageOrientation.Up:
        //     case UIImageOrientation.Down:
        //     case UIImageOrientation.UpMirrored:
        //     case UIImageOrientation.DownMirrored:
        //     default:
        //         break;
        // }
        var rotatedImage = RotateCGImageToPortrait(photo.CGImageRepresentation, photo.Properties.Orientation.ToUIImageOrientation());
        return Task.FromResult(rotatedImage.AsJPEG(0.8f)?.ToArray());
        // var width = image.Size.Width;
        // var height = image.Size.Height;
        // var degrees = 0;
        // Console.WriteLine($"orientation: {photo.Properties.Orientation}");
        // switch (photo.Properties.Orientation)
        // {
        //     case CIImageOrientation.RightTop:
        //         degrees = 90;
        //         width  = image.Size.Height;
        //         height = image.Size.Width;
        //         break;
        //     case CIImageOrientation.BottomLeft:
        //         degrees = 180;
        //         break;
        // }
        // Console.WriteLine($"degrees: {degrees}");
        // Console.WriteLine($"height: {height}, width: {width}");
        // // var contextRect = new CGRect(0, 0, width, height);
        //
        // var contextRect = new CGRect(0,0, width, height);
        // UIGraphics.BeginImageContext(new CGSize(width, height));
        //
        // // if (shouldTransform)
        // // {
        //     // UIGraphics.GetCurrentContext().ConcatCTM(transform);
        //     // Console.WriteLine($"degrees: {degrees}");
        //     // CGRectGetMidX(contextRect), CGRectGetMidY(contextRect)
        //     var context = UIGraphics.GetCurrentContext();
        //
        //     //Make sure its not mirrored
        //     // var transform = CGAffineTransform.MakeTranslation(0, height);
        //     // transform = CGAffineTransform.Scale(transform, 1.0f, -1.0f);
        //     // context.ConcatCTM(transform);
        //     // var transform =CGAffineTransformMakeTranslation(0.0, height);
        //     // transform = CGAffineTransformScale(transform, 1.0, -1.0);
        //     // CGContextConcatCTM(context, transform);
        //     
        //     //Rotate it
        //     var contextCenter = new CGPoint(contextRect.GetMidX(), contextRect.GetMidY());
        //     context.TranslateCTM( contextCenter.X, contextCenter.Y );
        //     context.RotateCTM((nfloat)(Math.PI / 180) * degrees);
        //     context.TranslateCTM( -contextCenter.X, -contextCenter.Y );
        //     if (degrees == 90)
        //     {
        //         image.Draw(new CGRect(0,0,height,width));    
        //     }
        //     else
        //     {
        //         image.Draw(new CGRect(0, 0, width,height));
        //     }
            
            // context.DrawImage(new CGRect(0, 0, image.Size.Width,image.Size.Height), photo.CGImageRepresentation);
        // }
        // else
        // {
        
        // }
        var newImage = UIGraphics.GetImageFromCurrentImageContext();
        UIGraphics.EndImageContext();
        return Task.FromResult(newImage.AsJPEG(0.8f)?.ToArray());
    }
    
    
public UIImage RotateCGImageToPortrait(CGImage cgImage, UIImageOrientation photoOrientation)
{
    nfloat width = cgImage.Width;
    nfloat height = cgImage.Height;

    // Set the correct transform based on the photo's orientation
    CGAffineTransform transform = CGAffineTransform.MakeIdentity();

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
            return new UIImage(cgImage);
    }

    Console.WriteLine($"height: {height}, width: {width}");
    // Create a new CGContext for the rotated image
    using (var context = new CGBitmapContext(IntPtr.Zero,
                                             (int)width,
                                             (int)height,
                                             cgImage.BitsPerComponent,
                                             cgImage.BytesPerRow,
                                             cgImage.ColorSpace,
                                             cgImage.BitmapInfo))
    {
        // Apply the transformation to the context
        context.ConcatCTM(transform);

        // Draw the CGImage with the correct dimensions
        var x = 0;
        var y = 0;
        if (photoOrientation == UIImageOrientation.Right)
        {
            x = (int) (width - cgImage.Width); //Move X (which is Y when it needs rotation = 90 degrees) to the correct position    
        }

        if (photoOrientation == UIImageOrientation.Left)
        {
            y = (int) (height - cgImage.Height);
        }
        
        context.DrawImage(new CGRect(x, y, cgImage.Width, cgImage.Height), cgImage);

        // Get the rotated CGImage
        var rotatedCGImage = context.ToImage();

        // Convert the rotated CGImage to a UIImage for further use
        return new UIImage(rotatedCGImage);
    }
}




    public AVFoundation.AVCapturePhoto Photo { get; }
    //
    //
    // autoreleasepool {
    //     var context: CGContext?
    //
    //         guard let colorSpace = cgImage.colorSpace, let _context = CGContext(data: nil, width: Int(image.size.width), height: Int(image.size.height), bitsPerComponent: cgImage.bitsPerComponent, bytesPerRow: 0, space: colorSpace, bitmapInfo: CGImageAlphaInfo.premultipliedLast.rawValue) else {
    //         return
    //     }
    //     context = _context
    //
    //     context?.concatenate(transform)
    //
    //     var drawRect: CGRect = .zero
    //     switch imageOrientation {
    //         case .left, .leftMirrored, .right, .rightMirrored:
    //         drawRect.size = CGSize(width: size.height, height: size.width)
    //         default:
    //         drawRect.size = CGSize(width: size.width, height: size.height)
    //     }
    //
    //     context?.draw(cgImage, in: drawRect)
    //
    //     guard let newCGImage = context?.makeImage() else {
    //         return
    //     }
    //     cgImage = newCGImage
    // }
    //
    // let uiImage = UIImage(cgImage: cgImage, scale: 1, orientation: .up)
        
        
        /**
         *  static func fixedOrientation(for image: UIImage) -> UIImage? {

        guard image.imageOrientation != .up else {
            return image
        }

        let size = image.size

        let imageOrientation = image.imageOrientation

        var transform: CGAffineTransform = .identity

        switch image.imageOrientation {
        case .down, .downMirrored:
            transform = transform.translatedBy(x: size.width, y: size.height)
            transform = transform.rotated(by: CGFloat.pi)
        case .left, .leftMirrored:
            transform = transform.translatedBy(x: size.width, y: 0)
            transform = transform.rotated(by: CGFloat.pi / 2.0)
        case .right, .rightMirrored:
            transform = transform.translatedBy(x: 0, y: size.height)
            transform = transform.rotated(by: CGFloat.pi / -2.0)
        case .up, .upMirrored:
            break
        @unknown default:
            break
        }

        // Flip image one more time if needed to, this is to prevent flipped image
        switch imageOrientation {
        case .upMirrored, .downMirrored:
            transform = transform.translatedBy(x: size.width, y: 0)
            transform = transform.scaledBy(x: -1, y: 1)
        case .leftMirrored, .rightMirrored:
            transform = transform.translatedBy(x: size.height, y: 0)
            transform = transform.scaledBy(x: -1, y: 1)
        case .up, .down, .left, .right:
            break
        @unknown default:
            break
        }

        guard var cgImage = image.cgImage else {
            return nil
        }

        autoreleasepool {
            var context: CGContext?

            guard let colorSpace = cgImage.colorSpace, let _context = CGContext(data: nil, width: Int(image.size.width), height: Int(image.size.height), bitsPerComponent: cgImage.bitsPerComponent, bytesPerRow: 0, space: colorSpace, bitmapInfo: CGImageAlphaInfo.premultipliedLast.rawValue) else {
                return
            }
            context = _context

            context?.concatenate(transform)

            var drawRect: CGRect = .zero
            switch imageOrientation {
            case .left, .leftMirrored, .right, .rightMirrored:
                drawRect.size = CGSize(width: size.height, height: size.width)
            default:
                drawRect.size = CGSize(width: size.width, height: size.height)
            }

            context?.draw(cgImage, in: drawRect)

            guard let newCGImage = context?.makeImage() else {
                return
            }
            cgImage = newCGImage
        }

        let uiImage = UIImage(cgImage: cgImage, scale: 1, orientation: .up)
        return uiImage
    }
         */
        // if (photo.CGImageRepresentation != null)
        // {
        //
        //     var image = UIImage.FromImage(photo.CGImageRepresentation, 1f, photo.Properties.Orientation.ToUIImageOrientation());
        //     var size = image.Size;);
        //     UIGraphics.BeginImageContext(size, );
        //     image.Draw(new CGRect(0,0,size.Width,size.Height));
        //     var newImage = UIGraphics.GetImageFromCurrentImageContext();
        //     UIGraphics.EndImageContext();
        //     return Task.FromResult(newImage.AsJPEG(0.8f)?.ToArray());
        // }
        //     // var uiImage = UIImage.FromImage(photo.CGImageRepresentation, 0.1f, UIImageOrientation.Right);
        //     // return Task.FromResult(uiImage.AsJPEG(0.8f)?.ToArray());
        //
        // return null;
#endif
}