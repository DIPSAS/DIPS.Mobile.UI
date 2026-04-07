using Entry = DIPS.Mobile.UI.Components.TextFields.Entry.Entry;

namespace MemoryLeakTests.Tests;

public class EntryTest : UITest
{
    public override void BeforeTest(ContentPage contentPage)
    {
        contentPage.Content = new VerticalStackLayout
        {
            Children =
            {
                new Entry { Placeholder = "Entry 1" },
                new Entry { Placeholder = "Entry 2" }
            }
        };
    }

    public override string Name => "Entry";
}
