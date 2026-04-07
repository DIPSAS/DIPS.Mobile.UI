using DIPS.Mobile.UI.Components.Labels;
using Label = DIPS.Mobile.UI.Components.Labels.Label;

namespace MemoryLeakTests.Tests;

public class LabelTest : UITest
{
    public override void BeforeTest(ContentPage contentPage)
    {
        contentPage.Content = new Label { Text = "Label" };
    }

    public override string Name => "Label";
}
