using DIPS.Mobile.UI.Components.BottomSheets;
using DIPS.Mobile.UI.Components.Images;
using DIPS.Mobile.UI.Resources.Icons;
using ImageButton = DIPS.Mobile.UI.Components.Images.ImageButton;

namespace Playground.VetleSamples;

public partial class VetleTestPage1 
{
    public VetleTestPage1()
    {
        InitializeComponent();
        
    }

    public void AddButton(object sender, EventArgs eventArgs)
    {
        Layout.Add(new ImageButton{ TintColor = Colors.Blue, Source = Icons.GetIcon(IconName.bell_fill)});
    }

    private void NavigationListItem_OnTapped(object sender, EventArgs e)
    {
        BottomSheetService.OpenBottomSheet(new TestBottomSheetNotFitToContent());
    }

    private void Test(object sender, EventArgs e)
    {
        BottomSheetService.OpenBottomSheet(new TestBottomSheetFitToContent());
    }

    private void Lol(object sender, EventArgs e)
    {
        BottomSheetService.OpenBottomSheet(new BottomSheetWithToolbar());
    }
}