using DIPS.Mobile.UI.Resources.LocalizedStrings.LocalizedStrings;
using Microsoft.Maui.Platform;
using UIKit;
using Colors = Microsoft.Maui.Graphics.Colors;

// ReSharper disable once CheckNamespace
namespace DIPS.Mobile.UI.Components.Alerting.Dialog;

public static partial class DialogService
{
    private static TaskCompletionSource<DialogAction>? m_taskCompletionSource;
    private static UIWindow Window { get; } = new() { BackgroundColor = Colors.Transparent.ToPlatform() };

    public static partial Task<DialogAction> ShowMessage(string title, string message, string actionTitle)
    {
        return Show(title, message, actionTitle);
    }

    public static partial Task<DialogAction> ShowConfirmationMessage(string title, string message, string closeTitle,
        string actionTitle)
    {
        return Show(title, message, actionTitle, closeTitle);
    }

    public static partial Task<DialogAction> ShowDestructiveConfirmationMessage(string title, string message,
        string closeTitle, string actionTitle)
    {
        return Show(title, message, actionTitle, closeTitle, true);
    }

    public async static partial Task Remove()
    {
        if (TryGetUiAlertViewController(out var viewController))
        {
            await viewController?.DismissViewControllerAsync(false)!;
            Window.Hidden = true;
            m_taskCompletionSource?.TrySetResult(DialogAction.Closed);   
        }
    }

    internal static bool TryGetUiAlertViewController(out UIViewController? viewController)
    {
        viewController = null;
        if (Window.RootViewController?.PresentedViewController is null)
        {
            return false;
        }

        viewController = Window.RootViewController;
        return true;

    }
    
    public static partial bool IsShowing()
    {
        return TryGetUiAlertViewController(out _);
    }

    private async static Task<DialogAction> Show(
        string title,
        string message,
        string actionButtonTitle,
        string? cancelButtonTitle = null,
        bool isDestructiveDialog = false)
    {
        await Remove();

        m_taskCompletionSource = new TaskCompletionSource<DialogAction>();

        var uiAlertController = UIAlertController.Create(title, message, UIAlertControllerStyle.Alert);
        if (cancelButtonTitle is not null)
        {
            uiAlertController.AddAction(
                UIAlertAction.Create(
                    cancelButtonTitle,
                    UIAlertActionStyle.Default,
                    _ =>
                    {
                        Window.Hidden = true;
                        m_taskCompletionSource?.SetResult(DialogAction.Closed);
                    }));
        }

        uiAlertController.AddAction(
            UIAlertAction.Create(
                actionButtonTitle,
                isDestructiveDialog ? UIAlertActionStyle.Destructive : UIAlertActionStyle.Default,
                _ =>
                {
                    Window.Hidden = true;
                    m_taskCompletionSource?.SetResult(DialogAction.TappedAction);
                }));
        
        Window.RootViewController = new UIViewController();
        if (Window.RootViewController.View == null)
        {
            return await m_taskCompletionSource.Task;
        }
        
        Window.RootViewController.View.BackgroundColor = Colors.Transparent.ToPlatform();
        Window.WindowLevel = UIWindowLevel.Alert + 1;
        Window.MakeKeyAndVisible();
        Window.RootViewController.PresentViewController(uiAlertController, true, () => { });

        return await m_taskCompletionSource.Task;
    }
}