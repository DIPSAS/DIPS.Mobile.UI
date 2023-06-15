using Microsoft.Maui.Platform;

// ReSharper disable once CheckNamespace
namespace DIPS.Mobile.UI.Components.Images.ImageButton;

public partial class ImageButtonHandler
{
    private static partial void TrySetTintColor(ImageButtonHandler handler, ImageButton imageButton)
    {
        handler.PlatformView.SetColorFilter(imageButton.TintColor.ToPlatform());
    }

}