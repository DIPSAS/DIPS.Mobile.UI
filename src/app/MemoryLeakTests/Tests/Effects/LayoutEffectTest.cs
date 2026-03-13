using DIPS.Mobile.UI.Components.Labels;
using DIPS.Mobile.UI.Effects.Layout;
using Label = DIPS.Mobile.UI.Components.Labels.Label;
using Layout = DIPS.Mobile.UI.Effects.Layout.Layout;

namespace MemoryLeakTests.Tests;

public class LayoutEffectTest : UITest
{
    public override void BeforeTest(ContentPage contentPage)
    {
        var grid = new Grid
        {
            HeightRequest = 50,
            Children = { new Label { Text = "Corner radius" } }
        };
        Layout.SetCornerRadius(grid, new CornerRadius(8));

        contentPage.Content = grid;
    }

    public override string Name => "Layout Effect";
}
