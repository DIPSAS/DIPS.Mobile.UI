using System.Runtime.InteropServices;
using CoreImage;
using DIPS.Mobile.UI.Internal.Logging;
using UIKit;

namespace DIPS.Mobile.UI.API.Camera.ImageCapturing.Views.BottomToolbar.StreamingState;

internal sealed partial class CapturedImagesGalleryButton
{
    private const float BlurRadius = 220f;
    private const double BlurFadeDurationInSeconds = 0.2;

    private UIImageView? m_blurOverlay;

    private partial void SetThumbnailBlurred(bool shouldBlurThumbnail)
    {
        try
        {
            if (shouldBlurThumbnail)
            {
                FadeBlurredThumbnailIn();
            }
            else
            {
                FadeBlurredThumbnailOut();
            }
        }
        catch (Exception e)
        {
            DUILogService.LogError<CapturedImagesGalleryButton>(e.Message);
        }
    }

    private void FadeBlurredThumbnailIn()
    {
        // The thumbnail's native view is not realized yet, so there is nothing to blur.
        if (m_thumbnail.Handler?.PlatformView is not UIImageView nativeThumbnail)
            return;
        
        // No previous image to blur, for example on the very first capture.
        if (nativeThumbnail.Image?.CGImage is null)
            return;

        using var sourceImage = ConvertToCiImage(nativeThumbnail.Image);
        
        var blurredImage = sourceImage
            .RegisterEdgeClampFilter()
            .RegisterGaussianBlurFilter(BlurRadius)
            .ApplyFilters(sourceImage.Extent);
        
        if (blurredImage is null)
            return;

        var blurredImageView = CreateBlurredOverlay(nativeThumbnail);

        blurredImageView.Image = blurredImage;
        blurredImageView.Frame = nativeThumbnail.Bounds;

        UIView.Animate(BlurFadeDurationInSeconds, () => blurredImageView.Alpha = 1);
    }

    private void FadeBlurredThumbnailOut()
    {
        if (m_blurOverlay is null)
            return;
        
        var blurOverlay = m_blurOverlay;
        
        UIView.Animate(BlurFadeDurationInSeconds, () => blurOverlay.Alpha = 0);
    }

    private UIImageView CreateBlurredOverlay(UIView nativeThumbnail)
    {
        if (m_blurOverlay is not null)
            return m_blurOverlay;

        var blurOverlay = new UIImageView
        {
            ContentMode = UIViewContentMode.ScaleAspectFill,
            ClipsToBounds = true,
            Alpha = 0,
            AutoresizingMask = UIViewAutoresizing.FlexibleWidth | UIViewAutoresizing.FlexibleHeight
        };
        
        blurOverlay.Layer.CornerRadius = (NFloat)Sizes.GetSize(SizeName.radius_small);

        nativeThumbnail.AddSubview(blurOverlay);
        
        m_blurOverlay = blurOverlay;
        
        return blurOverlay;
    }

    private static CIImage ConvertToCiImage(UIImage source)
    {
        ArgumentNullException.ThrowIfNull(source);
        ArgumentNullException.ThrowIfNull(source.CGImage);

        var coreImageInput = new CIImage(source.CGImage);
        return coreImageInput;
    }
}
