using CoreGraphics;
using DIPS.Mobile.UI.API.Library;
using Microsoft.Maui.Platform;
using UIKit;

// ReSharper disable once CheckNamespace
namespace DIPS.Mobile.UI.Components.FloatingActionButton.FloatingActionButtonMenu;

public static partial class FloatingActionButtonMenuService
{
    public static partial void Create()
    {
        CreateFab();
    }

    private static async void CreateFab()
    {
        // Small delay to wait for iOS to initialize KeyWindow
        await Task.Delay(10);
        var fab = new FloatingActionButtonMenu();
        if (OperatingSystem.IsIOSVersionAtLeast(14, 1))
        {
            UIApplication.SharedApplication.Windows.First().RootViewController!.View!.AddSubview(fab.ToPlatform(DUI.GetCurrentMauiContext!));
        }
    }
    
    public static partial void Hide()
    {
        
    }
    
}