using DIPS.Mobile.UI.Components.ListItems;
using DIPS.Mobile.UI.Components.ListItems.Extensions;

namespace MemoryLeakTests.Tests;

public class ShellItemChangedTests : UITest
{
    public override void BeforeTest(ContentPage contentPage)
    {
        contentPage.Content = new ListItem
        {
            Title = "Swap to multi-tab shell",
            Command = new Command(SwapToMultiTabShell)
        };
    }

    public override string Name => "Shell Item Changed";

    private static void SwapToMultiTabShell()
    {
        var tabBar = new TabBar();

        var tab1 = new Tab { Title = "Tab 1" };
        tab1.Items.Add(new ShellContent
        {
            ContentTemplate = new DataTemplate(() => new NavigablePage("Tab 1"))
        });

        var tab2 = new Tab { Title = "Tab 2" };
        tab2.Items.Add(new ShellContent
        {
            ContentTemplate = new DataTemplate(() => new NavigablePage("Tab 2"))
        });

        tabBar.Items.Add(tab1);
        tabBar.Items.Add(tab2);
        Shell.Current.Items.Clear();
        Shell.Current.Items.Add(tabBar);
    }

    internal static void SwapBackToMainPage()
    {
        var tabBar = new TabBar();
        var tab = new Tab();
        tab.Items.Add(new ShellContent
        {
            ContentTemplate = new DataTemplate(() => App.Container.GetInstance(typeof(MainPage)))
        });
        tabBar.Items.Add(tab);
        Shell.Current.Items.Clear();
        Shell.Current.Items.Add(tabBar);
    }

    private class NavigablePage : ContentPage
    {
        public NavigablePage(string tabName)
        {
            Title = tabName;

            var layout = new VerticalStackLayout { Spacing = 0 };

            layout.Add(new NavigationListItem
            {
                Title = "Push page",
                Command = new Command(() =>
                    Shell.Current.Navigation.PushAsync(new NavigablePage(tabName)))
            });

            layout.Add(new ListItem
            {
                Title = "Switch back to tests",
                Command = new Command(SwapBackToMainPage)
            });

            Content = layout;
        }
    }
}
