using UIKit;

// ReSharper disable once CheckNamespace
namespace DIPS.Mobile.UI.API.Library;

public static partial class DUI
{

    public static Action? OnRemoveViewsLocatedOnTopOfPage;
    
    private static partial void RemovePlatformSpecificViewsLocatedOnTopOfPage()
    {
        OnRemoveViewsLocatedOnTopOfPage?.Invoke();
    }
    
    private static async partial void PlatformInit()
    {
        await Task.Delay(10);
        var appDelegate = UIApplication.SharedApplication.Delegate as MauiUIApplicationDelegate;
        RootController = appDelegate.Window.RootViewController!.View!;
    }
    
    public static UIView? RootController { get; private set; }

}