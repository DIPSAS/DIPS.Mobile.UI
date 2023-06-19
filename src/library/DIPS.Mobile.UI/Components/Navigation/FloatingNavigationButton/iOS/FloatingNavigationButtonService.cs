using CoreGraphics;
using DIPS.Mobile.UI.API.Library;
using Microsoft.Maui.Platform;
using UIKit;

// ReSharper disable once CheckNamespace
namespace DIPS.Mobile.UI.Components.Navigation.FloatingNavigationButton;

public static partial class FloatingNavigationButtonService
{
    private static async partial void AttachToRootWindow(FloatingNavigationButton fab)
    {
        if (OperatingSystem.IsIOSVersionAtLeast(14, 1))
        {
            var rootView = DUI.RootController;
            if (rootView is null)
            {
                await Task.Delay(10);
                rootView = DUI.RootController;
            }
            fab.HeightRequest = rootView.Frame.Height;
            fab.WidthRequest = rootView.Frame.Width;
            rootView.AddSubview(fab.ToPlatform(DUI.GetCurrentMauiContext!));
            
        }
    }
}