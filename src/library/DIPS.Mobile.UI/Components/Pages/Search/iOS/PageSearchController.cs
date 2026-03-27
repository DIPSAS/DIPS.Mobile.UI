using Foundation;
using UIKit;

namespace DIPS.Mobile.UI.Components.Pages.Search;

/// <summary>
/// Provides a Notes-app-style search experience for ContentPage.
/// A native <see cref="UISearchBar"/> is placed at the bottom of the page.
/// When activated, it animates (translates) to the top of the page, showing the keyboard.
/// When cancelled, it animates back to the bottom position.
/// Uses all native UIKit components: <see cref="UISearchBar"/>, <see cref="NSLayoutConstraint"/>,
/// and <see cref="UIView.Animate(double, Action)"/>.
/// </summary>
internal class PageSearchController : NSObject, IUISearchBarDelegate
{
    private readonly WeakReference<SearchBehavior> m_weakBehavior;
    private string m_previousText = string.Empty;

    private UISearchBar? m_searchBar;
    private NSLayoutConstraint? m_bottomConstraint;
    private NSLayoutConstraint? m_topConstraint;
    private bool m_isActive;

    public PageSearchController(SearchBehavior behavior)
    {
        m_weakBehavior = new WeakReference<SearchBehavior>(behavior);
    }

    /// <summary>
    /// Sets up the native UISearchBar at the bottom of the view controller's view.
    /// </summary>
    public void SetupInViewController(UIViewController viewController)
    {
        var view = viewController.View;
        if (view == null) return;

        m_searchBar = new UISearchBar
        {
            SearchBarStyle = UISearchBarStyle.Minimal,
            AutocapitalizationType = UITextAutocapitalizationType.None,
            Delegate = this,
            TranslatesAutoresizingMaskIntoConstraints = false
        };
        view.AddSubview(m_searchBar);

        // Bottom position (initial state) — pinned to bottom safe area
        m_bottomConstraint = m_searchBar.BottomAnchor.ConstraintEqualTo(
            view.SafeAreaLayoutGuide.BottomAnchor);

        // Top position (search mode) — pinned to top safe area
        m_topConstraint = m_searchBar.TopAnchor.ConstraintEqualTo(
            view.SafeAreaLayoutGuide.TopAnchor);

        // Start at bottom
        m_bottomConstraint.Active = true;
        m_topConstraint.Active = false;

        NSLayoutConstraint.ActivateConstraints([
            m_searchBar.LeadingAnchor.ConstraintEqualTo(view.SafeAreaLayoutGuide.LeadingAnchor),
            m_searchBar.TrailingAnchor.ConstraintEqualTo(view.SafeAreaLayoutGuide.TrailingAnchor)
        ]);
    }

    private void TranslateToTop()
    {
        if (m_searchBar == null || m_isActive) return;
        m_isActive = true;

        m_searchBar.ShowsCancelButton = true;
        m_searchBar.SetShowsCancelButton(true, true);

        // Switch constraints: deactivate bottom, activate top
        m_bottomConstraint!.Active = false;
        m_topConstraint!.Active = true;

        UIView.Animate(0.35, 0, UIViewAnimationOptions.CurveEaseInOut, () =>
        {
            m_searchBar.Superview?.LayoutIfNeeded();
        }, () =>
        {
            m_searchBar?.BecomeFirstResponder();
        });
    }

    private void TranslateToBottom()
    {
        if (m_searchBar == null || !m_isActive) return;
        m_isActive = false;

        m_searchBar.ResignFirstResponder();
        m_searchBar.Text = string.Empty;
        m_searchBar.SetShowsCancelButton(false, true);

        // Switch constraints: deactivate top, activate bottom
        m_topConstraint!.Active = false;
        m_bottomConstraint!.Active = true;

        UIView.Animate(0.35, 0, UIViewAnimationOptions.CurveEaseInOut, () =>
        {
            m_searchBar.Superview?.LayoutIfNeeded();
        }, null);
    }

    // IUISearchBarDelegate — text changes
    [Export("searchBar:textDidChange:")]
    public void TextChanged(UISearchBar searchBar, string searchText)
    {
        if (!m_weakBehavior.TryGetTarget(out var behavior))
            return;

        var oldText = m_previousText;
        m_previousText = searchText;
        behavior.OnNativeSearchTextChanged(searchText, oldText);
    }

    // IUISearchBarDelegate — search bar activated (tapped)
    [Export("searchBarTextDidBeginEditing:")]
    public void OnEditingStarted(UISearchBar searchBar)
    {
        TranslateToTop();
    }

    // IUISearchBarDelegate — cancel button
    [Export("searchBarCancelButtonClicked:")]
    public void CancelButtonClicked(UISearchBar searchBar)
    {
        if (m_weakBehavior.TryGetTarget(out var behavior))
        {
            var oldText = m_previousText;
            m_previousText = string.Empty;
            behavior.OnNativeSearchTextChanged(string.Empty, oldText);
        }

        TranslateToBottom();
    }

    public void Focus()
    {
        TranslateToTop();
    }

    public void Unfocus()
    {
        TranslateToBottom();
    }

    public void Cleanup()
    {
        m_searchBar?.ResignFirstResponder();
        m_searchBar?.RemoveFromSuperview();
        m_searchBar = null;
        m_bottomConstraint = null;
        m_topConstraint = null;
    }
}
