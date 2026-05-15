using System.Collections.ObjectModel;
using DIPS.Mobile.UI.Components.Searching;
using ContentPage = DIPS.Mobile.UI.Components.Pages.ContentPage;
using ScrollView = DIPS.Mobile.UI.Components.Lists.ScrollView;
using SearchBar = DIPS.Mobile.UI.Components.Searching.SearchBar;

namespace Playground.VetleSamples.CollectionViewTests;

public class RemoveFocusOnScrollScrollViewPage : ContentPage
{
    public RemoveFocusOnScrollScrollViewPage(bool searchBarInScrollView)
    {
        Title = searchBarInScrollView ? "SearchBar inside ScrollView" : "SearchBar outside ScrollView";

        var searchBar = new SearchBar
        {
            Placeholder = "Search (scroll to dismiss keyboard)"
        };

        var stackLayout = new VerticalStackLayout();
        
        if (searchBarInScrollView)
        {
            stackLayout.Children.Add(searchBar);
        }

        for (var i = 1; i <= 200; i++)
        {
            var label = new Label
            {
                Text = $"Item {i}",
                Padding = new Thickness(16, 12)
            };
            label.GestureRecognizers.Add(new TapGestureRecognizer
            {
                Command = new Command(() => System.Diagnostics.Debug.WriteLine("Item tapped"))
            });
            stackLayout.Children.Add(label);
        }

        var scrollView = new ScrollView
        {
            RemoveFocusOnScroll = true,
            Content = stackLayout
        };

        if (searchBarInScrollView)
        {
            Content = scrollView;
        }
        else
        {
            Grid.SetRow(scrollView, 1);

            Content = new Grid
            {
                RowDefinitions = [new RowDefinition(GridLength.Auto), new RowDefinition(GridLength.Star)],
                Children =
                {
                    searchBar,
                    scrollView
                }
            };
        }
    }
}
