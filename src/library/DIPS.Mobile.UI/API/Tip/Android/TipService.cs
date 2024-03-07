using Android.Content.Res;
using DIPS.Mobile.UI.Resources.LocalizedStrings.LocalizedStrings;
using Google.Android.Material.Snackbar;
using Microsoft.Maui.Handlers;
using Microsoft.Maui.Platform;

namespace DIPS.Mobile.UI.API.Tip;

public static partial class TipService
{
    public static partial void Show(string message, View anchoredView)
    {
        if (anchoredView.Handler is not ViewHandler viewHandler) return;
        if (viewHandler.PlatformView == null) return;
        var snackBar = Snackbar.Make(viewHandler.PlatformView, message, BaseTransientBottomBar.LengthLong);
        snackBar.SetAnchorView(viewHandler.PlatformView);

        var actionColor = Resources.Colors.Colors.GetColor(ColorName.color_neutral_90);
        snackBar.SetBackgroundTintList(
            ColorStateList.ValueOf(Resources.Colors.Colors.GetColor(ColorName.color_system_white).ToPlatform()));
        snackBar.SetActionTextColor(
            ColorStateList.ValueOf((actionColor.ToPlatform())));
        snackBar.SetTextColor(
            ColorStateList.ValueOf((actionColor.ToPlatform())));
        snackBar.SetTextMaxLines(5);
        snackBar.SetAction(DUILocalizedStrings.Close, view =>
        {
            snackBar.Dismiss();
        });
        snackBar.Show();
    }
}