using DIPS.Mobile.UI.Components.ListItems.Extensions;
using DIPS.Mobile.UI.Components.Pickers.MultiItemsPicker;
using ContentPage = DIPS.Mobile.UI.Components.Pages.ContentPage;

namespace MemoryLeakTests.Tests.Modals;

public class ModalRootPage : UITestContentPage
{
    public ModalRootPage()
    {
        ToolbarItems.Add(new ToolbarItem()
        {
            Text = "Close",
            Command = new Command(async () =>
            {
                var navigationPage =
                    Shell.Current.Navigation.ModalStack.FirstOrDefault(p => p is NavigationPage) as
                        NavigationPage;
                var stack = navigationPage.Navigation.NavigationStack;
                var pages = stack.Take(stack.Count - 1).ToList();
                foreach (var page in pages)
                {
                    navigationPage.Navigation
                        .RemovePage(page); // remove all pages except the one currently showing
                }

                Shell.Current.Navigation.PopModalAsync();
            })
            

        });

        Content = new VerticalStackLayout()
        {
            Children =
            {
                new NavigationListItem
                {
                    Title = "Navigate",
                    Command = new Command(() =>
                    {
                        Shell.Current.Navigation.PushModalAsync(new ModalTestPage());
                    }),
                    VerticalOptions = LayoutOptions.Start
                },
                new NavigationListItem
                {
                    Title = "Navigate",
                    Command = new Command(() =>
                    {
                        Shell.Current.Navigation.PushModalAsync(new ModalTestPage());
                    }),
                    VerticalOptions = LayoutOptions.Start
                },
                new DIPS.Mobile.UI.Components.Buttons.Button(){Text = "Hello!"},
                new MultiItemsPicker()
            }
        };
    }
}