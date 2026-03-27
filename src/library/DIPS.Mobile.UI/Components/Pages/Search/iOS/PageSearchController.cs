using Foundation;
using UIKit;

namespace DIPS.Mobile.UI.Components.Pages.Search;

/// <summary>
/// Manages a native <see cref="UISearchController"/> for ContentPage search functionality.
/// The search controller is added to the navigation item, providing the standard iOS search experience.
/// Implements <see cref="IUISearchResultsUpdating"/> to receive search text updates.
/// </summary>
internal class PageSearchController : NSObject, IUISearchResultsUpdating
{
    private readonly WeakReference<SearchBehavior> m_weakBehavior;
    private string m_previousText = string.Empty;

    public UISearchController SearchController { get; }

    public PageSearchController(SearchBehavior behavior)
    {
        m_weakBehavior = new WeakReference<SearchBehavior>(behavior);

        SearchController = new UISearchController(searchResultsController: null)
        {
            SearchResultsUpdater = this,
            ObscuresBackgroundDuringPresentation = false,
            HidesNavigationBarDuringPresentation = false
        };

        var searchBar = SearchController.SearchBar;
        searchBar.AutocapitalizationType = UITextAutocapitalizationType.None;
        searchBar.SearchBarStyle = UISearchBarStyle.Minimal;
    }

    public void UpdateSearchResultsForSearchController(UISearchController searchController)
    {
        if (!m_weakBehavior.TryGetTarget(out var behavior))
            return;

        var newText = searchController.SearchBar.Text ?? string.Empty;
        var oldText = m_previousText;
        m_previousText = newText;

        behavior.OnNativeSearchTextChanged(newText, oldText);
    }

    public void Focus()
    {
        SearchController.Active = true;
        SearchController.SearchBar.BecomeFirstResponder();
    }

    public void Unfocus()
    {
        SearchController.SearchBar.ResignFirstResponder();
    }

    public void Cleanup()
    {
        SearchController.SearchResultsUpdater = null;
        SearchController.Dispose();
    }
}
