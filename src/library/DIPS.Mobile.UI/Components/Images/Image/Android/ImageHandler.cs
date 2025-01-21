using Microsoft.Maui.Platform;

namespace DIPS.Mobile.UI.Components.Images.Image;

public partial class ImageHandler
{
    private static partial void TrySetTintColor(ImageHandler handler, Image image)
    {
        if (image.TintColor == null) return;
        handler.PlatformView.SetColorFilter(image.TintColor.ToPlatform());
    }
}