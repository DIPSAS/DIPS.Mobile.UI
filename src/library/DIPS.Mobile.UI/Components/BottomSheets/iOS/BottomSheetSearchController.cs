using DIPS.Mobile.UI.Resources.LocalizedStrings.LocalizedStrings;
using Foundation;
using UIKit;
using Colors = DIPS.Mobile.UI.Resources.Colors.Colors;

namespace DIPS.Mobile.UI.Components.BottomSheets.iOS;

/// <summary>
/// Manages a native UISearchController for BottomSheet search functionality.
/// The search controller is added to the navigation item, providing the standard iOS search experience.
/// </summary>
internal class BottomSheetSearchController : NSObject, IUISearchResultsUpdating
{
    private readonly WeakReference<BottomSheet> m_weakBottomSheet;
    private string m_previousText = string.Empty;

    public UISearchController SearchController { get; }

    public BottomSheetSearchController(BottomSheet bottomSheet)
    {
        m_weakBottomSheet = new WeakReference<BottomSheet>(bottomSheet);

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
        if (!m_weakBottomSheet.TryGetTarget(out var bottomSheet))
            return;

        var newText = searchController.SearchBar.Text ?? string.Empty;
        var oldText = m_previousText;
        m_previousText = newText;

        bottomSheet.OnNativeSearchTextChanged(newText, oldText);
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
