using DIPS.Mobile.UI.Components.ListItems;
using DIPS.Mobile.UI.Components.ListItems.Extensions;
using VerticalStackLayout = DIPS.Mobile.UI.Components.Lists.VerticalStackLayout;

namespace MemoryLeakTests.Tests;

public class ListItemTest : UITest
{
    public override void BeforeTest(ContentPage contentPage)
    {
        contentPage.Content = new VerticalStackLayout
        {
            Children =
            {
                new ListItem { Title = "ListItem", Command = new Command(() => { }) },
                new NavigationListItem { Title = "NavigationListItem", Command = new Command(() => { }) }
            }
        };
    }

    public override string Name => "ListItem";
}
