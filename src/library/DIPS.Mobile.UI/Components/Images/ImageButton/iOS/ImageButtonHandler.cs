using DIPS.Mobile.UI.Platforms.iOS;
using Microsoft.Maui.Platform;
using UIKit;

// ReSharper disable once CheckNamespace
namespace DIPS.Mobile.UI.Components.Images.ImageButton;

public partial class ImageButtonHandler
{
    protected override UIButton CreatePlatformView()
    {
        return new UIButtonWithExtraTappableArea
        {
            ClipsToBounds = true
        };
    }

    private partial void AppendPropertyMapper()
    {
        
    }

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

    private static partial void MapAdditionalHitBoxSize(ImageButtonHandler handler, ImageButton imageButton)
    {
        if (handler.PlatformView is UIButtonWithExtraTappableArea uiButton)
            uiButton.AdditionalHitBoxSize = imageButton.AdditionalHitBoxSize;
    }

}