using Microsoft.Maui.Handlers;
using Button = Android.Widget.Button;

namespace DIPS.Mobile.UI.Components.Navigation.FloatingNavigationButton;

public partial class FloatingNavigationButtonHandler
{
    private static partial void MapIsClickable(FloatingNavigationButtonHandler handler,
        FloatingNavigationButton floatingNavigationButton)
    {
        if (handler.PlatformView is not global::Android.Views.View aView) return;
        if (handler.VirtualView is not FloatingNavigationButton fab) return;
        
        if (floatingNavigationButton.IsClickable)
        {
            aView.Clickable = true;
            aView.SetOnClickListener(new Effects.Touch.TouchPlatformEffect.ClickListener(() =>
            {
                _ = fab.Close();
            }));    
        }
        else
        {
            aView.Clickable = false;
        }
    }
}