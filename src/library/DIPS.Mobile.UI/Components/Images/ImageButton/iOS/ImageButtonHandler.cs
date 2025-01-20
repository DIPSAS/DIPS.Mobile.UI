using DIPS.Mobile.UI.Internal.Logging;
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

    /// <summary>
    /// TODO: I think this can be removed, it causes memory leak
    /// </summary>
    private async void MapOverrideSource(ImageButtonHandler handler, ImageButton imageButton)
    {
        await handler.SourceLoader.UpdateImageSourceAsync();
        TrySetTintColor(handler, imageButton);
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

        if (handler.PlatformView is not null)
        {
            handler.PlatformView.SetImage(handler.PlatformView.ImageView.Image.ImageWithRenderingMode(UIImageRenderingMode.AlwaysTemplate), UIControlState.Normal);
            handler.PlatformView.TintColor = imageButton.TintColor.ToPlatform();
        }
        else
        {
            DUILogService.LogError<ImageButtonHandler>("PlatformView is null, this should not happen, most likely the issue is that the Content is rendered and then the handler is instantly disconnected");
        }
    }

    private static partial void MapAdditionalHitBoxSize(ImageButtonHandler handler, ImageButton imageButton)
    {
        if (handler.PlatformView is UIButtonWithExtraTappableArea uiButton)
            uiButton.AdditionalHitBoxSize = imageButton.AdditionalHitBoxSize;
    }
}