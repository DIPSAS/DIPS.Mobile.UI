using System.Windows.Input;

namespace DIPS.Mobile.UI.Components.Pages.Search;

public partial class SearchBehavior
{
    /// <summary>
    /// The command to be executed when the text in the search field is changed.
    /// The command parameter is the new search text (string).
    /// </summary>
    public ICommand? SearchCommand
    {
        get => (ICommand?)GetValue(SearchCommandProperty);
        set => SetValue(SearchCommandProperty, value);
    }

    /// <summary>
    /// Determines whether the search field should be automatically focused when the page appears.
    /// </summary>
    public bool ShouldAutoFocus
    {
        get => (bool)GetValue(ShouldAutoFocusProperty);
        set => SetValue(ShouldAutoFocusProperty, value);
    }

    /// <summary>
    /// Event raised when the text in the search field is changed.
    /// </summary>
    public event EventHandler<TextChangedEventArgs>? SearchTextChanged;

    public static readonly BindableProperty SearchCommandProperty = BindableProperty.Create(
        nameof(SearchCommand),
        typeof(ICommand),
        typeof(SearchBehavior));

    public static readonly BindableProperty ShouldAutoFocusProperty = BindableProperty.Create(
        nameof(ShouldAutoFocus),
        typeof(bool),
        typeof(SearchBehavior));
}
