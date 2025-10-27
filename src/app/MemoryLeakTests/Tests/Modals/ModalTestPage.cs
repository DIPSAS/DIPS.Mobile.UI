using DIPS.Mobile.UI.Components.ListItems.Extensions;
using DIPS.Mobile.UI.Components.TextFields.InputFields;
using ContentPage = DIPS.Mobile.UI.Components.Pages.ContentPage;

namespace MemoryLeakTests.Tests.Modals;

public class ModalTestPage : ContentPage
{
    public ModalTestPage()
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
                    Title = "Push another modal",
                    Command = new Command(() =>
                    {
                        Shell.Current.Navigation.PushModalAsync(new NavigationPage(new ModalTestPage()));
                    }),
                    VerticalOptions = LayoutOptions.Start
                },
                new NavigationListItem
                {
                    Title = "Remove UITestContentPage from normal navigation stack",
                    Subtitle = "This should check if the page is GC'ed, and not interfere with anything else",
                    Command = new Command(() =>
                    {
                        var page = Shell.Current.Navigation.NavigationStack.FirstOrDefault(p => p is UITestContentPage);
                        if(page is null)
                            return;
                        Shell.Current.Navigation.RemovePage(page);
                    })
                },
                new NavigationListItem
                {
                    Title = "Remove modal root page",
                    Command = new Command(() =>
                    {
                        var page = Shell.Current.Navigation.ModalStack.LastOrDefault(p => p is NavigationPage);
                        if(page is not NavigationPage navigationPage)
                            return;
                        
                        navigationPage.Navigation.RemovePage(navigationPage.RootPage);
                    })
                },
                new SingleLineInputField()
            }
        };

    }
}