using DIPS.Mobile.UI.Components.Labels;
using DIPS.Mobile.UI.Effects.Touch;
using Label = DIPS.Mobile.UI.Components.Labels.Label;

namespace MemoryLeakTests.Tests;

public class TouchLongPressEffectTest : UITest
{
    public override void BeforeTest(ContentPage contentPage)
    {
        var grid = new Grid
        {
            HeightRequest = 50,
            Children = { new Label { Text = "Long press me" } }
        };
        Touch.SetCommand(grid, new Command(() => { }));
        Touch.SetLongPressCommand(grid, new Command(() => { }));
        Touch.SetIsEnabled(grid, true);
        SemanticProperties.SetDescription(grid, "Long press test");

        contentPage.Content = grid;
    }

    public override string Name => "Touch LongPress Effect";
}
