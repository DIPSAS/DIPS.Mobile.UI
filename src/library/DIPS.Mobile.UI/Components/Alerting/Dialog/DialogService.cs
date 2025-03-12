namespace DIPS.Mobile.UI.Components.Alerting.Dialog;

public static partial class DialogService
{
    
    /// <summary>
    /// Display an input dialog and use the <see cref="IInputDialogConfigurator"/> to configure it.
    /// This is used to get a simple input from the user.
    /// <example>
    /// For example:
    /// <code>
    ///  var someInput = new StringDialogInputField(placeholder: "Type here");
    ///  var result = await dialogService.ShowInputDialog(configurator =>
    /// {
    ///     configurator
    ///         .AddTitle("Title")
    ///         .AddDescription("This is some description")
    ///         .AddInputField(someInput);
    /// });
    ///
    /// if(result.DialogAction == DialogAction.TappedAction)
    /// {
    ///     // Use someInput.Value (or result.DialogInputs[0].Value)
    /// }
    /// </code>
    /// </example>
    /// </summary>
    /// <param name="configurator"></param>
    /// <returns><see cref="InputDialogAction"/></returns>
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

    /// <summary>
    /// Show a dialog and use the <see cref="IDialogConfigurator"/> to configure it.
    /// <example>
    /// For example:
    /// <code>
    /// var result = await dialogService.ShowMessage(configurator =>
    /// {
    ///     configurator
    ///         .AddTitle("Title")
    ///         .AddDescription("This is some description");
    /// });
    /// </code>
    /// </example>
    /// </summary>
    /// <param name="configurator"></param>
    /// <returns>
    ///     <see cref="DialogAction.TappedAction" />
    /// ,
    ///     <see cref="DialogAction.Closed" />
    /// </returns>
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
