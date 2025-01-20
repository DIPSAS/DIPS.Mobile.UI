using Android.Content.Res;
using Android.Graphics.Drawables;
using DIPS.Mobile.UI.Effects.Touch;
using DIPS.Mobile.UI.Extensions.Android;
using DIPS.Mobile.UI.Internal.Logging;
using Google.Android.Material.ImageView;
using Microsoft.Maui.Platform;

// ReSharper disable once CheckNamespace
namespace DIPS.Mobile.UI.Components.Images.ImageButton;

public partial class ImageButtonHandler
{

    private partial void AppendPropertyMapper() { }
    protected override void ConnectHandler(ShapeableImageView platformView)
    {
        base.ConnectHandler(platformView);
        
        // We need to set ripple effect because MAUI has not yet done this for ImageButtons
        var colorStateList = new ColorStateList(
            new[] { Array.Empty<int>() },
            new[] { (int)TouchPlatformEffect.DefaultNativeAnimationColor.ToPlatform() });
        
        var ripple = new RippleDrawable(colorStateList, null,  null);
        platformView.Foreground = ripple;
    }

    private static partial void TrySetTintColor(ImageButtonHandler handler, ImageButton imageButton)
    {
        if (handler.PlatformView is null)
        {
            DUILogService.LogError<ImageButtonHandler>("PlatformView is null, this should not happen, most likely the issue is that the Content is rendered and then the handler is instantly disconnected");
            return;
        }
            
        handler.PlatformView?.SetColorFilter(imageButton.TintColor.ToPlatform());
    }

    private static async partial void MapAdditionalHitBoxSize(ImageButtonHandler handler, ImageButton imageButton)
    {
        await Task.Delay(1);

        if (handler.PlatformView is null)
        {
            DUILogService.LogError<ImageButtonHandler>("PlatformView is null, this should not happen, most likely the issue is that the Content is rendered and then the handler is instantly disconnected");
            return;
        }
        
        handler.PlatformView?.SetAdditionalHitBoxSize(imageButton, imageButton.AdditionalHitBoxSize, handler.MauiContext!);
        
    }
}

