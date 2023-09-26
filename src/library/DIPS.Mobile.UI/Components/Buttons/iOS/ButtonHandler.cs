using CoreGraphics;
using DIPS.Mobile.UI.Platforms.iOS;
using Microsoft.Maui.Handlers;
using Microsoft.Maui.Platform;
using UIKit;
using IImage = Microsoft.Maui.IImage;

namespace DIPS.Mobile.UI.Components.Buttons;

public partial class ButtonHandler : Microsoft.Maui.Handlers.ButtonHandler
{
    private int LargeButtonHeight { get; } = Sizes.GetSize(SizeName.size_12);
    
    protected override UIButton CreatePlatformView()
    {
        var button = new UIButtonWithExtraTappableArea();
        return button;
    }

    protected override void ConnectHandler(UIButton platformView)
    {
        base.ConnectHandler(platformView);

        var percentOfHeight = (float)VirtualView.Height / LargeButtonHeight;
        platformView.ImageView.Transform = CGAffineTransform.MakeScale(percentOfHeight, percentOfHeight);
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

        var imageViewTransform = handler.PlatformView.ImageView.Transform;
        handler.PlatformView.ImageView.Transform = CGAffineTransform.MakeScale(-1 * imageViewTransform.A, 1 * imageViewTransform.D);
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
        
        handler.PlatformView.SetImage(handler.PlatformView.ImageView.Image.ImageWithRenderingMode(UIImageRenderingMode.AlwaysTemplate), UIControlState.Normal);
        handler.PlatformView.ImageView.TintColor = button.ImageTintColor.ToPlatform();
    }

    private static partial void MapAdditionalHitBoxSize(ButtonHandler handler, Button button)
    {
        if (handler.PlatformView is UIButtonWithExtraTappableArea uiButton)
            uiButton.AdditionalHitBoxSize = button.AdditionalHitBoxSize;
    }
    
}