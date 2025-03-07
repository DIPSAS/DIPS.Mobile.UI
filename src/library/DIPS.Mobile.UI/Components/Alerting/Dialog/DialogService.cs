namespace DIPS.Mobile.UI.Components.Alerting.Dialog;

public static partial class DialogService
{
    public static partial Task<InputDialogAction> ShowInputDialog(Action<IInputDialogConfigurator> configurator);

    /// <summary>
    ///     Removes the currently showing alert.
    /// </summary>
    public static partial Task Remove();
    
    /// <summary>
    /// Determines if any dialog is showing.
    /// </summary>
    /// <returns>true/false depending on if its showing.</returns>
    public static partial bool IsShowing();

    public static partial Task<DialogAction> ShowMessage(Action<IDialogConfigurator> configurator);
    
    internal static bool ActionEnabledState(IInputDialog? inputDialog)
    {
        return inputDialog.InputDialogEntryConfigurators.All(s =>
        {
            if(s is IDialogInputField<string> stringDialogInputField)
            {
                return !stringDialogInputField.MustBeSet || !string.IsNullOrEmpty(stringDialogInputField.Value);
            }

            return true;
        });
    }
    
    private static void OnCancelButtonTapped(IInputDialog inputDialogConfig, TaskCompletionSource<InputDialogAction> taskCompletionSource)
    {
        inputDialogConfig.InputDialogEntryConfigurators.ForEach(input =>
        {
            input.Reset();
        });

        taskCompletionSource.TrySetResult(new InputDialogAction
        {
            DialogAction = DialogAction.Closed,
            DialogInputs = inputDialogConfig.InputDialogEntryConfigurators
        });
    }
    
    private static void OnActionButtonTapped(IInputDialog inputDialogConfig, TaskCompletionSource<InputDialogAction> taskCompletionSource)
    {
        taskCompletionSource.TrySetResult(new InputDialogAction
        {
            DialogAction = DialogAction.TappedAction,
            DialogInputs = inputDialogConfig.InputDialogEntryConfigurators
        });
    }
}

public class InputDialogAction
{
    public DialogAction DialogAction { get; set; }
    public IEnumerable<IDialogInputField> DialogInputs { get; set; } = [];
}


/// <summary>
///     Types of results that can come from a user in a dialog context.
/// </summary>
public enum DialogAction
{
    /// <summary>
    ///     The dialog was closed due to people tapping the action button.
    /// </summary>
    TappedAction,
    /// <summary>
    ///     The dialog was closed by the user due to people tapping the close button or tapping outside of the dialog.
    /// </summary>
    Closed
}
