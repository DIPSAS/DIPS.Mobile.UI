using Microsoft.Maui.Controls.Handlers.Compatibility;
using UIKit;
using Colors = DIPS.Mobile.UI.Resources.Colors.Colors;
using MauiShell = Microsoft.Maui.Controls.Shell;

namespace DIPS.Mobile.UI.Components.Pages;

internal class ModalNavigationRenderer : NavigationRenderer
{
    public override UIStatusBarStyle PreferredStatusBarStyle()
    {
        return GetModalStatusBarStyle() ?? base.PreferredStatusBarStyle();
    }

    public override UIViewController? ChildViewControllerForStatusBarStyle()
    {
        return GetModalStatusBarStyle() is null ? base.ChildViewControllerForStatusBarStyle() : null;
    }

    private UIStatusBarStyle? GetModalStatusBarStyle()
    {
        if (Element is not NavigationPage navigationPage || navigationPage.CurrentPage is not ContentPage contentPage)
            return null;

        if (!IsModalNavigationPage(navigationPage))
            return null;

        var backgroundColor = contentPage.NavigationBarColor
                              ?? navigationPage.BarBackgroundColor
                              ?? GetShellBackgroundColor(contentPage)
                              ?? GetShellBackgroundColor(navigationPage)
                              ?? GetShellBackgroundColor(MauiShell.Current)
                              ?? Colors.GetColor(Shell.Shell.BackgroundColorName);

        return backgroundColor.GetLuminosity() > 0.5 ? UIStatusBarStyle.DarkContent : UIStatusBarStyle.LightContent;
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
}