using Android.Content.Res;
using Android.Graphics.Drawables;
using Android.Graphics.Drawables.Shapes;
using Android.Widget;
using DIPS.Mobile.UI.Components.Images.ImageButton.Android;
using DIPS.Mobile.UI.Effects.Touch;
using DIPS.Mobile.UI.Extensions.Android;
using Google.Android.Material.ImageView;
using Google.Android.Material.Shape;
using Java.Util;
using Microsoft.Maui.Controls.Shapes;
using Microsoft.Maui.Platform;
using Colors = Microsoft.Maui.Graphics.Colors;

// ReSharper disable once CheckNamespace
namespace DIPS.Mobile.UI.Components.Images.ImageButton;

public partial class ImageButtonHandler
{
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

    private partial void AppendPropertyMapper()
    {
        PropertyMapper.Add(nameof(IImageButton.Padding), OverrideMapPadding);
    }

    //TODO .NET8: Remove
    // To fix bug: https://github.com/dotnet/maui/pull/14905
    private void OverrideMapPadding(ImageButtonHandler handler, ImageButton imageButton)
    {
        handler.PlatformView.SetContentPadding(imageButton);
        handler.PlatformView.Post(() =>
        {
            handler.PlatformView.SetContentPadding(imageButton);
        });
        handler.PlatformView.SetContentPadding(imageButton);
    }

    private static partial void TrySetTintColor(ImageButtonHandler handler, ImageButton imageButton)
    {
        handler.PlatformView.SetColorFilter(imageButton.TintColor.ToPlatform());
    }

    private static async partial void MapAdditionalHitBoxSize(ImageButtonHandler handler, ImageButton imageButton)
    {
        await Task.Delay(1);

        handler.PlatformView.SetAdditionalHitBoxSize(imageButton, imageButton.AdditionalHitBoxSize, handler.MauiContext!);
        
    }

}

