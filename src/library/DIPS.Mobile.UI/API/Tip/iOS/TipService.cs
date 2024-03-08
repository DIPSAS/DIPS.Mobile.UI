using Microsoft.Maui.Handlers;
using UIKit;

namespace DIPS.Mobile.UI.API.Tip;

public static partial class TipService
{
    public static async partial void Show(string message, View anchoredView, int durationInMilliseconds)
    {
        if (anchoredView.Handler is not ViewHandler viewHandler) return;
        if (viewHandler.PlatformView is null) return;
        if (anchoredView.Window?.Handler is not WindowHandler windowHandler) return;
        if (windowHandler.PlatformView.RootViewController is not { } rootViewController) return;

        var tipUiViewController = new TipUIViewController(message, viewHandler.PlatformView);
        tipUiViewController.SetupPopover();

        await rootViewController.PresentViewControllerAsync(
            tipUiViewController,
            true);
        
        if (durationInMilliseconds > 0)
        {
            _ = new Timer(_ =>
            {
                MainThread.InvokeOnMainThreadAsync(() => tipUiViewController.Close());
            }, null, TimeSpan.FromMilliseconds(durationInMilliseconds), TimeSpan.FromMilliseconds(durationInMilliseconds));
        }
    }
}