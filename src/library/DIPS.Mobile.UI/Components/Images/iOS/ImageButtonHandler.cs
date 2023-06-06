using Microsoft.Maui.Platform;
using UIKit;

// ReSharper disable once CheckNamespace
namespace DIPS.Mobile.UI.Components.Images;

public partial class ImageButtonHandler
{
    private static async partial void TrySetTintColor(ImageButtonHandler handler, ImageButton imageButton)
    {
        var tries = 0;
        // Wait for Image to be set
        while (handler.PlatformView.ImageView.Image is null)
        {
            if(tries > 10)
                return;
            
            await Task.Delay(1);
            tries++;
        }
        
        handler.PlatformView.SetImage(handler.PlatformView.CurrentImage.ImageWithRenderingMode(UIImageRenderingMode.AlwaysTemplate), UIControlState.Normal);
        handler.PlatformView.TintColor = imageButton.TintColor.ToPlatform();
    }

}