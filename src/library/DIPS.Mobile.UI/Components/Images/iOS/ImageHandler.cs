using Microsoft.Maui.Handlers;
using Microsoft.Maui.Platform;
using UIKit;

// ReSharper disable once CheckNamespace
namespace DIPS.Mobile.UI.Components.Images;

public partial class ImageHandler : ViewHandler<Image, UIImageView>
{
    protected override UIImageView CreatePlatformView() => new UIImageView();
    
    private static partial void TrySetSystemImage(ImageHandler imageHandler, Image image)
    {
        if (string.IsNullOrEmpty(image.iOSProperties.SystemIconName))
        {
            return;
        }

        var systemImage = UIImage.GetSystemImage(image.iOSProperties.SystemIconName);
            imageHandler.PlatformView.AdjustsImageSizeForAccessibilityContentSizeCategory = true;
            imageHandler.PlatformView.Image = systemImage;
    }

    private static partial void TrySetImageColor(ImageHandler imageHandler, Image image)
    {
        if (image?.Color != null)
        {
            imageHandler.PlatformView.TintColor = image?.Color.ToPlatform();
        }
    }
}