using DIPS.Mobile.UI.API.Library;
using DIPS.Mobile.UI.Components.Alerting.Dialog.Android;
using DIPS.Mobile.UI.Internal.Logging;
using Java.Lang;
using Microsoft.Maui.Platform;
using Exception = System.Exception;

// ReSharper disable once CheckNamespace
namespace DIPS.Mobile.UI.Components.Alerting.Dialog;

public static partial class DialogService
{
    private const string DialogTag = "DUI.DialogFragment";

    public static partial async Task<InputDialogAction> ShowInputDialog(
        Action<IInputDialogConfigurator> configurator)
    {
        var taskCompletionSource = new TaskCompletionSource<InputDialogAction>();
        var inputDialogConfigurator = new InputDialogConfigurator();
        try
        {
            RemovePreviousDialog();
            configurator.Invoke(inputDialogConfigurator);
            
            IInputDialog inputDialogConfig = inputDialogConfigurator;

            var inputDialog = new DialogFragment(
                inputDialogConfigurator,
                () =>
                {
                    OnActionButtonTapped(inputDialogConfig, taskCompletionSource);
                },
                () =>
                {
                    OnCancelButtonTapped(inputDialogConfig, taskCompletionSource);
                });
             
            inputDialog.Show(DUI.GetCurrentMauiContext?.Context?.GetFragmentManager()!, DialogTag);
        }
        catch(IllegalStateException)
        {
            taskCompletionSource.TrySetResult(new InputDialogAction
            {
                DialogAction = DialogAction.Closed,
                DialogInputs = []
            });
        }
        catch (Exception e)
        {
            DUILogService.LogError<DialogFragment>(e.Message);
            taskCompletionSource.TrySetResult(new InputDialogAction
            {
                DialogAction = DialogAction.Closed,
                DialogInputs = []
            });
            throw;
        }

        return await taskCompletionSource.Task;
    }

    public static partial Task<DialogAction> ShowMessage(Action<IDialogConfigurator> configurator)
    {
        var dialogConfigurator = new DialogConfigurator();
        configurator.Invoke(dialogConfigurator);
        
        IDialog dialog = dialogConfigurator;
        
        var taskCompletionSource = new TaskCompletionSource<DialogAction>();

        try
        {
            RemovePreviousDialog();

            var alertDialog = new DialogFragment(dialog,
                () => taskCompletionSource.TrySetResult(DialogAction.TappedAction),
                () => taskCompletionSource.TrySetResult(DialogAction.Closed));
            alertDialog.Show(DUI.GetCurrentMauiContext!.Context!.GetFragmentManager()!, DialogTag);
        }
        catch (IllegalStateException)
        {
            taskCompletionSource.TrySetResult(DialogAction.Closed);
        }
        catch (Exception e)
        {
            DUILogService.LogError<DialogFragment>(e.Message);
            taskCompletionSource.TrySetResult(DialogAction.Closed);
            throw;
        }

        return taskCompletionSource.Task;
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
            DUILogService.LogError<DialogFragment>(e.Message);
            throw;
        }

        return Task.CompletedTask;
    }
    
    public static partial bool IsShowing()
    {
        return TryGetAlertDialog(out _);

    }

    internal static bool TryGetAlertDialog(out DialogFragment? alertDialog)
    {
        var fragmentManager = DUI.GetCurrentMauiContext!.Context!.GetFragmentManager();
        var previous = fragmentManager!.FindFragmentByTag(DialogTag);
        alertDialog = null;
        if (previous is not DialogFragment theDialog)
        {
            return false;
        }

        alertDialog = theDialog;
        return true;
    }

    [Obsolete("Remove when no references")]
    private static Task<DialogAction> Show(
        string title,
        string message,
        string actionButtonTitle,
        string? cancelButtonTitle = null,
        bool isDestructiveDialog = false)
    {
        return ShowMessage(config =>
        {
            config.SetTitle(title);
            config.SetDescription(message);
            config.SetActionTitle(actionButtonTitle);
            if(cancelButtonTitle is not null)
                config.SetCancelTitle(cancelButtonTitle);
            if(isDestructiveDialog)
                config.SetDestructive();
        });
    }

    private static void RemovePreviousDialog()
    {
        var fragmentManager = DUI.GetCurrentMauiContext!.Context!.GetFragmentManager();
        var previous = fragmentManager!.FindFragmentByTag(DialogTag);
        if (previous is DialogFragment alertDialog)
        {
            alertDialog.Dismiss();
        }
    }
}