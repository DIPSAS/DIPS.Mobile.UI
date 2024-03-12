using DIPS.Mobile.UI.Components.Alerting.Dialog;
using Microsoft.Maui.Handlers;
using UIKit;

namespace DIPS.Mobile.UI.API.Tip;

public static partial class TipService
{
    public static async partial void Show(string message, View anchoredView, int durationInMilliseconds)
    {
        if (TryGetPlatformViewAndRootViewController(anchoredView, out var viewTuple))
        {
            await Show(message, durationInMilliseconds, viewTuple.Item1,
                viewTuple.Item2);
        }
    }
    
    public static async Task Show(string message, int durationInMilliseconds, UIView uiView,
        UIViewController rootViewController,
        UIPopoverArrowDirection permittedArrowDirection = UIPopoverArrowDirection.Any)
    {
        await SetupAndShow(message, durationInMilliseconds, rootViewController, tip =>
        {
            tip.SetupPopover(uiView, permittedArrowDirection: permittedArrowDirection);
        });
    }

    public static bool TryGetPlatformViewAndRootViewController(VisualElement anchoredView,
        out Tuple<UIView, UIViewController> tuple)
    {
        tuple = new Tuple<UIView, UIViewController>(new UIView(), new UIViewController());
        if (anchoredView.Handler is not ViewHandler viewHandler) return false;
        if (viewHandler.PlatformView is null) return false;
        if (anchoredView.Window?.Handler is not WindowHandler windowHandler) return false;
        if (windowHandler.PlatformView.RootViewController is not { } rootViewController) return false;

        var uiView = viewHandler.PlatformView;
        if ((anchoredView is Slider))
        {
            uiView = uiView.Subviews.FirstOrDefault()?.Subviews.LastOrDefault() ?? viewHandler.PlatformView;
        }

        tuple = new Tuple<UIView, UIViewController>(uiView, rootViewController);
        return true;
    }
    

    private static async Task SetupAndShow(string message, int durationInMilliseconds,
        UIViewController rootViewController, Action<TipUIViewController> setup)
    {
        var tipUiViewController = new TipUIViewController(message);
        setup.Invoke(tipUiViewController);

        
        await rootViewController.PresentViewControllerAsync(
            tipUiViewController,
            true);

        if (durationInMilliseconds > 0)
        {
            _ = new Timer(_ =>
                {
                    MainThread.InvokeOnMainThreadAsync(() => tipUiViewController.Close());
                }, null, TimeSpan.FromMilliseconds(durationInMilliseconds),
                TimeSpan.FromMilliseconds(durationInMilliseconds));
        }
    }
}