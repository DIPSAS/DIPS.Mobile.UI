using DIPS.Mobile.UI.Components.BottomSheets;

namespace Playground.VetleSamples;

public partial class TestBottomSheetFitToContent
{
    public TestBottomSheetFitToContent()
    {
        InitializeComponent();
    }

    private void Button_OnClicked(object sender, EventArgs e)
    {
        BottomSheetService.CloseAll();
    }
}