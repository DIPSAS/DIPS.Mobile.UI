using DIPS.Mobile.UI.Resources.LocalizedStrings.LocalizedStrings;

namespace DIPS.Mobile.UI.Components.Alerting.Dialog;

public class DialogConfigurator : IDialogConfigurator, IDialog
{
    public IDialogConfigurator SetTitle(string title)
    {
        ((IDialog)this).Title = title;
        return this;
    }

    public IDialogConfigurator SetDescription(string description)
    {
        ((IDialog)this).Description = description;
        return this;
    }

    public IDialogConfigurator SetActionTitle(string actionTitle)
    {
        ((IDialog)this).ActionTitle = actionTitle;
        return this;
    }

    public IDialogConfigurator SetCancelTitle(string? cancelTitle = null)
    {
        ((IDialog)this).CancelTitle = cancelTitle ?? DUILocalizedStrings.Cancel;
        return this;
    }

    public IDialogConfigurator SetDestructive()
    {
        ((IDialog)this).IsDestructive = true;
        return this;
    }

    bool IDialog.IsDestructive { get; set; }
    string IDialog.Title { get; set; } = string.Empty;
    string IDialog.Description { get; set; } = string.Empty;
    string IDialog.ActionTitle { get; set; } = "Ok";
    string? IDialog.CancelTitle { get; set; }
}

internal interface IDialog
{
    string Title { get; set; }
    string Description { get; set; }
    string? CancelTitle { get; set; }
    string ActionTitle { get; set; }
    bool IsDestructive { get; set; }
}

public interface IDialogConfigurator
{
    IDialogConfigurator SetTitle(string title);
    IDialogConfigurator SetDescription(string description);
    IDialogConfigurator SetActionTitle(string actionTitle);
    IDialogConfigurator SetCancelTitle(string cancelTitle);
    IDialogConfigurator SetDestructive();
}