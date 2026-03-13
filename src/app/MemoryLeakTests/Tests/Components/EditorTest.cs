using Editor = DIPS.Mobile.UI.Components.TextFields.Editor.Editor;

namespace MemoryLeakTests.Tests;

public class EditorTest : UITest
{
    public override void BeforeTest(ContentPage contentPage)
    {
        contentPage.Content = new VerticalStackLayout
        {
            Children =
            {
                new Editor { Placeholder = "Editor 1" },
                new Editor { Placeholder = "Editor 2" }
            }
        };
    }

    public override string Name => "Editor";
}
