using Android.Content.Res;
using Android.Graphics.Drawables;
using DIPS.Mobile.UI.Effects.Touch;
using DIPS.Mobile.UI.Extensions.Android;
using Microsoft.Maui.Platform;

// ReSharper disable once CheckNamespace
namespace DIPS.Mobile.UI.Components.Images.ImageButton;

public partial class ImageButtonHandler
{
    protected override Google.Android.Material.ImageView.ShapeableImageView CreatePlatformView()
    {
        return new ShapeableImageView(Context)
        {
            ImageButton = VirtualView as ImageButton
        };
    }

    protected override void ConnectHandler(Google.Android.Material.ImageView.ShapeableImageView platformView)
    {
        base.ConnectHandler(platformView);
        
        // We need to set ripple effect because MAUI has not yet done this for ImageButtons
        var colorStateList = new ColorStateList([[]], [TouchPlatformEffect.s_defaultNativeAnimationColor.ToPlatform()]);
        
        var ripple = new RippleDrawable(colorStateList, null,  null);
        platformView.Foreground = ripple;
    }

    private static partial void TrySetTintColor(ImageButtonHandler handler, ImageButton imageButton)
    {
        handler.PlatformView.SetColorFilter(imageButton.TintColor.ToPlatform());
    }

    private static partial void MapAdditionalHitBoxSize(ImageButtonHandler handler, ImageButton imageButton)
    {
        handler.PlatformView.SetAdditionalHitBoxSize(imageButton.AdditionalHitBoxSize);
    }
}

