using DIPS.Mobile.UI.Components.ListItems;
using DIPS.Mobile.UI.Components.ListItems.Extensions;
using MemoryLeakTests.Tests;

namespace MemoryLeakTests;

public class MainPage : ContentPage
{
    private readonly IEnumerable<UITest> m_uiTests;

    public MainPage(IEnumerable<UITest> uiTests)
    {
        m_uiTests = uiTests;
        var collectionView = new CollectionView()
        {
            ItemsSource = uiTests,
            ItemTemplate = new DataTemplate((UITestTemplate))
        };

        Content = collectionView;
    }

    private object UITestTemplate()
    {
        var navigationListItem = new NavigationListItem();
        navigationListItem.SetBinding(ListItem.TitleProperty, nameof(UITest.Name));
        navigationListItem.Command = new Command(() =>
        {
            var uiTest = m_uiTests.FirstOrDefault(test => test.Name == navigationListItem.Title);
            Application.Current?.MainPage?.Navigation.PushAsync(uiTest.CreatePage());
        });
        return navigationListItem;
    }
}