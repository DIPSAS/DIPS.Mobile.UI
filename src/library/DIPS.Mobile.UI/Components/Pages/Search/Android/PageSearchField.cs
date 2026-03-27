using Android.Content;
using Android.Text;
using Android.Views;
using Android.Views.InputMethods;
using AView = Android.Views.View;
using MaterialSearchBar = Google.Android.Material.Search.SearchBar;
using MaterialSearchView = Google.Android.Material.Search.SearchView;
using FrameLayout = Android.Widget.FrameLayout;

namespace DIPS.Mobile.UI.Components.Pages.Search;

/// <summary>
/// A Material 3 search component for ContentPage using the actual
/// <see cref="MaterialSearchBar"/> and <see cref="MaterialSearchView"/> components.
/// The SearchBar provides the collapsed pill-shaped search trigger at the bottom,
/// while the SearchView provides the full-screen expanded search mode.
/// The SearchView is attached to the root decor view so it can properly
/// overlay the entire page when activated.
/// </summary>
internal class PageSearchField : Java.Lang.Object, ITextWatcher, MaterialSearchView.ITransitionListener
{
    private readonly WeakReference<SearchBehavior> m_weakBehavior;
    private MaterialSearchBar? m_searchBar;
    private MaterialSearchView? m_searchView;
    private string m_previousText = string.Empty;

    public PageSearchField(Context context, SearchBehavior behavior)
    {
        m_weakBehavior = new WeakReference<SearchBehavior>(behavior);

        // Material 3 SearchBar (pill-shaped search trigger) — placed at bottom of page
        m_searchBar = new MaterialSearchBar(context);
        m_searchBar.LayoutParameters = new FrameLayout.LayoutParams(
            ViewGroup.LayoutParams.MatchParent,
            ViewGroup.LayoutParams.WrapContent);

        // Material 3 SearchView (full-screen expanded search mode)
        // Added to root decor view so it overlays the entire page
        m_searchView = new MaterialSearchView(context);
        m_searchView.LayoutParameters = new FrameLayout.LayoutParams(
            ViewGroup.LayoutParams.MatchParent,
            ViewGroup.LayoutParams.MatchParent);

        // Connect SearchView to SearchBar for proper M3 transitions
        m_searchView.SetupWithSearchBar(m_searchBar);

        // Listen for text changes on the SearchView's EditText
        m_searchView.EditText.AddTextChangedListener(this);

        // Listen for transition events (show/hide)
        m_searchView.AddTransitionListener(this);
    }

    /// <summary>
    /// The pill-shaped SearchBar to be placed at the bottom of the page.
    /// </summary>
    public AView SearchBarView => m_searchBar!;

    /// <summary>
    /// The full-screen SearchView to be attached to the root decor view.
    /// </summary>
    public AView SearchViewView => m_searchView!;

    public void Focus()
    {
        m_searchView?.Show();
        m_searchView?.EditText?.RequestFocus();
        var imm = (InputMethodManager?)m_searchView?.EditText?.Context?.GetSystemService(Context.InputMethodService);
        imm?.ShowSoftInput(m_searchView!.EditText, ShowFlags.Implicit);
    }

    public void Unfocus()
    {
        var imm = (InputMethodManager?)m_searchView?.EditText?.Context?.GetSystemService(Context.InputMethodService);
        imm?.HideSoftInputFromWindow(m_searchView?.EditText?.WindowToken, 0);
        m_searchView?.Hide();
    }

    // ITextWatcher implementation
    public void AfterTextChanged(IEditable? s)
    {
        var newText = s?.ToString() ?? string.Empty;

        if (!m_weakBehavior.TryGetTarget(out var behavior))
            return;

        behavior.OnNativeSearchTextChanged(newText, m_previousText);
        m_previousText = newText;
    }

    public void BeforeTextChanged(Java.Lang.ICharSequence? s, int start, int count, int after)
    {
    }

    public void OnTextChanged(Java.Lang.ICharSequence? s, int start, int before, int count)
    {
    }

    // MaterialSearchView.ITransitionListener implementation
    public void OnStateChanged(MaterialSearchView searchView, MaterialSearchView.TransitionState previousState, MaterialSearchView.TransitionState newState)
    {
    }

    public void Cleanup()
    {
        if (m_searchView != null)
        {
            m_searchView.EditText?.RemoveTextChangedListener(this);
            m_searchView.RemoveTransitionListener(this);
        }

        // Remove SearchView from its parent (root decor view)
        if (m_searchView?.Parent is ViewGroup searchViewParent)
            searchViewParent.RemoveView(m_searchView);

        // Remove SearchBar from its parent
        if (m_searchBar?.Parent is ViewGroup searchBarParent)
            searchBarParent.RemoveView(m_searchBar);

        m_searchBar = null;
        m_searchView = null;
    }
}
