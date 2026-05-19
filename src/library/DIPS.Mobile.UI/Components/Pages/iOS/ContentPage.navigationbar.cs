using Microsoft.Maui.Platform;
using UIKit;
using Colors = DIPS.Mobile.UI.Resources.Colors.Colors;
using MauiShell = Microsoft.Maui.Controls.Shell;

namespace DIPS.Mobile.UI.Components.Pages;

public partial class ContentPage
{
    private partial void ApplyNavigationBarAppearanceOnPlatform()
    {
        if (Handler?.MauiContext is not { } mauiContext)
            return;

        var navigationPage = GetParentNavigationPage();
        if (navigationPage is null || !IsModalNavigationPage(navigationPage))
            return;

        var viewController = this.ToUIViewController(mauiContext);
        var navigationController = viewController.NavigationController ?? viewController as UINavigationController;
        if (navigationController is null)
            return;

        var backgroundColor = NavigationBarColor
                              ?? navigationPage.BarBackgroundColor
                              ?? GetShellBackgroundColor(this)
                              ?? GetShellBackgroundColor(navigationPage)
                              ?? GetShellBackgroundColor(MauiShell.Current)
                              ?? Colors.GetColor(Shell.Shell.BackgroundColorName);
        var textColor = NavigationBarTextColor
                        ?? navigationPage.BarTextColor
                        ?? GetShellTitleColor(this)
                        ?? GetShellTitleColor(navigationPage)
                        ?? GetShellTitleColor(MauiShell.Current)
                        ?? Colors.GetColor(Shell.Shell.TitleTextColorName);

        navigationPage.BarBackgroundColor = backgroundColor;
        navigationPage.BarTextColor = textColor;

        navigationController.NavigationBar.TintColor = textColor.ToPlatform();
        navigationController.SetNeedsStatusBarAppearanceUpdate();
    }

    private NavigationPage? GetParentNavigationPage()
    {
        var parent = Parent;
        while (parent is not null)
        {
            if (parent is NavigationPage navigationPage)
                return navigationPage;

            parent = parent.Parent;
        }

        return null;
    }

    private static bool IsModalNavigationPage(NavigationPage navigationPage)
    {
        return MauiShell.Current?.Navigation.ModalStack.Contains(navigationPage) == true
               || Application.Current?.Windows.Any(window => window.Page?.Navigation.ModalStack.Contains(navigationPage) == true) == true;
    }

    private static Color? GetShellBackgroundColor(BindableObject? bindableObject)
    {
        return bindableObject is null ? null : MauiShell.GetBackgroundColor(bindableObject);
    }

    private static Color? GetShellTitleColor(BindableObject? bindableObject)
    {
        return bindableObject is null ? null : MauiShell.GetTitleColor(bindableObject);
    }
}