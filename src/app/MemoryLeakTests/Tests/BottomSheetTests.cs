using DIPS.Mobile.UI.Components.BottomSheets;

namespace MemoryLeakTests.Tests;

public class BottomSheetTests : UITest
{
    public override void BeforeTest(ContentPage contentPage)
    {
        contentPage.Content = new Button()
        {
            Command = new Command(() =>
            {
                _ = BottomSheetService.Open(new BottomSheet() {Title = "Testing", BindingContext = "This is a test binding context"});
            })
        };
    }

    public override string Name => "Bottom Sheet Tests";
}