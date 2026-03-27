using System.Windows.Input;

namespace DIPS.Mobile.UI.Components.Pages.Search;

/// <summary>
/// A behavior that adds native platform search to a <see cref="ContentPage"/>.
/// On iOS, this integrates a <c>UISearchController</c> into the navigation bar.
/// On Android, this adds a Material 3 <c>SearchBar</c> + <c>SearchView</c>.
/// </summary>
/// <remarks>
/// Set this on <see cref="ContentPage.SearchBehavior"/> to enable search.
/// The page must be inside a NavigationPage or Shell for iOS navigation bar integration.
/// </remarks>
public partial class SearchBehavior : BindableObject
{
    private WeakReference<ContentPage>? m_weakPage;

    internal void AttachToPage(ContentPage page)
    {
        DetachFromCurrentPage();

        m_weakPage = new WeakReference<ContentPage>(page);
        page.HandlerChanged += OnPageHandlerChanged;

        if (page.Handler != null)
            SetupNativeSearch(page);
    }

    internal void DetachFromCurrentPage()
    {
        if (m_weakPage?.TryGetTarget(out var page) == true)
        {
            page.HandlerChanged -= OnPageHandlerChanged;
            TeardownNativeSearch(page);
        }

        FocusSearchAction = null;
        UnfocusSearchAction = null;
        m_weakPage = null;
    }

    private void OnPageHandlerChanged(object? sender, EventArgs e)
    {
        if (sender is not ContentPage page)
            return;

        if (page.Handler != null)
            SetupNativeSearch(page);
        else
            TeardownNativeSearch(page);
    }

    /// <summary>
    /// Platform-specific setup of native search UI. Implemented in iOS/ and Android/ partial classes.
    /// </summary>
    partial void SetupNativeSearch(ContentPage page);

    /// <summary>
    /// Platform-specific teardown of native search UI. Implemented in iOS/ and Android/ partial classes.
    /// </summary>
    partial void TeardownNativeSearch(ContentPage page);

    /// <summary>
    /// Called by native search controllers when the search text changes.
    /// Routes the change through the existing events and command.
    /// </summary>
    internal void OnNativeSearchTextChanged(string newValue, string oldValue)
    {
        SearchTextChanged?.Invoke(this, new TextChangedEventArgs(oldValue, newValue));
        SearchCommand?.Execute(newValue);
    }

    /// <summary>
    /// Focuses the native search field.
    /// </summary>
    public void FocusSearch() => FocusSearchAction?.Invoke();

    /// <summary>
    /// Unfocuses the native search field.
    /// </summary>
    public void UnfocusSearch() => UnfocusSearchAction?.Invoke();

    internal Action? FocusSearchAction { get; set; }
    internal Action? UnfocusSearchAction { get; set; }
}
