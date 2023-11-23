using UIKit;

// ReSharper disable once CheckNamespace
namespace DIPS.Mobile.UI.Components.Alerting.Dialog;

public static partial class DialogService
{
    private static TaskCompletionSource<DialogAction>? m_taskCompletionSource;
    private static UIWindow Window => UIApplication.SharedApplication.KeyWindow!;
    
    private static UIWindow KeyWindow { get; set; }

    private static UIAlertController? m_currentAlertController;
    
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
        if (m_currentAlertController is not null)
        {
            await m_currentAlertController.DismissViewControllerAsync(false)!;
            m_currentAlertController = null;
            m_taskCompletionSource?.TrySetResult(DialogAction.Closed);
        }
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

        m_currentAlertController = UIAlertController.Create(title, message, UIAlertControllerStyle.Alert);
        if (cancelButtonTitle is not null)
        {
            m_currentAlertController.AddAction(
                UIAlertAction.Create(
                    cancelButtonTitle,
                    UIAlertActionStyle.Default,
                    _ =>
                    {
                        m_currentAlertController = null;
                        m_taskCompletionSource?.SetResult(DialogAction.Closed);
                    }));
        }

        m_currentAlertController.AddAction(
            UIAlertAction.Create(
                actionButtonTitle,
                isDestructiveDialog ? UIAlertActionStyle.Destructive : UIAlertActionStyle.Default,
                _ =>
                {
                    m_currentAlertController = null;
                    m_taskCompletionSource?.SetResult(DialogAction.TappedAction);
                }));
        
        // Window.RootViewController = new UIViewController();
        // if (Window.RootViewController.View == null)
        // {
        //     return await m_taskCompletionSource.Task;
        // }
        //
        // Window.RootViewController.View.BackgroundColor = Colors.Transparent.ToPlatform();
        // Window.WindowLevel = UIWindowLevel.Alert + 1;
        // Window.MakeKeyAndVisible();
        Window.RootViewController!.PresentViewController(m_currentAlertController, true, () => { });

        return await m_taskCompletionSource.Task;
    }
}