using Microsoft.Maui.Handlers;
using UIKit;

namespace DIPS.Mobile.UI.Components.Navigation.FloatingNavigationButton;

public partial class FloatingNavigationButtonHandler
{
    private static partial void MapIsClickable(FloatingNavigationButtonHandler handler,
        FloatingNavigationButton floatingNavigationButton)
    {
        if (handler.PlatformView is not UIView uiView) return;
        if (handler.VirtualView is not FloatingNavigationButton fab) return;
        
        if (floatingNavigationButton.IsClickable)
        {
            uiView.UserInteractionEnabled = true;
            uiView.GestureRecognizers = new UIGestureRecognizer[]
            {
                new UITapGestureRecognizer(() =>
                {
                    _ = fab.Close();
                })
            };
        }
        else
        {
            uiView.UserInteractionEnabled = false;
            uiView.GestureRecognizers = Array.Empty<UIGestureRecognizer>();
        }
    }
}