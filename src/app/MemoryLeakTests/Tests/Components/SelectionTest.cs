using DIPS.Mobile.UI.Components.Selection;
using RadioButton = DIPS.Mobile.UI.Components.Selection.RadioButton;
using Switch = DIPS.Mobile.UI.Components.Selection.Switch;

namespace MemoryLeakTests.Tests;

public class SelectionTest : UITest
{
    public override void BeforeTest(ContentPage contentPage)
    {
        contentPage.Content = new VerticalStackLayout
        {
            Children =
            {
                new Switch(),
                new Checkmark(),
                new RadioButton()
            }
        };
    }

    public override string Name => "Selection";
}
