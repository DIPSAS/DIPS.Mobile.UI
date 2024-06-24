using Android.Graphics.Drawables;
using DIPS.Mobile.UI.Extensions.Android;
using Google.Android.Material.Button;
using Microsoft.Maui.Controls.Platform;
using Microsoft.Maui.Platform;
using Colors = Microsoft.Maui.Graphics.Colors;

namespace DIPS.Mobile.UI.Components.Buttons;

public partial class ButtonHandler : Microsoft.Maui.Handlers.ButtonHandler
{
    /// <summary>
    ///  MAUI changes the Background to a BorderDrawable if the Background changes
    ///  Thus we have to set a RippleDrawable to the Foreground
    /// 
    ///  If the Background has been changed the Button has a BorderDrawable as its background
    ///  MAUI only changes the radius on the Drawable
    ///  Thus we need to set the CornerRadius on the button itself so that our foreground ripple
    ///  does not go out of bounds
    /// </summary>
    protected override void ConnectHandler(MaterialButton platformView)
    {
        base.ConnectHandler(platformView);

        var rippleColor = Resources.Colors.Colors.GetColor(ColorName.color_neutral_90);
        var ripple = new RippleDrawable(new Color(rippleColor.Red, rippleColor.Green, rippleColor.Blue, 0.1f).ToDefaultColorStateList(),
            null,
            platformView.Background);

        // MaterialButton sets text to all caps as default
        platformView.SetAllCaps(false);
        
        platformView.CornerRadius = (int)platformView.Context.ToPixels(VirtualView.CornerRadius);
        platformView.Foreground = ripple;

        /*platformView.IconSize = (int)platformView.Context.ToPixels(VirtualView.Height / 2);*/
        platformView.Icon?.SetColorFilter((VirtualView as Button)!.ImageTintColor.ToPlatform(), FilterMode.SrcAtop);
    }

    private static void OverrideMapBackgroundColor(ButtonHandler handler, Button button)
    {
        handler.PlatformView.UpdateBackground(button);
        handler.PlatformView.StateListAnimator = null; //To remove shadows properly, bug in Android
    }
    
    private static void OverrideMapBackground(ButtonHandler handler, Button button)
    {
        OverrideMapBackgroundColor(handler, button);
    }

    private static async partial void OverrideMapImageSource(ButtonHandler handler, Button button)
    {
        await MapImageSourceAsync(handler, button);
        MapImageTintColor(handler, button);
    }
    
    private static partial void MapImageTintColor(ButtonHandler handler, Button button)
    {
        handler.PlatformView.Icon?.SetColorFilter(button.ImageTintColor.ToPlatform(), FilterMode.SrcAtop);
    }
    
    private static partial void MapImageToRightSide(ButtonHandler handler, Button button)
    {
        if(button.ImagePlacement == ImagePlacement.Left)
            return;
        
        button.ContentLayout =
            new Microsoft.Maui.Controls.Button.ButtonContentLayout(
                Microsoft.Maui.Controls.Button.ButtonContentLayout.ImagePosition.Right, button.ContentLayout.Spacing);
        handler.PlatformView.UpdateContentLayout(button);
    }

    private partial void AppendPropertyMapper()
    {
        PropertyMapper.Add(nameof(Microsoft.Maui.Controls.Button.BackgroundColor), OverrideMapBackgroundColor);
        PropertyMapper.Add(nameof(Microsoft.Maui.Controls.Button.Background), OverrideMapBackground);
    }

    private static async partial void MapAdditionalHitBoxSize(ButtonHandler handler, Button button)
    {
        // Small delay to wait for initialization of hitrect
        await Task.Delay(1);

        handler.PlatformView.SetAdditionalHitBoxSize(button, button.AdditionalHitBoxSize, handler.MauiContext!);
    }
}
