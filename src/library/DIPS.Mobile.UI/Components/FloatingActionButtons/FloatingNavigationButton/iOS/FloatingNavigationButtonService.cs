using CoreGraphics;
using DIPS.Mobile.UI.API.Library;
using Microsoft.Maui.Platform;
using UIKit;

// ReSharper disable once CheckNamespace
namespace DIPS.Mobile.UI.Components.FloatingActionButtons.FloatingNavigationButton;

public static partial class FloatingNavigationButtonService
{
    internal static async partial void AttachToRootWindow(FloatingNavigationButton fab)
    {
        // Small delay to wait for iOS to initialize KeyWindow
        await Task.Delay(10);
        if (OperatingSystem.IsIOSVersionAtLeast(14, 1))
        {
            var appDelegate = UIApplication.SharedApplication.Delegate as MauiUIApplicationDelegate;
            var rootView = appDelegate.Window.RootViewController!.View!;
            fab.HeightRequest = rootView.Frame.Height;
            fab.WidthRequest = rootView.Frame.Width;
            rootView.AddSubview(fab.ToPlatform(DUI.GetCurrentMauiContext!));
        }

    }

    public static partial void Hide()
    {
        
    }
    
}