using System.Collections.ObjectModel;
using DIPS.Mobile.UI.Components.Searching;
using DIPS.Mobile.UI.Resources.Sizes;
using DIPS.Mobile.UI.Resources.Styles;
using DIPS.Mobile.UI.Resources.Styles.Label;
using ButtonStyle = DIPS.Mobile.UI.Resources.Styles.Button.ButtonStyle;
using CollectionView = DIPS.Mobile.UI.Components.Lists.CollectionView;
using ContentPage = DIPS.Mobile.UI.Components.Pages.ContentPage;
using HorizontalStackLayout = DIPS.Mobile.UI.Components.Lists.HorizontalStackLayout;
using SearchBar = DIPS.Mobile.UI.Components.Searching.SearchBar;
using VerticalStackLayout = DIPS.Mobile.UI.Components.Lists.VerticalStackLayout;

namespace Playground.VetleSamples.CollectionViewTests;

public class RemoveFocusOnScrollAutoSearchPage : ContentPage
{
    private readonly CollectionView m_collectionView;
    private readonly Label m_lastEventLabel;
    private readonly Label m_focusCountersLabel;
    private readonly Label m_resultCountersLabel;
    private readonly SearchTriggerMode m_searchTriggerMode;
    private UpdateMode m_updateMode = UpdateMode.ReplaceItemsSource;
    private int m_focusedCount;
    private int m_unfocusedCount;
    private int m_resultVersion;
    private int m_searchVersion;
    private ObservableCollection<string> m_items = [];

    public RemoveFocusOnScrollAutoSearchPage(SearchTriggerMode searchTriggerMode = SearchTriggerMode.TextChanged)
    {
        m_searchTriggerMode = searchTriggerMode;
        Title = searchTriggerMode == SearchTriggerMode.TextChanged
            ? "Auto search + RemoveFocusOnScroll"
            : "SearchCommand + RemoveFocusOnScroll";

        var searchBar = new SearchBar
        {
            Placeholder = searchTriggerMode == SearchTriggerMode.TextChanged
                ? "Type to auto replace results"
                : "Type, then tap keyboard Search",
            ShouldDelay = searchTriggerMode == SearchTriggerMode.TextChanged,
            Delay = 500,
            HasBusyIndication = true,
            ReturnKeyType = searchTriggerMode == SearchTriggerMode.TextChanged
                ? ReturnType.Done
                : ReturnType.Search,
            ShouldCloseKeyboardOnReturnKeyTapped = true
        };
        searchBar.SearchCommand = new Command(() => RunSearchCommand(searchBar.Text));
        searchBar.TextChanged += OnSearchTextChanged;
        searchBar.Focused += (_, _) =>
        {
            m_focusedCount++;
            UpdateStatus("SearchBar focused");
        };
        searchBar.Unfocused += (_, _) =>
        {
            m_unfocusedCount++;
            UpdateStatus("SearchBar unfocused");
        };

        m_lastEventLabel = CreateLabel(LabelStyle.Body200);
        m_focusCountersLabel = CreateLabel(LabelStyle.UI100);
        m_resultCountersLabel = CreateLabel(LabelStyle.UI100);

        var replaceModeButton = CreateButton("Auto: Replace ItemsSource", () => SetUpdateMode(UpdateMode.ReplaceItemsSource));
        var mutateModeButton = CreateButton("Auto: Mutate collection", () => SetUpdateMode(UpdateMode.MutateCollection));
        var replaceButton = CreateButton("Replace ItemsSource", () => ReplaceItems(searchBar.Text, "Button"));
        var mutateButton = CreateButton("Mutate collection", () => MutateItems(searchBar.Text, "Button"));
        var clearButton = CreateButton("Clear results", ClearItems);
        var addButton = CreateButton("Add item", AddItem);
        var resetCountersButton = CreateButton("Reset counters", ResetCounters);

        var diagnosticsLayout = new VerticalStackLayout
        {
            Padding = new Thickness(Sizes.GetSize(SizeName.content_margin_medium)),
            Spacing = Sizes.GetSize(SizeName.size_1),
            Children =
            {
                new Label
                {
                    Text = "Diagnostics",
                    Style = Styles.GetLabelStyle(LabelStyle.SectionHeader)
                },
                m_lastEventLabel,
                m_focusCountersLabel,
                m_resultCountersLabel,
                new HorizontalStackLayout
                {
                    Spacing = Sizes.GetSize(SizeName.size_2),
                    Children = { replaceModeButton, mutateModeButton }
                },
                new HorizontalStackLayout
                {
                    Spacing = Sizes.GetSize(SizeName.size_2),
                    Children = { replaceButton, mutateButton }
                },
                new HorizontalStackLayout
                {
                    Spacing = Sizes.GetSize(SizeName.size_2),
                    Children = { clearButton, addButton, resetCountersButton }
                }
            }
        };

        m_collectionView = new CollectionView
        {
            RemoveFocusOnScroll = true,
            Header = new VerticalStackLayout
            {
                Spacing = Sizes.GetSize(SizeName.size_2),
                Children =
                {
                    searchBar,
                    diagnosticsLayout
                }
            },
            ItemTemplate = new DataTemplate(() =>
            {
                var label = CreateLabel(LabelStyle.Body200);
                label.SetBinding(Label.TextProperty, ".");
                label.Padding = new Thickness(Sizes.GetSize(SizeName.content_margin_medium), Sizes.GetSize(SizeName.content_margin_small));
                return label;
            })
        };

        Content = m_collectionView;
        ReplaceItems("initial", "Initial");
    }

