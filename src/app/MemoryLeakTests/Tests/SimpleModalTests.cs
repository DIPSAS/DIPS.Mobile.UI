using DIPS.Mobile.UI.Components.ListItems.Extensions;
using MemoryLeakTests.Tests.Modals;
using VerticalStackLayout = DIPS.Mobile.UI.Components.Lists.VerticalStackLayout;

namespace MemoryLeakTests.Tests;

public class SimpleModalTests : UITest
{
    public override void BeforeTest(ContentPage contentPage)
    {
        var verticalStackLayout = new VerticalStackLayout { Spacing = 0 };

        verticalStackLayout.Add(new NavigationListItem
        {
            Title = "Open simple modal (no NavigationPage)",
            Command = new Command(() => Shell.Current.Navigation.PushModalAsync(new SimpleModalPage()))
        });

        contentPage.Content = new ContentView { AutomationId = "Test", Content = verticalStackLayout };
    }

    public override string Name => "Simple Modal Tests (No NavigationPage)";
}
