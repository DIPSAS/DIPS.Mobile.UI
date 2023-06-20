using System.Windows.Input;
using Components.Resources.LocalizedStrings;
using DIPS.Mobile.UI.Components.Alerting.Dialog;

namespace Components.ComponentsSamples.Alerting.Dialogs;

public class DialogSamplesViewModel
{
    public DialogSamplesViewModel()
    {
        DisplayDialogCommand = new Command(() => DialogService.ShowMessage(LocalizedStrings.Dialog_FakeTitle,
            LocalizedStrings.Dialog_FakeDescription, LocalizedStrings.OK));
        DisplayTemporaryDialogCommand = new Command(async () =>
        {
            _ = DialogService.ShowMessage(LocalizedStrings.Dialog_FakeTitle, 
                LocalizedStrings.Dialog_TemporaryDescription, 
                LocalizedStrings.OK);
            await Task.Delay(5000);
            _ = DialogService.Remove();
        });
        DisplayDestructiveDialogCommand = new Command(() => DialogService.ShowDestructiveConfirmationMessage(LocalizedStrings.Dialog_FakeTitle,
                LocalizedStrings.Dialog_DestructiveDescription, LocalizedStrings.OK, LocalizedStrings.Destroy));
    }
    
    public ICommand DisplayDialogCommand { get; }
    public ICommand DisplayTemporaryDialogCommand { get; }
    public ICommand DisplayDestructiveDialogCommand { get; }
}