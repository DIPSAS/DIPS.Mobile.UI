using System.Windows.Input;
using Components.Resources.LocalizedStrings;
using DIPS.Mobile.UI.Components.Alerting.Dialog;
using DIPS.Mobile.UI.Components.BottomSheets;
using DIPS.Mobile.UI.MVVM;

namespace Components.ComponentsSamples.BottomSheets.Sheets;

public class BottomSheetWithToolbarViewModel : ViewModel
{
    public BottomSheetWithToolbarViewModel()
    {
        TryCloseBottomSheetCommand = new Command<Action>(TryCloseBottomSheet);
    }

    private async void TryCloseBottomSheet(Action action)
    {
        var result = await DialogService.ShowConfirmationMessage(LocalizedStrings.AreYouSure_,
            LocalizedStrings.ThisWillCloseBottomSheet, LocalizedStrings.Cancel, LocalizedStrings.Yes);

        if (result == DialogAction.TappedAction)
        {
            action.Invoke();
        }
    }

    public ICommand TryCloseBottomSheetCommand { get; set; }

}