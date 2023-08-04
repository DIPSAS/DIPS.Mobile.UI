using Android.Graphics.Drawables;
using Android.Util;
using Android.Views;
using AndroidX.Core.Graphics;
using AndroidX.Core.Widget;
using DIPS.Mobile.UI.API.Library;
using DIPS.Mobile.UI.Extensions.Android;
using Google.Android.Material.Button;
using Microsoft.Maui.Platform;

namespace DIPS.Mobile.UI.Components.Buttons;

public partial class ButtonHandler : Microsoft.Maui.Handlers.ButtonHandler
{
    private void MapBackgroundColor(ButtonHandler handler, Button button)
    {
        handler.PlatformView.UpdateBackground(button);
        
        handler.PlatformView.StateListAnimator = null; //To remove shadows properly, bug in Android
        
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