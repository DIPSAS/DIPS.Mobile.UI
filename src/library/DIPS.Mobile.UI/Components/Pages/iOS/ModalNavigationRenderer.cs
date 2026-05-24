using Microsoft.Maui.Controls.Handlers.Compatibility;
using Microsoft.Maui.Controls.Platform;
using Microsoft.Maui.Dispatching;
using UIKit;

namespace DIPS.Mobile.UI.Components.Pages;

internal class ModalNavigationRenderer : NavigationRenderer
{
    protected override void OnElementChanged(VisualElementChangedEventArgs e)
    {
        if (e.OldElement is NavigationPage oldNavigationPage)
        {
            oldNavigationPage.Popped -= OnNavigationPagePopped;
        }

        base.OnElementChanged(e);

        if (e.NewElement is NavigationPage newNavigationPage)
        {
            newNavigationPage.Popped += OnNavigationPagePopped;
        }
    }

    protected override void Dispose(bool disposing)
    {
        if (disposing && Element is NavigationPage navigationPage)
        {
            navigationPage.Popped -= OnNavigationPagePopped;
        }

        base.Dispose(disposing);
    }

    public override UIStatusBarStyle PreferredStatusBarStyle()
    {
        return GetStatusBarStyle() ?? base.PreferredStatusBarStyle();
    }

    public override UIViewController? ChildViewControllerForStatusBarStyle()
    {
        return GetStatusBarStyle() is null ? base.ChildViewControllerForStatusBarStyle() : null;
    }

    private UIStatusBarStyle? GetStatusBarStyle()
    {
        if (Element is not NavigationPage navigationPage
            || !ContentPage.IsModalNavigationPage(navigationPage)
            || navigationPage.CurrentPage is not ContentPage contentPage)
            return null;

        if (GetNavigationBarColor(contentPage) is not { } backgroundColor)
            return null;

        return backgroundColor.GetLuminosity() > 0.5 ? UIStatusBarStyle.DarkContent : UIStatusBarStyle.LightContent;
    }

    private void RefreshStatusBarStyleAfterTransition()
    {
        if (Element?.Dispatcher is { } dispatcher)
        {
            dispatcher.Dispatch(SetNeedsStatusBarAppearanceUpdate);
            return;
        }

        SetNeedsStatusBarAppearanceUpdate();
    }

    private void OnNavigationPagePopped(object? sender, NavigationEventArgs e)
    {
        RefreshStatusBarStyleAfterTransition();
    }

    private static Color? GetNavigationBarColor(BindableObject bindableObject)
    {
        return (Color?)bindableObject.GetValue(NavigationPage.BarBackgroundColorProperty);
    }
}