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
        CloseBottomSheetCommand = new Command(() => { BottomSheetService.CloseAll(); });

        TryCloseBottomSheetCommand = new Command(TryCloseBottomSheet);
    }

    private async void TryCloseBottomSheet()
    {
        var result = await DialogService.ShowConfirmationMessage(LocalizedStrings.AreYouSure_,
            LocalizedStrings.ThisWillCloseBottomSheet, LocalizedStrings.Cancel, LocalizedStrings.Yes);

        if (result == DialogAction.TappedAction)
        {
            _ = BottomSheetService.CloseAll();
        }
    }

    public ICommand CloseBottomSheetCommand { get; set; }

    public ICommand TryCloseBottomSheetCommand { get; set; }

}