namespace DIPS.Mobile.UI.Components.Alerting.Dialog;

public partial class DialogService
{
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
}