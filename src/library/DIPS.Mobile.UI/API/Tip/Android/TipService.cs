using Android.Content.Res;
using DIPS.Mobile.UI.Resources.LocalizedStrings.LocalizedStrings;
using Google.Android.Material.Snackbar;
using Microsoft.Maui.Handlers;
using Microsoft.Maui.Platform;

namespace DIPS.Mobile.UI.API.Tip;

public static partial class TipService
{
    public static async partial void Show(string message, View anchoredView, int durationInMilliseconds)
    {
        if (anchoredView.Handler is not ViewHandler viewHandler) return;
        if (viewHandler.PlatformView == null) return;
        Show(message, viewHandler.PlatformView, durationInMilliseconds);
    }

public static void Show(string message, Android.Views.View anchorView, int durationInMilliseconds)
    {
        if (durationInMilliseconds <= 0)
        {
            durationInMilliseconds = BaseTransientBottomBar.LengthIndefinite;
        }
        
        var snackBar = Snackbar.Make(anchorView, message, durationInMilliseconds);
        snackBar.SetAnchorView(anchorView);
        
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