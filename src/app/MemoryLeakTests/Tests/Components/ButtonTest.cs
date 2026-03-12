using Button = DIPS.Mobile.UI.Components.Buttons.Button;

namespace MemoryLeakTests.Tests;

public class ButtonTest : UITest
{
    public override void BeforeTest(ContentPage contentPage)
    {
        contentPage.Content = new Button { Text = "Button", Command = new Command(() => { }) };
    }

    public override string Name => "Button";
}
