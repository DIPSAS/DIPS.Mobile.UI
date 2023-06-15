using System.Windows.Input;
using DIPS.Mobile.UI.Components.BottomSheets;
using DIPS.Mobile.UI.MVVM;

namespace Components.ComponentsSamples.BottomSheets.Sheets;

public class BottomSheetWithToolbarViewModel : ViewModel
{
    public BottomSheetWithToolbarViewModel()
    {
        CloseBottomSheetCommand = new Command(() => { BottomSheetService.CloseCurrentBottomSheet(); });        
    }
    
    public ICommand CloseBottomSheetCommand { get; set; }

    public string Title => "Cancel";

}