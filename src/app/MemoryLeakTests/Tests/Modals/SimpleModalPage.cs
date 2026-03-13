using DIPS.Mobile.UI.Components.ListItems.Extensions;
using DIPS.Mobile.UI.Components.TextFields.InputFields;
using ContentPage = DIPS.Mobile.UI.Components.Pages.ContentPage;

namespace MemoryLeakTests.Tests.Modals;

public class SimpleModalPage : ContentPage
{
    public SimpleModalPage()
    {
        Title = "Simple Modal (No NavigationPage)";
        
        Content = new VerticalStackLayout
        {
            Children =
            {
                new NavigationListItem
                {
                    Title = "Close modal",
                    Command = new Command(() => Shell.Current.Navigation.PopModalAsync())
                },
                new DIPS.Mobile.UI.Components.Buttons.Button { Text = "Hello!" },
                new SingleLineInputField()
            }
        };
    }
}
