using System.Collections;
using System.Windows.Input;

namespace DIPS.Mobile.UI.Components.Pickers.MultiItemsPicker;

public partial class MultiItemsPicker
{

    public static readonly BindableProperty HasHapticsProperty = BindableProperty.Create(
        nameof(HasHaptics),
        typeof(bool),
        typeof(MultiItemsPicker), defaultValue:true);

    /// <summary>
    /// Determines if the phone should stimulate the sense of touch and motion by the use of vibration when people tap the items.
    /// </summary>
    public bool HasHaptics
    {
        get => (bool)GetValue(HasHapticsProperty);
        set => SetValue(HasHapticsProperty, value);
    }

    public BottomSheetPickerConfiguration BottomSheetPickerConfiguration
    {
        get => m_bottomSheetPickerConfiguration;
        set
        {
            m_bottomSheetPickerConfiguration = value;
            m_bottomSheetPickerConfiguration.SetBinding(BindingContextProperty, static (MultiItemsPicker multiItemsPicker) => multiItemsPicker.BindingContext, source: this);

        }
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

    public static readonly BindableProperty HasDoneButtonBindableProperty = BindableProperty.Create(
        nameof(HasDoneButton),
        typeof(bool),
        typeof(MultiItemsPicker));

    private BottomSheetPickerConfiguration m_bottomSheetPickerConfiguration = new();

    /// <summary>
    /// Whether the bottom sheet should have a "Done" button, which will close the bottom sheet when tapped. Setting
    /// this to true will also disallow people from closing the bottom sheet by dragging it down or tapping outside it,
    /// and also hide the close button in the header.
    /// </summary>
    public bool HasDoneButton
    {
        get => (bool)GetValue(HasDoneButtonBindableProperty);
        set => SetValue(HasDoneButtonBindableProperty, value);
    }
    
    /// <summary>
    /// Opens the picker.
    /// </summary>
    public ICommand OpenCommand { get; }

    /// <summary>
    /// Defines how the picker should handle resetting its selected items. Setting it will make a "Reset"-button appear
    /// in the picker's bottom sheet, which will reset the list. Set the <see cref="ResetBehaviour.Command"/> property
    /// to override the reset method.
    /// </summary>
    public ResetBehaviour? ResetBehaviour { get; set; }
}