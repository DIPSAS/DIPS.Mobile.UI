using CoreGraphics;
using CoreImage;
using UIKit;

namespace DIPS.Mobile.UI.API.Camera.ImageCapturing.Views.BottomToolbar.StreamingState;

internal static class CoreImageFilterExtensions
{
    /// <summary>
    /// Gaussian blur mixes each pixel with surrounding ones. At a photo's edge there is nothing outside to
    /// mix with, so the edges will fade out. EdgeClamp repeats edge pixels outwards to fix this.
    /// </summary>
    /// <param name="image">The input image.</param>
    /// <returns>A clamped image suitable for subsequent blur filtering.</returns>
    public static CIImage RegisterEdgeClampFilter(this CIImage image)
    {
        ArgumentNullException.ThrowIfNull(image);
        
        using var edgeClampFilter = new CIAffineClamp();
        
        edgeClampFilter.InputImage = image;

        var clampedImage = GetOutputImageFromFilter(edgeClampFilter);

        return clampedImage;
    }

    public static CIImage RegisterGaussianBlurFilter(this CIImage image, float radius)
    {
        ArgumentNullException.ThrowIfNull(image);
        ArgumentOutOfRangeException.ThrowIfNegativeOrZero(radius);

        using var gaussianBlurFilter = new CIGaussianBlur();
        
        gaussianBlurFilter.InputImage = image;
        gaussianBlurFilter.Radius = radius;

        var blurredImage = GetOutputImageFromFilter(gaussianBlurFilter);

        return blurredImage;
    }

    public static UIImage? ApplyFilters(this CIImage filteredImage, CGRect originalBounds)
    {
        ArgumentNullException.ThrowIfNull(filteredImage);

        using var renderContext = CIContext.FromOptions(null);

        // Crop back to original size to counteract any edge clamp filter.
        using var renderedBitmap = renderContext.CreateCGImage(filteredImage, originalBounds);

        if (renderedBitmap is null)
            return null;

        var imageWithFiltersApplied = UIImage.FromImage(renderedBitmap);
        
        return imageWithFiltersApplied;
    }

    private static CIImage GetOutputImageFromFilter(CIFilter filter)
    {
        ArgumentNullException.ThrowIfNull(filter);

        var outputImage = filter.OutputImage;
        if (outputImage is null)
            throw new InvalidOperationException($"{filter.GetType().Name} produced no output image.");

        return outputImage;
    }
}
