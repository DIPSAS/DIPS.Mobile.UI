using CoreGraphics;
using UIKit;

namespace DIPS.Mobile.UI.Extensions.iOS;

public static class UIImageExtensions
{
    /// <summary>
    /// Resize the <see cref="UIImage"/> without stretching it. The height and width will be uniform. 
    /// </summary>
    /// <param name="uiImage">The <see cref="UIImage"/> to resize.></param>
    /// <param name="uniformSize">The uniform size to resize the <see cref="UIImage"/> to.</param>
    /// <returns></returns>
    public static UIImage ResizeImage(this UIImage uiImage, double uniformSize) => ResizeImage(uiImage, uniformSize, uniformSize);

    /// <summary>
    /// Resize the <see cref="UIImage"/> without stretching it.
    /// </summary>
    /// <param name="uiImage">The <see cref="UIImage"/> to resize</param>
    /// <param name="width">The width to set the size to.</param>
    /// <param name="height">The height to set the size to.</param>
    /// <returns>A new <see cref="UIImage"/></returns>
    /// <remarks>Taken from here: https://www.advancedswift.com/resize-uiimage-no-stretching-swift/</remarks>
    public static UIImage ResizeImage(this UIImage uiImage, double width, double height)
    {
        var targetSize = new CGSize(new nfloat(width), new nfloat(height));
        // Determine the scale factor that preserves aspect ratio
        var widthRatio = targetSize.Width / uiImage.Size.Width;
        var heightRatio = targetSize.Height / uiImage.Size.Height;

        var scaleFactor = Math.Min(widthRatio, heightRatio);
        
        // Compute the new image size that preserves aspect ratio
        var scaledImageSize = new CGSize(
            width: uiImage.Size.Width * scaleFactor,
            height: uiImage.Size.Height * scaleFactor
        );

        // Draw and return the resized UIImage
        var renderer = new UIGraphicsImageRenderer(
            size: scaledImageSize
        );

        var scaledImage = renderer.CreateImage((_ =>
        {
            uiImage.Draw(new CGRect(location: CGPoint.Empty, scaledImageSize));
        }));

        return scaledImage;
    }
}