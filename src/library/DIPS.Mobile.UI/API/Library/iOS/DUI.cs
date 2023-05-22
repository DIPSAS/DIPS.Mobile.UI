using UIKit;

// ReSharper disable once CheckNamespace
namespace DIPS.Mobile.UI.API.Library;

public static partial class DUI
{

    private static partial void RemovePlatformSpecificViewsLocatedOnTopOfPage()
    {
        // Not yet implemented for iOS
        /*if (DatePickerService.IsOpen())
        { 
            _ = DateTimePickerService.Close();
        }*/
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