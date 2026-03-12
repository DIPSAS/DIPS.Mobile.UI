using DIPS.Mobile.UI.Components.Chips;

namespace MemoryLeakTests.Tests;

public class ChipTest : UITest
{
    public override void BeforeTest(ContentPage contentPage)
    {
        contentPage.Content = new Chip { Title = "Chip", Command = new Command(() => { }) };
    }

    public override string Name => "Chip";
}
