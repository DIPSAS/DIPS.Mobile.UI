namespace DIPS.Mobile.UI.Components.Pickers;

public class BottomSheetConfiguration : BindableObject
{
    public static readonly BindableProperty HasSearchBarProperty = BindableProperty.Create(
        nameof(HasSearchBar),
        typeof(bool),
        typeof(BottomSheetConfiguration), defaultValue: true);

    /// <summary>
    /// Determines if a search bar should be visible when the picker is visible for people when in bottom sheet mode.
    /// </summary>
    public bool HasSearchBar
    {
        get => (bool)GetValue(HasSearchBarProperty);
        set => SetValue(HasSearchBarProperty, value);
    }

    public ControlTemplate SelectableItemTemplate { get; set; }
}