using Android.Widget;
using Microsoft.Maui.Handlers;
using Microsoft.Maui.Platform;

// ReSharper disable once CheckNamespace
namespace DIPS.Mobile.UI.Components.Images;

public partial class ImageHandler : ViewHandler<Image, ImageView>
{
    protected override ImageView CreatePlatformView() => new ImageView(Platform.AppContext);

    private static partial void TrySetSystemImage(ImageHandler imageHandler, Image image)
    {
        if (string.IsNullOrEmpty(image.AndroidProperties.IconResourceName))
        {
            return;
        }

        var androidResource = 
            API.Library.Android.DUI.GetResourceId(image.AndroidProperties.IconResourceName, "drawable");
        if (androidResource != null)
        {
            imageHandler.PlatformView.SetImageResource((int)androidResource);    
        }
    }

    private static partial void TrySetImageColor(ImageHandler imageHandler, Image image)
    {
        if (image is {Color: not null })
        {
            imageHandler.PlatformView.SetColorFilter(image.Color.ToPlatform());
        }
    }
}