using System.Collections.ObjectModel;
using DIPS.Mobile.UI.Components.Lists;
using DIPS.Mobile.UI.Components.Loading;
using DIPS.Mobile.UI.Components.Searching;
using CollectionView = DIPS.Mobile.UI.Components.Lists.CollectionView;
using ContentPage = DIPS.Mobile.UI.Components.Pages.ContentPage;
using RefreshView = DIPS.Mobile.UI.Components.Loading.RefreshView;
using SearchBar = DIPS.Mobile.UI.Components.Searching.SearchBar;

namespace Playground.VetleSamples.CollectionViewTests;

public class RemoveFocusOnScrollPage : ContentPage
{
    public RemoveFocusOnScrollPage(bool wrapInRefreshView, bool searchBarInHeader, bool delayedBinding = false)
    {
        var parts = new List<string>();
        parts.Add(searchBarInHeader ? "SearchBar in Header" : "SearchBar outside");
        if (wrapInRefreshView) parts.Add("RefreshView");
        if (delayedBinding) parts.Add("Delayed");
        Title = string.Join(" + ", parts);

        var searchBar = new SearchBar
        {
            Placeholder = "Search (scroll to dismiss keyboard)"
        };

        var collectionView = new CollectionView
        {
            RemoveFocusOnScroll = true,
            ItemTemplate = new DataTemplate(() =>
            {
                var label = new Label();
                label.SetBinding(Label.TextProperty, ".");
                label.Padding = new Thickness(16, 12);
                label.GestureRecognizers.Add(new TapGestureRecognizer
                {
                    Command = new Command(() => System.Diagnostics.Debug.WriteLine("Item tapped"))
                });
                return label;
            })
        };

        if (!delayedBinding)
        {
            var items = new ObservableCollection<string>();
            for (var i = 1; i <= 200; i++)
                items.Add($"Item {i}");
            collectionView.ItemsSource = items;
        }

        if (searchBarInHeader)
        {
            collectionView.Header = searchBar;
        }

        View listView = collectionView;

        if (wrapInRefreshView)
        {
            listView = new RefreshView
            {
                Content = collectionView
            };
        }

        if (searchBarInHeader)
        {
            Content = listView;
        }
        else
        {
            Grid.SetRow(listView, 1);

            Content = new Grid
            {
                RowDefinitions = [new RowDefinition(GridLength.Auto), new RowDefinition(GridLength.Star)],
                Children =
                {
                    searchBar,
                    listView
                }
            };
        }

        if (delayedBinding)
        {
            _ = LoadItemsAsync(collectionView);
        }
    }

    private static async Task LoadItemsAsync(CollectionView collectionView)
    {
        await Task.Delay(1000);
        var items = new ObservableCollection<string>();
        for (var i = 1; i <= 200; i++)
            items.Add($"Item {i}");
        collectionView.ItemsSource = items;
    }
}
