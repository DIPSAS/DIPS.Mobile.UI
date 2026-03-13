using DIPS.Mobile.UI.Components.Loading;
using ActivityIndicator = DIPS.Mobile.UI.Components.Loading.ActivityIndicator;

namespace MemoryLeakTests.Tests;

public class ActivityIndicatorTest : UITest
{
    public override void BeforeTest(ContentPage contentPage)
    {
        contentPage.Content = new ActivityIndicator { IsRunning = true };
    }

    public override string Name => "ActivityIndicator";
}
