using DIPS.Mobile.UI.Components.TextFields.InputFields;
using DIPS.Mobile.UI.Components.TextFields.InputFields.MultiLineInputField;

namespace MemoryLeakTests.Tests;

public class InputFieldTest : UITest
{
    public override void BeforeTest(ContentPage contentPage)
    {
        contentPage.Content = new VerticalStackLayout
        {
            Children =
            {
                new SingleLineInputField { HeaderText = "Single line" },
                new MultiLineInputField { HeaderText = "Multi line" }
            }
        };
    }

    public override string Name => "InputFields";
}
