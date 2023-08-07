using System.Collections;
using System.Windows.Input;

namespace DIPS.Mobile.UI.Components.Pickers.MultiItemsPicker;

public partial class MultiItemsPicker
{
    public static readonly BindableProperty ContainerCornerRadiusProperty = BindableProperty.Create(
        nameof(ContainerCornerRadius),
        typeof(CornerRadius),
        typeof(MultiItemsPicker));

    public CornerRadius ContainerCornerRadius
    {
        get => (CornerRadius)GetValue(ContainerCornerRadiusProperty);
        set => SetValue(ContainerCornerRadiusProperty, value);
    }

    public static readonly BindableProperty ItemsSourceProperty = BindableProperty.Create(
        nameof(ItemsSource),
        typeof(IEnumerable),
        typeof(ItemPicker.ItemPicker));

    /// <summary>
    /// The items people can select from when opening the picker.
    /// </summary>
    public IEnumerable? ItemsSource
    {
        get => (IEnumerable)GetValue(ItemsSourceProperty);
        set => SetValue(ItemsSourceProperty, value);
    }

    public static readonly BindableProperty SelectedItemsProperty = BindableProperty.Create(
        nameof(SelectedItems),
        typeof(IEnumerable<object>),
        typeof(MultiItemsPicker), propertyChanged: (bindable, value, newValue) => ((MultiItemsPicker)bindable).OnSelectedItemsChanged());

    /// <summary>
    /// The items that was selected by people when using the picker.
    /// </summary>
    public IEnumerable<object>? SelectedItems
    {
        get => (IEnumerable<object>)GetValue(SelectedItemsProperty);
        set => SetValue(SelectedItemsProperty, value);
    }

    public static readonly BindableProperty HasSearchBarProperty = BindableProperty.Create(
        nameof(HasSearchBar),
        typeof(bool),
        typeof(MultiItemsPicker), defaultValue: true);

    /// <summary>
    /// Determines if a search bar should be visible when the picker is visible for people.
    /// </summary>
    public bool HasSearchBar
    {
        get => (bool)GetValue(HasSearchBarProperty);
        set => SetValue(HasSearchBarProperty, value);
    }
    
    /// <summary>
    /// The name of the property of <see cref="SelectedItems"/> to use when displaying the  item for people in the bottom sheet and in the page.
    /// </summary>
    /// <remarks>
    /// When this is not set, it fall back to <code>.ToString()</code> of the <see cref="SelectedItems"/>.
    /// This is used when people search for the item.
    /// </remarks>
    public string? ItemDisplayProperty { get; set; }
    
    public static readonly BindableProperty SelectedItemCommandProperty = BindableProperty.Create(
        nameof(SelectedItemCommand),
        typeof(ICommand),
        typeof(MultiItemsPicker));

    /// <summary>
    /// The command to be executed when people select / deselect an item from the picker.
    /// </summary>
    public ICommand? SelectedItemCommand
    {
        get => (ICommand)GetValue(SelectedItemCommandProperty);
        set => SetValue(SelectedItemCommandProperty, value);
    }
    
    /// <summary>
    /// The event to be raised when people select an item from the picker.
    /// </summary>
    public event EventHandler<object>? DidSelectItem;
    
    /// <summary>
    /// The event to be raised when people de-select an item from the picker.
    /// </summary>
    public event EventHandler<object>? DidDeSelectItem;

    public static readonly BindableProperty PlaceholderProperty = BindableProperty.Create(
        nameof(Placeholder),
        typeof(string),
        typeof(MultiItemsPicker));

    /// <summary>
    /// The place holder for people to see when they have not selected a item.
    /// </summary>
    public string Placeholder
    {
        get => (string)GetValue(PlaceholderProperty);
        set => SetValue(PlaceholderProperty, value);
    }

    public ICommand OpenCommand { get; }
}