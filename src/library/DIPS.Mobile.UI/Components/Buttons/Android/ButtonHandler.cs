using Android.Content.Res;
using Android.Graphics.Drawables;
using DIPS.Mobile.UI.Extensions.Android;
using DIPS.Mobile.UI.Internal.Logging;
using Google.Android.Material.Button;
using Microsoft.Maui.Controls.Platform;
using Microsoft.Maui.Platform;
using Colors = Microsoft.Maui.Graphics.Colors;

namespace DIPS.Mobile.UI.Components.Buttons;

public partial class ButtonHandler : Microsoft.Maui.Handlers.ButtonHandler
{
    static readonly ColorStateList TransparentColorStateList = Colors.Transparent.ToDefaultColorStateList();
    
    protected override MaterialButton CreatePlatformView()
    {
        return new Android.MaterialButton(Context)
        {
            Button = VirtualView as Button,
            IconGravity = MaterialButton.IconGravityTextStart,
            IconTintMode = global::Android.Graphics.PorterDuff.Mode.Add,
            IconTint = TransparentColorStateList,
            SoundEffectsEnabled = false
        };
    }

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

        UpdateForegroundRipple();
        
        // MaterialButton sets text to all caps as default
        platformView.SetAllCaps(false);
        
        platformView.SetMinHeight(0);
        platformView.Icon?.SetColorFilter((VirtualView as Button)!.ImageTintColor.ToPlatform(), FilterMode.SrcAtop);
    }

    private void UpdateForegroundRipple()
    {
        try
        {
            var rippleColor = Resources.Colors.Colors.GetColor(ColorName.color_icon_action);
            var ripple = new RippleDrawable(new Color(rippleColor.Red, rippleColor.Green, rippleColor.Blue, .2f).ToDefaultColorStateList(),
                null,
                PlatformView.Background);
            PlatformView.Foreground = ripple;
        }
        catch (Exception e)
        {
            DUILogService.LogError<ButtonHandler>("Failed to set ripple effect on button: " + e.Message);
        }
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
        PropertyMapper.Add(nameof(Microsoft.Maui.Controls.Button.CornerRadius), OverrideMapCornerRadius);
    }

    private void OverrideMapCornerRadius(ButtonHandler handler, Button button)
    {
        MapCornerRadius(handler, button);
        UpdateForegroundRipple();
    }

    private static partial void MapAdditionalHitBoxSize(ButtonHandler handler, Button button)
    {
        handler.PlatformView.SetAdditionalHitBoxSize(button.AdditionalHitBoxSize);
    }
}
