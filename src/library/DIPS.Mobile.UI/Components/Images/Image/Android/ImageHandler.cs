using Microsoft.Maui.Platform;

namespace DIPS.Mobile.UI.Components.Images.Image;

public partial class ImageHandler
{
    private static partial void TrySetTintColor(ImageHandler handler, Image image)
    {
        if (image.TintColor is null)
        {
            handler.PlatformView.ClearColorFilter();
        }
        else
        {
            handler.PlatformView.SetColorFilter(image.TintColor.ToPlatform());
        }
    }
}