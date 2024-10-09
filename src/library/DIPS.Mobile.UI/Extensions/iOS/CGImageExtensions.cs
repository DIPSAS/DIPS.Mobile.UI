using CoreGraphics;
using UIKit;

namespace DIPS.Mobile.UI.Extensions.iOS;

public static class CGImageExtensions
{
    public static Byte[]? RotateAndGetByteArray(this CGImage image,int width, int height, float degrees)
    {
        if (degrees is < 0 or > 0)
        {
            width = height;
            height = width;
        }
        var radians = degrees * (float)Math.PI / 180;
        
        var colorSpace = image.ColorSpace ?? CGColorSpace.CreateDeviceRGB();

        using var context = new CGBitmapContext(null, width, height, image.BitsPerComponent, image.BytesPerRow, colorSpace, image.BitmapInfo);
        // Set a rotation point to the middle of the image
        context.TranslateCTM(width / 2, height / 2);
        // Rotate
        context.RotateCTM(radians);
        //Draw original image, with adjusted position to center the image
        context.TranslateCTM(-width / 2, -height / 2);
        context.DrawImage(new CGRect(0, 0, width, height), image);

        // Get the image from the context
        var rotatedImage = context.ToImage();
        if (rotatedImage == null) return null;

        // Konverter CGImage til UIImage for å kunne komprimere det til PNG
        return UIImage.FromImage(rotatedImage).AsJPEG(0.1f)?.ToArray();
    }
}