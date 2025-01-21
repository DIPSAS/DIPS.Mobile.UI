using DIPS.Mobile.UI.Internal.Logging;
using Microsoft.Maui.Platform;
using UIKit;

namespace DIPS.Mobile.UI.Components.Images.Image;

public partial class ImageHandler
{

    private static async partial void TrySetTintColor(ImageHandler handler, Image image)
    {
        if(handler.PlatformView is null)
        {
            DUILogService.LogError<ImageHandler>("PlatformView is null, this should not happen, most likely the issue is that the Content is rendered and then the handler is instantly disconnected");
            return;
        }
        
        try
        {
            var tries = 0;
            // Wait for Image to be set
            while (handler.PlatformView.Image is null)
            {
                if (tries > 10)
                    return;

                await Task.Delay(1);
                tries++;
            }

            if (image.TintColor == null) 
                return;

            if (handler.PlatformView is null)
            {
                DUILogService.LogError<ImageHandler>("PlatformView is null, this should not happen, most likely the issue is that the Content is rendered and then the handler is instantly disconnected");
                return;
            }

            handler.PlatformView.Image =
                handler.PlatformView.Image.ImageWithRenderingMode(UIImageRenderingMode.AlwaysTemplate);
            handler.PlatformView.TintColor = image.TintColor.ToPlatform();
        }
        catch
        {
            // If it doesn't work, it doesn't work
        }
    }

    private static partial void AppendToPropertyMapper()
    {
        PropertyMapper.Add(nameof(Image.Source), MapOverrideSource);
    }

    private static async void MapOverrideSource(ImageHandler handler, Image image)
    {
        await handler.SourceLoader.UpdateImageSourceAsync();
        TrySetTintColor(handler, image);
    }
}