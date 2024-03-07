using Google.Android.Material.Snackbar;
using Microsoft.Maui.Handlers;

namespace DIPS.Mobile.UI.API.Tip;

public static partial class TipService
{
    public static partial void Show(string message, View anchoredView)
    {
        if (anchoredView.Handler is not ViewHandler viewHandler) return;
        if (viewHandler.PlatformView == null) return;
        var snackbar = Snackbar.Make(viewHandler.PlatformView, message, 5);
        //Configure it here
        snackbar.Show();
    }
}