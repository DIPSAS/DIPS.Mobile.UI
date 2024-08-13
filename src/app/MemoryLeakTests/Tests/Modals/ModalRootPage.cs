using DIPS.Mobile.UI.Components.ListItems.Extensions;

namespace MemoryLeakTests.Tests.Modals;

public class ModalRootPage : ContentPage
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

        Content = new NavigationListItem { Title = "Navigate", Command = new Command(() =>
            {
                Shell.Current.Navigation.PushModalAsync(new ModalTestPage());
            }), 
            VerticalOptions = LayoutOptions.Start
        };
    }
}