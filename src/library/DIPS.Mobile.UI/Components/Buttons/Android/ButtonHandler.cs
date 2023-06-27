using Android.Util;
using Android.Views;
using DIPS.Mobile.UI.API.Library;
using DIPS.Mobile.UI.Extensions.Android;
using Google.Android.Material.Button;
using Microsoft.Maui.Platform;
using View = Android.Views.View;
using ARect = Android.Graphics.Rect;

namespace DIPS.Mobile.UI.Components.Buttons;

public partial class ButtonHandler : Microsoft.Maui.Handlers.ButtonHandler
{
    protected override MaterialButton CreatePlatformView()
    {
        return base.CreatePlatformView();
    }

    protected override void ConnectHandler(MaterialButton platformView)
    {
        base.ConnectHandler(platformView);

    }

    private void MapBackgroundColor(ButtonHandler handler, Button button)
    {
        handler.PlatformView.UpdateBackground(button);
        
        // Bug in Android where setting the button to transparent does not remove its shadows, setting StateListAnimator to null removes shadows 
        if(Equals(button.Background, Brush.Transparent) || Equals(button.BackgroundColor, Microsoft.Maui.Graphics.Colors.Transparent))
            handler.PlatformView.StateListAnimator = null;
    }

    
    private partial void AppendPropertyMapper()
    {
        PropertyMapper.Add(nameof(Button.BackgroundColorProperty), MapBackgroundColor);
    }
    
    private static async partial void MapAdditionalHitBoxSize(ButtonHandler handler, Button button)
    {
        // Small delay to wait for initialization of hitrect
        await Task.Delay(1);
        
        handler.PlatformView.SetAdditionalHitBoxSize(button, button.AdditionalHitBoxSize, handler.MauiContext!);
    }
}