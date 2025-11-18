using DIPS.Mobile.UI.Components.ListItems;
using DIPS.Mobile.UI.Components.ListItems.Extensions;
using DIPS.Mobile.UI.MemoryManagement;
using MemoryLeakTests.Tests.Modals;
using Microsoft.Maui.Controls.PlatformConfiguration.iOSSpecific;
using NavigationPage = Microsoft.Maui.Controls.NavigationPage;
using VerticalStackLayout = DIPS.Mobile.UI.Components.Lists.VerticalStackLayout;

namespace MemoryLeakTests.Tests;

public class ModalTests : UITest
{
    public override void BeforeTest(ContentPage contentPage)
    {
        var verticalStackLayout = new VerticalStackLayout { Spacing = 0 };

        var navigationListItem = new NavigationListItem { Title = "Start modal navigation tests", Command = new Command(() =>
            {
                var navigationPage = new NavigationPage(new ModalRootPage());
                /*navigationPage.On<Microsoft.Maui.Controls.PlatformConfiguration.iOS>().SetModalPresentationStyle(UIModalPresentationStyle.PageSheet);*/
                Shell.Current.Navigation.PushModalAsync(navigationPage);
            })
        };
        
        verticalStackLayout.Add(navigationListItem);
        verticalStackLayout.Add(new Button()
        {
            Text = "Hello !"
        });

        contentPage.Content = new ContentView{ AutomationId = "Test", Content = verticalStackLayout};
    }

    public override string Name => "Modal Tests";
}