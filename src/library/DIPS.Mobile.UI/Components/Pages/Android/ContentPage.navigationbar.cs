using Android.Text;
using Android.Widget;
using AndroidX.Fragment.App;
using DIPS.Mobile.UI.API.Library.Android;
using Google.Android.Material.AppBar;
using Microsoft.Maui.Platform;
using Colors = DIPS.Mobile.UI.Resources.Colors.Colors;
using DuiShell = DIPS.Mobile.UI.Components.Shell.Shell;
using MauiShell = Microsoft.Maui.Controls.Shell;

namespace DIPS.Mobile.UI.Components.Pages;

public partial class ContentPage
{
    private partial void ApplyNavigationBarAppearanceOnPlatform()
    {
        var navigationPage = GetParentNavigationPage();
        if (navigationPage is null || !IsModalNavigationPage(navigationPage))
            return;

        var dialogFragment = StatusBarHandler.TryGetDialogFragmentForPage(this);
        if (dialogFragment is null)
            return;

        var materialToolbar = GetModalMaterialToolbar(dialogFragment);
        if (materialToolbar is null)
            return;

        var backgroundColor = NavigationBarColor
                              ?? navigationPage.BarBackgroundColor
                      ?? GetShellBackgroundColor(this)
                      ?? GetShellBackgroundColor(navigationPage)
                      ?? GetShellBackgroundColor(MauiShell.Current)
                              ?? Colors.GetColor(DuiShell.BackgroundColorName);
        var textColor = NavigationBarTextColor
                ?? navigationPage.BarTextColor
                ?? GetShellTitleColor(this)
                ?? GetShellTitleColor(navigationPage)
                ?? GetShellTitleColor(MauiShell.Current)
                ?? Colors.GetColor(DuiShell.TitleTextColorName);

        materialToolbar.SetBackgroundColor(backgroundColor.ToPlatform());
        materialToolbar.SetTitleTextColor(textColor.ToPlatform());
        materialToolbar.NavigationIcon?.SetTint(textColor.ToPlatform());
        materialToolbar.OverflowIcon?.SetTint(textColor.ToPlatform());
        StatusBarHandler.TrySetStatusBarColor(this, backgroundColor);

        const float shadowDp = 6f;
        var shadowPx = materialToolbar.Context?.Resources?.DisplayMetrics?.Density * shadowDp ?? 0;
        materialToolbar.Elevation = shadowPx;

        var textColorStateList = textColor.ToDefaultColorStateList();
        for (var i = 0; i < materialToolbar.Menu?.Size(); i++)
        {
            var item = materialToolbar.Menu.GetItem(i);
            item?.SetIconTintList(textColorStateList);

            if (item?.TitleFormatted is null)
                continue;

            var span = new SpannableString(item.TitleFormatted);
            span.SetSpan(new global::Android.Text.Style.ForegroundColorSpan(textColor.ToPlatform()), 0, span.Length(), 0);
            item.SetTitle(span);
        }
    }

    private static MaterialToolbar? GetModalMaterialToolbar(DialogFragment dialogFragment)
    {
        var linearLayout = dialogFragment.Dialog?.Window?.FindViewById<LinearLayout>(_Microsoft.Android.Resource.Designer.Resource.Id.navigationlayout_appbar);
        return linearLayout?.GetChildAt(0) as MaterialToolbar;
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