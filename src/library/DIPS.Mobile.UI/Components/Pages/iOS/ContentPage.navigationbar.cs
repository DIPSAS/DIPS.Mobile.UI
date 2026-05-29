using Microsoft.Maui.Platform;
using UIKit;

namespace DIPS.Mobile.UI.Components.Pages;

public partial class ContentPage
{
    private partial void RefreshStatusBarTextOnPlatform(NavigationPage navigationPage)
    {
        if (navigationPage.Handler?.MauiContext is not { } mauiContext)
            return;

        var viewController = navigationPage.ToUIViewController(mauiContext);
        if (viewController is UINavigationController navigationController)
        {
            navigationController.SetNeedsStatusBarAppearanceUpdate();
            return;
        }

        viewController.NavigationController?.SetNeedsStatusBarAppearanceUpdate();
    }
}