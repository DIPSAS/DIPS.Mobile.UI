using DIPS.Mobile.UI.Resources.LocalizedStrings.LocalizedStrings;

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
    
    [Obsolete]
    private async static Task<DialogAction> Show(
        string title,
        string message,
        string actionButtonTitle,
        string? cancelButtonTitle = null,
        bool isDestructiveDialog = false)
    {
        IDialog dialog = new DialogConfigurator();
        dialog.Title = title;
        dialog.Description = message;
        dialog.ActionTitle = actionButtonTitle;
        dialog.CancelTitle = cancelButtonTitle ?? DUILocalizedStrings.Cancel;
        dialog.IsDestructive = isDestructiveDialog;
        
        return await ShowMessage(configurator =>
        {
            configurator.SetTitle(dialog.Title);
            configurator.SetDescription(dialog.Description);
            configurator.SetActionTitle(dialog.ActionTitle);
            if(cancelButtonTitle is not null)
                configurator.SetCancelTitle(dialog.CancelTitle);
            if(isDestructiveDialog)
                configurator.SetDestructive();
        });
    }
}