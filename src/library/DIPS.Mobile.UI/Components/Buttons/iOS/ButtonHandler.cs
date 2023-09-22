using System.Runtime.CompilerServices;
using CoreGraphics;
using DIPS.Mobile.UI.Platforms.iOS;
using Microsoft.Maui.Handlers;
using Microsoft.Maui.Platform;
using UIKit;
using IImage = Microsoft.Maui.IImage;

namespace DIPS.Mobile.UI.Components.Buttons;

public partial class ButtonHandler : Microsoft.Maui.Handlers.ButtonHandler
{
    protected override UIButton CreatePlatformView()
    {
        var button = new UIButtonWithExtraTappableArea();
        return button;
    }
    private partial void AppendPropertyMapper()
    {
    }

    private static partial void MapImageToRightSide(ButtonHandler handler, Button button)
    {
        if(button.ImagePlacement == ImagePlacement.Left)
            return;
        
        handler.PlatformView.Transform = CGAffineTransform.MakeScale(-1, 1);
        handler.PlatformView.TitleLabel.Transform = CGAffineTransform.MakeScale(-1, 1);
        handler.PlatformView.ImageView.Transform = CGAffineTransform.MakeScale(-1, 1);
    }
    
    private static async partial void OverrideMapImageSource(ButtonHandler handler, Button button)
    {
        await MapImageSourceAsync(handler, button);
        MapImageTintColor(handler, button);
    }

    private static partial void MapImageTintColor(ButtonHandler handler, Button button)
    {
        if(handler.PlatformView.ImageView.Image is null)
            return;
        
        handler.PlatformView.SetImage(handler.PlatformView.CurrentImage.ImageWithRenderingMode(UIImageRenderingMode.AlwaysTemplate), UIControlState.Normal);
        handler.PlatformView.ImageView.TintColor = button.ImageTintColor.ToPlatform();
    }

    private static partial void MapAdditionalHitBoxSize(ButtonHandler handler, Button button)
    {
        if (handler.PlatformView is UIButtonWithExtraTappableArea uiButton)
            uiButton.AdditionalHitBoxSize = button.AdditionalHitBoxSize;
    }
    
}