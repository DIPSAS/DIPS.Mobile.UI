using System.Collections;
using System.Windows.Input;

namespace DIPS.Mobile.UI.Components.Pickers.MultiItemsPicker;

public partial class MultiItemsPicker
{
    public BottomSheetPickerConfiguration BottomSheetPickerConfiguration { get; set; } = new();
    
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
        typeof(IEnumerable),
        typeof(MultiItemsPicker), propertyChanged: (bindable, value, newValue) => ((MultiItemsPicker)bindable).OnSelectedItemsChanged(), defaultBindingMode: BindingMode.TwoWay);

    /// <summary>
    /// The items that was selected by people when using the picker.
    /// </summary>
    public IEnumerable? SelectedItems
    {
        get => (IEnumerable)GetValue(SelectedItemsProperty);
        set => SetValue(SelectedItemsProperty, value);
    }
    
    /// <summary>
    /// The name of the property of <see cref="SelectedItems"/> to use when displaying the  item for people in the bottom sheet and in the page.
    /// </summary>
    /// <remarks>
    /// When this is not set, it fall back to <code>.ToString()</code> of the <see cref="SelectedItems"/>.
    /// This is used when people search for the item.
    /// </remarks>
    public string? ItemDisplayProperty { get; set; }
    
    public static readonly BindableProperty SelectedItemsCommandProperty = BindableProperty.Create(
        nameof(SelectedItemsCommand),
        typeof(ICommand),
        typeof(MultiItemsPicker));

    /// <summary>
    /// The command to be executed when people select / deselect an item from the picker.
    /// </summary>
    /// <remarks>The command parameter is <see cref="SelectedItems"/></remarks>
    public ICommand? SelectedItemsCommand
    {
        get => (ICommand)GetValue(SelectedItemsCommandProperty);
        set => SetValue(SelectedItemsCommandProperty, value);
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
    
    /// <summary>
    /// Opens the picker.
    /// </summary>
    public ICommand OpenCommand { get; }
}