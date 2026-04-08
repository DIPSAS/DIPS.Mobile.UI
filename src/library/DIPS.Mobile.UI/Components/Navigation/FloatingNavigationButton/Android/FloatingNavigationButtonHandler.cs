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
        
        aView.Click -= handler.OnNativeViewClick;
        
        if (floatingNavigationButton.IsClickable)
        {
            aView.Clickable = true;
            aView.Click += handler.OnNativeViewClick;
        }
        else
        {
            aView.Clickable = false;
        }
    }
    
    private void OnNativeViewClick(object? sender, EventArgs e)
    {
        if (VirtualView is FloatingNavigationButton fab)
        {
            _ = fab.Close();
        }
    }
}