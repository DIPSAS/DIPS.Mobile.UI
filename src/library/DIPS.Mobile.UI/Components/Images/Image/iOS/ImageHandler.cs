using Microsoft.Maui.Platform;
using UIKit;

namespace DIPS.Mobile.UI.Components.Images.Image;

public partial class ImageHandler
{
    private static async partial void TrySetTintColor(ImageHandler handler, Image image)
    {
        var tries = 0;
        // Wait for Image to be set
        while (handler.PlatformView.Image is null)
        {
            if(tries > 10)
                return;
            
            await Task.Delay(1);
            tries++;
        }
        handler.PlatformView.Image = handler.PlatformView.Image.ImageWithRenderingMode(UIImageRenderingMode.AlwaysTemplate);
        handler.PlatformView.TintColor = image.TintColor.ToPlatform();
    }

}