namespace DIPS.Mobile.UI.Components.Pickers;

public class BottomSheetPickerConfiguration : BindableObject
{
    public static readonly BindableProperty HasSearchBarProperty = BindableProperty.Create(
        nameof(HasSearchBar),
        typeof(bool),
        typeof(BottomSheetPickerConfiguration), defaultValue: true);

    /// <summary>
    /// Determines if a search bar should be visible when the picker is visible for people when in bottom sheet mode.
    /// </summary>
    public bool HasSearchBar
    {
        get => (bool)GetValue(HasSearchBarProperty);
        set => SetValue(HasSearchBarProperty, value);
    }
    
    /// <summary>
    /// Determines if the items to pick from is replaced by a view that indicates activity.
    /// </summary>
    public static readonly BindableProperty IsBusyProperty = BindableProperty.Create(
        nameof(IsBusy),
        typeof(bool),
        typeof(BottomSheetPickerConfiguration));

    public bool IsBusy
    {
        get => (bool)GetValue(IsBusyProperty);
        set => SetValue(IsBusyProperty, value);
    }

    public ControlTemplate SelectableItemTemplate { get; set; }
}