using Microsoft.Maui.Handlers;
using UIKit;

namespace DIPS.Mobile.UI.Components.Pages.Search;

public partial class SearchBehavior
{
    private PageSearchController? m_searchController;

    partial void SetupNativeSearch(ContentPage page)
    {
        // Dispatch to ensure the handler and navigation hierarchy are fully established
        page.Dispatcher.Dispatch(async () =>
        {
            await Task.Delay(1);

            if (page.Handler is not IPlatformViewHandler platformHandler)
                return;

            var viewController = platformHandler.ViewController;
            if (viewController?.NavigationController == null)
                return;

            m_searchController = new PageSearchController(this);
            viewController.NavigationItem.SearchController = m_searchController.SearchController;
            viewController.NavigationItem.HidesSearchBarWhenScrolling = false;
            viewController.DefinesPresentationContext = true;

            FocusSearchAction = () => m_searchController?.Focus();
            UnfocusSearchAction = () => m_searchController?.Unfocus();

            if (ShouldAutoFocus)
                FocusSearch();
        });
    }

    partial void TeardownNativeSearch(ContentPage page)
    {
        if (page.Handler is IPlatformViewHandler platformHandler)
        {
            var viewController = platformHandler.ViewController;
            if (viewController != null)
                viewController.NavigationItem.SearchController = null;
        }

        m_searchController?.Cleanup();
        m_searchController = null;
        FocusSearchAction = null;
        UnfocusSearchAction = null;
    }
}
