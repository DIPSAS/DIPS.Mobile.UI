using DIPS.Mobile.UI.Components.Chips;
using DIPS.Mobile.UI.Components.ListItems;
using DIPS.Mobile.UI.Components.ListItems.Extensions;
using DIPS.Mobile.UI.Components.TextFields.InputFields;
using DIPS.Mobile.UI.Effects.Touch;
using SearchBar = DIPS.Mobile.UI.Components.Searching.SearchBar;
using Button = DIPS.Mobile.UI.Components.Buttons.Button;
using Entry = DIPS.Mobile.UI.Components.TextFields.Entry.Entry;
using Editor = DIPS.Mobile.UI.Components.TextFields.Editor.Editor;
using NavigationPage = Microsoft.Maui.Controls.NavigationPage;
using VerticalStackLayout = DIPS.Mobile.UI.Components.Lists.VerticalStackLayout;

namespace MemoryLeakTests.Tests;

public class ModalComponentTest : UITest
{
    public override void BeforeTest(ContentPage contentPage)
    {
        contentPage.Content = new NavigationListItem
        {
            Title = "Open modal with components",
            Command = new Command(() =>
            {
                Shell.Current.Navigation.PushModalAsync(new NavigationPage(new ModalComponentPage()));
            })
        };
    }

    public override string Name => "Modal Components";
}

internal class ModalComponentPage : UITestContentPage
{
    public ModalComponentPage()
    {
        Title = "Modal Components";
        ToolbarItems.Add(new ToolbarItem
        {
            Text = "Close",
            Command = new Command(() => Shell.Current.Navigation.PopModalAsync())
        });

        var layout = new VerticalStackLayout { Spacing = 4 };

        layout.Add(new Entry { Placeholder = "Entry" });
        layout.Add(new Editor { Placeholder = "Editor" });
        layout.Add(new SingleLineInputField { HeaderText = "Input" });
        layout.Add(new SearchBar { Placeholder = "Search" });
        layout.Add(new Button { Text = "Button" });
        layout.Add(new Chip { Title = "Chip" });
        layout.Add(new ListItem { Title = "ListItem" });

        var touchGrid = new Grid
        {
            HeightRequest = 50,
            Children = { new Label { Text = "Touch effect" } }
        };
        Touch.SetCommand(touchGrid, new Command(() => { }));
        Touch.SetIsEnabled(touchGrid, true);
        SemanticProperties.SetDescription(touchGrid, "Touch");
        layout.Add(touchGrid);

        Content = new DIPS.Mobile.UI.Components.Lists.ScrollView { Content = layout };
    }
}
