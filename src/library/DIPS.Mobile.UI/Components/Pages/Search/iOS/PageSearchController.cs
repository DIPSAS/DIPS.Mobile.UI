using CoreGraphics;
using Foundation;
using UIKit;
using Colors = DIPS.Mobile.UI.Resources.Colors.Colors;
using DIPS.Mobile.UI.Resources.Colors;
using DIPS.Mobile.UI.Resources.Sizes;

namespace DIPS.Mobile.UI.Components.Pages.Search;

/// <summary>
/// Provides a search experience for ContentPage with a pill-shaped search trigger at the bottom
/// and a full-screen search mode overlay when activated.
/// When the search pill is tapped, the page "transforms" into search mode with the search field
/// at the top and the keyboard visible.
/// </summary>
internal class PageSearchController : NSObject, IUISearchBarDelegate
{
    private readonly WeakReference<SearchBehavior> m_weakBehavior;
    private string m_previousText = string.Empty;

    private UIView? m_searchPill;
    private UIView? m_searchOverlay;
    private UISearchBar? m_searchBar;
    private WeakReference<UIViewController>? m_weakViewController;

    public PageSearchController(SearchBehavior behavior)
    {
        m_weakBehavior = new WeakReference<SearchBehavior>(behavior);
    }

    /// <summary>
    /// Sets up the bottom search pill and the full-screen search overlay on the given view controller.
    /// </summary>
    public void SetupInViewController(UIViewController viewController)
    {
        m_weakViewController = new WeakReference<UIViewController>(viewController);
        var view = viewController.View;
        if (view == null) return;

        // Create the pill-shaped search trigger at the bottom
        m_searchPill = CreateSearchPill(view);
        view.AddSubview(m_searchPill);

        // Position pill at bottom with constraints
        m_searchPill.TranslatesAutoresizingMaskIntoConstraints = false;
        var pillMargin = (nfloat)Sizes.GetSize(SizeName.content_margin_medium);
        NSLayoutConstraint.ActivateConstraints([
            m_searchPill.LeadingAnchor.ConstraintEqualTo(view.SafeAreaLayoutGuide.LeadingAnchor, pillMargin),
            m_searchPill.TrailingAnchor.ConstraintEqualTo(view.SafeAreaLayoutGuide.TrailingAnchor, -pillMargin),
            m_searchPill.BottomAnchor.ConstraintEqualTo(view.SafeAreaLayoutGuide.BottomAnchor, -pillMargin),
            m_searchPill.HeightAnchor.ConstraintEqualTo(48)
        ]);

        // Create the full-screen search overlay (initially hidden)
        m_searchOverlay = CreateSearchOverlay(view);
        m_searchOverlay.Alpha = 0;
        m_searchOverlay.Hidden = true;
        view.AddSubview(m_searchOverlay);

        m_searchOverlay.TranslatesAutoresizingMaskIntoConstraints = false;
        NSLayoutConstraint.ActivateConstraints([
            m_searchOverlay.TopAnchor.ConstraintEqualTo(view.TopAnchor),
            m_searchOverlay.LeadingAnchor.ConstraintEqualTo(view.LeadingAnchor),
            m_searchOverlay.TrailingAnchor.ConstraintEqualTo(view.TrailingAnchor),
            m_searchOverlay.BottomAnchor.ConstraintEqualTo(view.BottomAnchor)
        ]);
    }

    private UIView CreateSearchPill(UIView parentView)
    {
        var pill = new UIView
        {
            BackgroundColor = Colors.GetColor(ColorName.color_surface_default).ToPlatform(),
        };
        pill.Layer.CornerRadius = 24;
        pill.Layer.BorderWidth = 1;
        pill.Layer.BorderColor = Colors.GetColor(ColorName.color_border_default).ToCGColor();

        // Search icon
        var searchIcon = new UIImageView(UIImage.GetSystemImage("magnifyingglass"))
        {
            TintColor = Colors.GetColor(ColorName.color_text_default).ToPlatform(),
            ContentMode = UIViewContentMode.ScaleAspectFit,
            TranslatesAutoresizingMaskIntoConstraints = false
        };
        pill.AddSubview(searchIcon);

        // Placeholder label
        var placeholderLabel = new UILabel
        {
            Text = "Search",
            TextColor = Colors.GetColor(ColorName.color_text_subtle).ToPlatform(),
            Font = UIFont.SystemFontOfSize(16),
            TranslatesAutoresizingMaskIntoConstraints = false
        };
        pill.AddSubview(placeholderLabel);

        NSLayoutConstraint.ActivateConstraints([
            searchIcon.LeadingAnchor.ConstraintEqualTo(pill.LeadingAnchor, 16),
            searchIcon.CenterYAnchor.ConstraintEqualTo(pill.CenterYAnchor),
            searchIcon.WidthAnchor.ConstraintEqualTo(20),
            searchIcon.HeightAnchor.ConstraintEqualTo(20),

            placeholderLabel.LeadingAnchor.ConstraintEqualTo(searchIcon.TrailingAnchor, 12),
            placeholderLabel.CenterYAnchor.ConstraintEqualTo(pill.CenterYAnchor),
            placeholderLabel.TrailingAnchor.ConstraintEqualTo(pill.TrailingAnchor, -16)
        ]);

        // Tap gesture to open search mode
        var tapGesture = new UITapGestureRecognizer(() => ShowSearchMode());
        pill.AddGestureRecognizer(tapGesture);

        return pill;
    }

