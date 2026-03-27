using Android.Views;
using Android.Widget;

namespace DIPS.Mobile.UI.Components.Pages.Search;

public partial class SearchBehavior
{
    private PageSearchField? m_searchField;
    private LinearLayout? m_wrapperLayout;

    partial void SetupNativeSearch(ContentPage page)
    {
        if (page.Handler?.PlatformView is not Android.Views.View platformView)
            return;

        var context = platformView.Context;
        if (context == null)
            return;

        m_searchField = new PageSearchField(context, this);

        // Wrap the page's native view: insert a LinearLayout with page content at top, SearchBar at bottom
        if (platformView.Parent is ViewGroup parent)
        {
            var index = parent.IndexOfChild(platformView);
            var originalLayoutParams = platformView.LayoutParameters;

            parent.RemoveView(platformView);

            m_wrapperLayout = new LinearLayout(context)
            {
                Orientation = Orientation.Vertical,
                LayoutParameters = originalLayoutParams ?? new ViewGroup.LayoutParams(
                    ViewGroup.LayoutParams.MatchParent,
                    ViewGroup.LayoutParams.MatchParent)
            };

            // Page content fills remaining space (weight=1)
            m_wrapperLayout.AddView(platformView, new LinearLayout.LayoutParams(
                ViewGroup.LayoutParams.MatchParent, 0, 1f));

            // SearchBar pill at the bottom
            m_wrapperLayout.AddView(m_searchField.SearchBarView);

            parent.AddView(m_wrapperLayout, index);

            // Add SearchView to the root decor view so it can overlay the entire page (full-screen search mode)
            if (context is Android.App.Activity activity)
            {
                var decorView = activity.Window?.DecorView as ViewGroup;
                var contentRoot = decorView?.FindViewById<FrameLayout>(Android.Resource.Id.Content);
                contentRoot?.AddView(m_searchField.SearchViewView);
            }
        }

        FocusSearchAction = () => m_searchField?.Focus();
        UnfocusSearchAction = () => m_searchField?.Unfocus();

        if (ShouldAutoFocus)
            FocusSearch();
    }

    partial void TeardownNativeSearch(ContentPage page)
    {
        // Restore the original view hierarchy
        if (page.Handler?.PlatformView is Android.Views.View platformView &&
            m_wrapperLayout != null)
        {
            if (m_wrapperLayout.Parent is ViewGroup grandParent)
            {
                var index = grandParent.IndexOfChild(m_wrapperLayout);
                m_wrapperLayout.RemoveView(platformView);
                grandParent.RemoveView(m_wrapperLayout);
                grandParent.AddView(platformView, index);
            }
        }

        m_searchField?.Cleanup();
        m_searchField = null;
        m_wrapperLayout = null;
        FocusSearchAction = null;
        UnfocusSearchAction = null;
    }
}
