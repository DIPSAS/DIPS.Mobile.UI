using DIPS.Mobile.UI.API.Library;
using DIPS.Mobile.UI.Components.Alerting.Dialog.Android;
using DIPS.Mobile.UI.Internal.Logging;
using DIPS.Mobile.UI.Resources.LocalizedStrings.LocalizedStrings;
using Java.Lang;
using Microsoft.Maui.Platform;
using Exception = System.Exception;

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
        try
        {
            RemovePreviousDialog();
        }
        catch (IllegalStateException)
        {
            // ignored
        }
        catch (Exception e)
        {
            DUILogService.LogError<AlertDialog>(e.Message);
            throw;
        }

        return Task.CompletedTask;
    }
    
    public static partial bool IsShowing()
    {
        return TryGetAlertDialog(out _);

    }

    internal static bool TryGetAlertDialog(out AlertDialog? alertDialog)
    {
        var fragmentManager = DUI.GetCurrentMauiContext!.Context!.GetFragmentManager();
        var previous = fragmentManager!.FindFragmentByTag(DialogTag);
        alertDialog = null;
        if (previous is not AlertDialog theDialog)
        {
            return false;
        }

        alertDialog = theDialog;
        return true;

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
        catch (IllegalStateException)
        {
            taskCompletionSource.TrySetResult(DialogAction.Closed);
        }
        catch (Exception e)
        {
            DUILogService.LogError<AlertDialog>(e.Message);
            taskCompletionSource.TrySetResult(DialogAction.Closed);
            throw;
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