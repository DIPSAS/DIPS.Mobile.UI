using System.Collections;
using System.Windows.Input;
using DIPS.Mobile.UI.Components.BottomSheets;
using Colors = DIPS.Mobile.UI.Resources.Colors.Colors;

namespace DIPS.Mobile.UI.Components.Pickers.SegmentedControl;


public partial class SegmentedControl
{
    /// <summary>
    /// The items people can select from when opening the picker.
    /// </summary>
    public IEnumerable? ItemsSource
    {
        get => (IEnumerable)GetValue(ItemsSourceProperty);
        set => SetValue(ItemsSourceProperty, value);
    }

    /// <summary>
    /// The name of the property of <see cref="SelectedItem"/> to use when displaying the  item for people in the picker.
    /// </summary>
    /// <remarks>
    /// When this is not set, it fall back to <code>.ToString()</code> of the <see cref="SelectedItem"/>.
    /// This is used when people search if <see cref="Dlfcn.Mode"/> is set to <see cref="BottomSheet"/>
    /// This is not used if you set a <see cref="BottomSheetPickerConfiguration.SelectableItemTemplate"/>
    /// </remarks>
    public string? ItemDisplayProperty { get; set; }

    /// <summary>
    /// The color of the item that is selected for people to see.
    /// </summary>
    public Color SelectedColor
    {
        get => (Color)GetValue(SelectedColorProperty);
        set => SetValue(SelectedColorProperty, value);
    }

    /// <summary>
    /// The color of the items that is unselected for people to see.
    /// </summary>
    public Color UnSelectedColor
    {
        get => (Color)GetValue(UnSelectedColorProperty);
        set => SetValue(UnSelectedColorProperty, value);
    }

    /// <summary>
    /// The event to be raised when people select an item from the <see cref="SegmentedControl"/>.
    /// </summary>
    public event EventHandler<object>? DidSelectItem;

    /// <summary>
    /// The command to be executed when people select/de-select an item from the picker.
    /// </summary>
    public ICommand SelectedItemCommand
    {
        get => (ICommand)GetValue(SelectedItemCommandProperty);
        set => SetValue(SelectedItemCommandProperty, value);
    }

    /// <summary>
    /// The item that was selected by people when using the picker.
    /// </summary>
    public object? SelectedItem
    {
        get => GetValue(SelectedItemProperty);
        set => SetValue(SelectedItemProperty, value);
    }

    /// <summary>
    /// The list of items that people can select from when using the <see cref="SegmentedControl"/>.
    /// </summary>
    public static readonly BindableProperty ItemsSourceProperty = BindableProperty.Create(
        nameof(ItemsSource),
        typeof(IEnumerable),
        typeof(SegmentedControl),
        propertyChanged: ((bindable, _, _) => ((SegmentedControl)bindable).ItemsSourceChanged()));

    public static readonly BindableProperty SelectedColorProperty = BindableProperty.Create(
        nameof(SelectedColor),
        typeof(Color),
        typeof(SegmentedControl),
        defaultValue: Colors.GetColor(ColorName.color_neutral_20));

    public static readonly BindableProperty UnSelectedColorProperty = BindableProperty.Create(
        nameof(UnSelectedColor),
        typeof(Color),
        typeof(SegmentedControl),
        defaultValue: Colors.GetColor(ColorName.color_system_white));

    public static readonly BindableProperty SelectedItemCommandProperty = BindableProperty.Create(
        nameof(SelectedItemCommand),
        typeof(ICommand),
        typeof(SegmentedControl));

    public static readonly BindableProperty SelectedItemProperty = BindableProperty.Create(
        nameof(SelectedItem),
        typeof(object),
        typeof(SegmentedControl),
        propertyChanged: ((bindable, _, _) => ((SegmentedControl)bindable).SelectedItemChanged()),
        defaultBindingMode: BindingMode.TwoWay);
}