// ReSharper disable once CheckNamespace
namespace DIPS.Mobile.UI.Components.Alerting.Dialog;

public static partial class DialogService
{
    public static partial Task<InputDialogAction> ShowInputDialog(Action<IInputDialogConfigurator> configurator)
    {
        return Task.FromResult(new InputDialogAction
        {
            DialogAction = DialogAction.TappedAction,
            DialogInputs = {  }
        });
    }
    
    public static partial Task<DialogAction> ShowMessage(string title, string message, string actionTitle)
    {
        return Task.FromResult(DialogAction.TappedAction);
    }

    public static partial Task<DialogAction> ShowConfirmationMessage(string title, string message, string closeTitle,
        string actionTitle)
    {
        return Task.FromResult(DialogAction.TappedAction);
    }

    public static partial Task<DialogAction> ShowDestructiveConfirmationMessage(string title, string message,
        string closeTitle, string actionTitle)
    {
        return Task.FromResult(DialogAction.TappedAction);
    }

    public static partial Task<DialogAction> ShowMessage(Action<IDialogConfigurator> configurator)
    {
        return Task.FromResult(DialogAction.TappedAction);
    }
    
    public static partial Task Remove()
    {
        return Task.FromResult(DialogAction.TappedAction);
    }
    
    public static partial bool IsShowing()
    {
        return false;
    }

}