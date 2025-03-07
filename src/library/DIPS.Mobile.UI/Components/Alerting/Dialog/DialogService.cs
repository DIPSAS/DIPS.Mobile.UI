namespace DIPS.Mobile.UI.Components.Alerting.Dialog;

public static partial class DialogService
{
    public static partial Task<InputDialogAction> ShowInputDialog(Action<IInputDialogConfigurator> configurator);
    
    /// <summary>
    ///     Attempt to show a message with a suggestion and one button.
    /// </summary>
    /// <param name="title">Title of the alert</param>
    /// <param name="message">Message shown in alert</param>
    /// <param name="actionTitle">Title of the button</param>
    /// <returns>
    ///     <see cref="DialogAction.TappedAction" />
    ///     <see cref="DialogAction.Closed" />
    /// </returns>
    public static partial Task<DialogAction> ShowMessage(string title, string message, string actionTitle);

    /// <summary>
    ///     Show a message with a suggestion and two buttons.
    /// </summary>
    /// <param name="title">Title of the alert</param>
    /// <param name="message">Message shown in alert</param>
    /// <param name="closeTitle">Title of the cancel button</param>
    /// <param name="actionTitle">Title of the action button</param>
    /// <returns>
    ///     <see cref="DialogAction.TappedAction" />
    ///     <see cref="DialogAction.Closed" />
    /// </returns>
    public static partial Task<DialogAction> ShowConfirmationMessage(string title, string message, string closeTitle, string actionTitle);

    /// <summary>
    ///     Show a message with a suggestion and two buttons where the action button is a destructive button.
    /// </summary>
    /// <param name="title">Title of the alert</param>
    /// <param name="message">Message shown in alert</param>
    /// <param name="closeTitle">Title of the cancel button</param>
    /// <param name="actionTitle">Title of the action button</param>
    /// <returns>
    ///     <see cref="DialogAction.TappedAction" />
    ///     <see cref="DialogAction.Closed" />
    /// </returns>
    public static partial Task<DialogAction> ShowDestructiveConfirmationMessage(string title, string message, string closeTitle, string actionTitle);

    /// <summary>
    ///     Removes the currently showing alert.
    /// </summary>
    public static partial Task Remove();
    
    /// <summary>
    /// Determines if any dialog is showing.
    /// </summary>
    /// <returns>true/false depending on if its showing.</returns>
    public static partial bool IsShowing();
    
    internal static bool ActionEnabledState(IInputDialog inputDialog)
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
}

public class InputDialogAction
{
    public DialogAction DialogAction { get; set; }
    public IEnumerable<IDialogInputField> DialogInputs { get; set; }
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
