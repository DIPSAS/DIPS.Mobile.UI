using UIKit;

namespace DIPS.Mobile.UI.API.Library.iOS;

public static class DUI
{

    public static void Init()
    {
    }

    /// <summary>
    /// Gets the current presented view controller
    /// </summary>
    public static UIViewController? CurrentViewController
    {
        get
        {
            var window = UIApplication.SharedApplication.KeyWindow;

            var currentViewController = window?.RootViewController;

            while (currentViewController?.PresentedViewController != null)
            {
                currentViewController = currentViewController.PresentedViewController;
            }

            return currentViewController;
        }
    }
        
}