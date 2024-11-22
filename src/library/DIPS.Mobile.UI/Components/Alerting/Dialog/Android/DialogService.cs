using DIPS.Mobile.UI.API.Library;
using DIPS.Mobile.UI.Components.Alerting.Dialog.Android;
using DIPS.Mobile.UI.Resources.LocalizedStrings.LocalizedStrings;
using Microsoft.Maui.Platform;

// ReSharper disable once CheckNamespace
namespace DIPS.Mobile.UI.Components.Alerting.Dialog;

public static partial class DialogService
{
    private const string DialogTag = "MessageAlertTag";
    
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

    public static partial Task Remove()
    {
        RemovePreviousDialog();
        return Task.CompletedTask;
    }

    private static Task<DialogAction> Show(
        string title,
        string message,
        string actionButtonTitle,
        string? cancelButtonTitle = null,
        bool isDestructiveDialog = false)
    {
        var taskCompletionSource = new TaskCompletionSource<DialogAction>();
        
        try
        {
            RemovePreviousDialog();

            var alertDialog = new AlertDialog(title,
                message,
                actionButtonTitle,
                cancelButtonTitle,
                () => taskCompletionSource.TrySetResult(DialogAction.TappedAction),
                () => taskCompletionSource.TrySetResult(DialogAction.Closed),
                isDestructiveDialog);
            alertDialog.Show(DUI.GetCurrentMauiContext!.Context!.GetFragmentManager()!, DialogTag);
        }
        catch
        {
            taskCompletionSource.TrySetResult(DialogAction.Closed);
        }

        return taskCompletionSource.Task;
    }

    private static void RemovePreviousDialog()
    {
        var fragmentManager = DUI.GetCurrentMauiContext!.Context!.GetFragmentManager();
        var previous = fragmentManager!.FindFragmentByTag(DialogTag);
        if (previous is AlertDialog alertDialog)
        {   
            alertDialog.Dismiss();
        }
    }
}