    private async void OnSearchTextChanged(object sender, TextChangedEventArgs e)
    {
        if (m_searchTriggerMode == SearchTriggerMode.SearchCommand)
        {
            UpdateStatus($"Text changed to '{e.NewTextValue}'. Tap keyboard Search to replace results and dismiss focus");
            return;
        }

        if (sender is SearchBar searchBar)
        {
            searchBar.IsBusy = true;
        }

        var searchVersion = ++m_searchVersion;
        var searchText = e.NewTextValue ?? string.Empty;
        UpdateStatus($"Waiting to replace results for '{searchText}'");

        await Task.Delay(350);

        if (searchVersion != m_searchVersion)
        {
            return;
        }

        UpdateItems(searchText, "Auto search");

        if (sender is SearchBar completedSearchBar)
        {
            completedSearchBar.IsBusy = false;
        }
    }

    private void RunSearchCommand(string searchText)
    {
        if (m_searchTriggerMode == SearchTriggerMode.TextChanged)
        {
            UpdateStatus("SearchCommand tapped in auto mode");
            return;
        }

        UpdateItems(searchText, "SearchCommand");
    }

    private static Label CreateLabel(LabelStyle labelStyle) => new()
    {
        Style = Styles.GetLabelStyle(labelStyle)
    };

    private static Button CreateButton(string text, Action action) => new()
    {
        Text = text,
        Style = Styles.GetButtonStyle(ButtonStyle.DefaultSmall),
        Command = new Command(action)
    };

    private void SetUpdateMode(UpdateMode updateMode)
    {
        m_updateMode = updateMode;
        UpdateStatus($"Auto mode changed to {m_updateMode}");
    }

    private void UpdateItems(string searchText, string source)
    {
        if (m_updateMode == UpdateMode.ReplaceItemsSource)
        {
            ReplaceItems(searchText, source);
            return;
        }

        MutateItems(searchText, source);
    }

    private void ReplaceItems(string searchText, string source)
    {
        m_resultVersion++;
        var normalizedSearchText = string.IsNullOrWhiteSpace(searchText) ? "empty" : searchText.Trim();
        m_items = new ObservableCollection<string>(Enumerable.Range(1, 80).Select(index =>
            $"{source} result {index} - version {m_resultVersion} - query '{normalizedSearchText}'"));
        m_collectionView.ItemsSource = m_items;
        UpdateStatus($"ItemsSource replaced by {source}");
    }

    private void MutateItems(string searchText, string source)
    {
        m_resultVersion++;
        var normalizedSearchText = string.IsNullOrWhiteSpace(searchText) ? "empty" : searchText.Trim();
        m_items.Clear();

        foreach (var item in Enumerable.Range(1, 80).Select(index =>
                     $"{source} result {index} - version {m_resultVersion} - query '{normalizedSearchText}'"))
        {
            m_items.Add(item);
        }

        UpdateStatus($"ObservableCollection mutated by {source}");
    }

    private void ClearItems()
    {
        m_resultVersion++;
        m_items = [];
        m_collectionView.ItemsSource = m_items;
        UpdateStatus("ItemsSource cleared");
    }

    private void AddItem()
    {
        m_resultVersion++;
        m_items.Add($"Added item - version {m_resultVersion}");
        UpdateStatus("ObservableCollection item added");
    }

    private void ResetCounters()
    {
        m_focusedCount = 0;
        m_unfocusedCount = 0;
        UpdateStatus("Counters reset");
    }

    private void UpdateStatus(string lastEvent)
    {
        m_lastEventLabel.Text = $"Last event: {lastEvent} at {DateTime.Now:HH:mm:ss.fff}";
        m_focusCountersLabel.Text = $"Focused: {m_focusedCount}, Unfocused: {m_unfocusedCount}";
        m_resultCountersLabel.Text = $"Trigger: {m_searchTriggerMode}, Mode: {m_updateMode}, Result version: {m_resultVersion}, Item count: {m_items.Count}";
    }

    public enum SearchTriggerMode
    {
        TextChanged,
        SearchCommand
    }

    private enum UpdateMode
    {
        ReplaceItemsSource,
        MutateCollection
    }
}