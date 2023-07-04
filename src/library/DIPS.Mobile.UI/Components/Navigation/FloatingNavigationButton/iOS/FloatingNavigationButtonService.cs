using CoreGraphics;
using DIPS.Mobile.UI.API.Library;
using DIPS.Mobile.UI.Extensions.iOS;
using DIPS.Mobile.UI.Platforms.iOS;
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
            fab.HeightRequest = rootView!.Frame.Height;
            fab.WidthRequest = rootView.Frame.Width;
            var uiView = fab.ToPlatform(DUI.GetCurrentMauiContext!);
            uiView.Tag = FloatingNavigationButtonIdentifier;
            
            rootView.AddSubview(uiView);
            
        }
    }

    private static partial void PlatformRemove()
    {
        DUI.RootController!.RemoveUIViewChildWithTag(FloatingNavigationButtonIdentifier);
    }
}