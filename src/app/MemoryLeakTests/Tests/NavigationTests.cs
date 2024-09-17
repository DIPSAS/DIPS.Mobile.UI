using DIPS.Mobile.UI.Components.ListItems;

namespace MemoryLeakTests.Tests;

public class NavigationTests : UITest
{
    public override void BeforeTest(ContentPage contentPage)
    {
        var verticalStackLayout = new VerticalStackLayout();
        var popToRoot = new ListItem() { Title = "Pop to root", Command = new Command(() =>
        {
            Shell.Current.Navigation.PopToRootAsync();
        })};
        var swapRoot = new ListItem() { Title = "Swap root", Command = new Command(async () =>
        {
            await Shell.Current.Navigation.PopToRootAsync();
            var tabBar = new TabBar();
            var tab = new Tab();

            tab.Items.Add(new ShellContent()
            {
                ContentTemplate = new DataTemplate(() => new DummyRootPage())
            });
            tabBar.Items.Add(tab);
            Shell.Current.Items.RemoveAt(0);
            Shell.Current.Items.Add(tabBar);   
        })};

        verticalStackLayout.Add(popToRoot);
        verticalStackLayout.Add(swapRoot);
        
        contentPage.Content = verticalStackLayout;
    }

    public override string Name => "Navigation tests";

    private class DummyRootPage : ContentPage
    {
        public DummyRootPage()
        {
            Content = new ListItem()
            {
                Title = "Switch back",
                Command = new Command(() =>
                {
                    var tabBar = new TabBar();
                    var tab = new Tab();

                    tab.Items.Add(new ShellContent()
                    {
                        ContentTemplate = new DataTemplate(() => App.Container.GetInstance(typeof(MainPage)))
                    });
                    tabBar.Items.Add(tab);
                    Shell.Current.Items.RemoveAt(0);
                    Shell.Current.Items.Add(tabBar);   
                })
            };
        }
    }
}