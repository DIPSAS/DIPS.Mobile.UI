namespace DIPS.Mobile.UI.Components.Alerting.Dialog;

public partial class DialogService
{
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
    [Obsolete("Will be removed, use ShowMessage(Action<IDialogConfigurator> configurator) instead.")]
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
    [Obsolete("Will be removed, use ShowMessage(Action<IDialogConfigurator> configurator) instead.")]
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
    [Obsolete("Will be removed, use ShowMessage(Action<IDialogConfigurator> configurator) instead.")]
    public static partial Task<DialogAction> ShowDestructiveConfirmationMessage(string title, string message, string closeTitle, string actionTitle);
}