using DIPS.Mobile.UI.Components.BottomSheets;

namespace Components.ComponentsSamples.BottomSheets.Sheets;

public partial class BottomSheetWithToolbar
{
    public BottomSheetWithToolbar()
    {
        InitializeComponent();
    }

    protected override void OnClosed()
    {
        base.OnClosed();
        
        
    }

    private void MenuItem_OnClicked(object sender, EventArgs e)
    {
        BottomSheetService.CloseCurrentBottomSheet();
    }
}