    private UIView CreateSearchOverlay(UIView parentView)
    {
        var overlay = new UIView
        {
            BackgroundColor = Colors.GetColor(ColorName.color_surface_default).ToPlatform()
        };

        // Top bar with search bar and cancel button
        var topBar = new UIView
        {
            TranslatesAutoresizingMaskIntoConstraints = false,
            BackgroundColor = Colors.GetColor(ColorName.color_surface_default).ToPlatform()
        };
        overlay.AddSubview(topBar);

        m_searchBar = new UISearchBar
        {
            SearchBarStyle = UISearchBarStyle.Minimal,
            AutocapitalizationType = UITextAutocapitalizationType.None,
            TranslatesAutoresizingMaskIntoConstraints = false,
            Delegate = this,
            ShowsCancelButton = true
        };
        topBar.AddSubview(m_searchBar);

        NSLayoutConstraint.ActivateConstraints([
            topBar.TopAnchor.ConstraintEqualTo(overlay.SafeAreaLayoutGuide.TopAnchor),
            topBar.LeadingAnchor.ConstraintEqualTo(overlay.LeadingAnchor),
            topBar.TrailingAnchor.ConstraintEqualTo(overlay.TrailingAnchor),
            topBar.HeightAnchor.ConstraintEqualTo(56),

            m_searchBar.TopAnchor.ConstraintEqualTo(topBar.TopAnchor),
            m_searchBar.LeadingAnchor.ConstraintEqualTo(topBar.LeadingAnchor, 8),
            m_searchBar.TrailingAnchor.ConstraintEqualTo(topBar.TrailingAnchor, -8),
            m_searchBar.BottomAnchor.ConstraintEqualTo(topBar.BottomAnchor)
        ]);

        return overlay;
    }

    private void ShowSearchMode()
    {
        if (m_searchOverlay == null || m_searchPill == null) return;

        m_searchOverlay.Hidden = false;

        UIView.Animate(0.3, () =>
        {
            m_searchOverlay!.Alpha = 1;
            m_searchPill!.Alpha = 0;
        }, () =>
        {
            m_searchBar?.BecomeFirstResponder();
        });
    }

    private void HideSearchMode()
    {
        if (m_searchOverlay == null || m_searchPill == null) return;

        m_searchBar?.ResignFirstResponder();

        UIView.Animate(0.3, () =>
        {
            m_searchOverlay!.Alpha = 0;
            m_searchPill!.Alpha = 1;
        }, () =>
        {
            m_searchOverlay!.Hidden = true;
        });
    }

    // IUISearchBarDelegate
    [Export("searchBar:textDidChange:")]
    public void TextChanged(UISearchBar searchBar, string searchText)
    {
        if (!m_weakBehavior.TryGetTarget(out var behavior))
            return;

        var oldText = m_previousText;
        m_previousText = searchText;
        behavior.OnNativeSearchTextChanged(searchText, oldText);
    }

    [Export("searchBarCancelButtonClicked:")]
    public void CancelButtonClicked(UISearchBar searchBar)
    {
        searchBar.Text = string.Empty;

        if (m_weakBehavior.TryGetTarget(out var behavior))
        {
            var oldText = m_previousText;
            m_previousText = string.Empty;
            behavior.OnNativeSearchTextChanged(string.Empty, oldText);
        }

        HideSearchMode();
    }

    public void Focus()
    {
        ShowSearchMode();
    }

    public void Unfocus()
    {
        HideSearchMode();
    }

    public void Cleanup()
    {
        m_searchBar?.ResignFirstResponder();
        m_searchBar = null;
        m_searchOverlay?.RemoveFromSuperview();
        m_searchOverlay = null;
        m_searchPill?.RemoveFromSuperview();
        m_searchPill = null;
        m_weakViewController = null;
    }
}
