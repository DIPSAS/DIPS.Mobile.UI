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
        DisplayTwoDialogsCommand = new Command(async () =>
        {
            await Task.WhenAll(ShowDialogOne(), ShowDialogTwo());
        });
    }
    
    public ICommand DisplayDialogCommand { get; }
    public ICommand DisplayTemporaryDialogCommand { get; }
    public ICommand DisplayDestructiveDialogCommand { get; }
    public ICommand DisplayTwoDialogsCommand { get; }
        
    private async Task ShowDialogOne()
    {
        await DialogService.ShowMessage("Dialog 1", "Dialog 1", "OK");
    }

    private async Task ShowDialogTwo()
    {
        await Task.Delay(1000);
        _ = DialogService.ShowMessage("Dialog 2", "Dialog 2", "OK");
    }
}