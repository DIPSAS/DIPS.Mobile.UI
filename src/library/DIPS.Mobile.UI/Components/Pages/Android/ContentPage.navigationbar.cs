using DIPS.Mobile.UI.API.Library.Android;
using Colors = DIPS.Mobile.UI.Resources.Colors.Colors;

namespace DIPS.Mobile.UI.Components.Pages;

public partial class ContentPage
{
    private partial void RefreshStatusBarTextOnPlatform(NavigationPage navigationPage)
    {
        StatusBarHandler.TrySetStatusBarColor(this, GetEffectiveStatusBarColor());
    }

    internal Color GetEffectiveStatusBarColor()
    {
        if (StatusBarColor is { } explicitColor)
            return explicitColor;
        if (Parent is NavigationPage navPage)
            return navPage.BarBackgroundColor;
        return Colors.GetColor(Shell.Shell.BackgroundColorName);
    }
}