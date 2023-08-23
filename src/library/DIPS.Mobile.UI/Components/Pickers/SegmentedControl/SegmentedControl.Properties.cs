using System.Collections;
using System.Windows.Input;
using DIPS.Mobile.UI.Components.BottomSheets;
using Colors = DIPS.Mobile.UI.Resources.Colors.Colors;

namespace DIPS.Mobile.UI.Components.Pickers.SegmentedControl;

public partial class SegmentedControl
{
    /// <summary>
    /// The event to be raised when people select an item from the <see cref="SegmentedControl"/>.
    /// </summary>
    public event EventHandler<object>? DidSelectItem;

    /// <summary>
    /// The event to be raised when people de-select an item from the <see cref="SegmentedControl"/>.
    /// </summary>
    public event EventHandler<object>? DidDeSelectItem;

    /// <summary>
    /// The command to be executed when people select an item from the picker.
    /// </summary>
    public ICommand? SelectedItemCommand
    {
        get => (ICommand)GetValue(SelectedItemCommandProperty);
        set => SetValue(SelectedItemCommandProperty, value);
    }

    /// <summary>
    /// The command to be executed when people de-select an item from the picker.
    /// </summary>
    public ICommand DeSelectedItemCommand
    {
        get => (ICommand)GetValue(DeSelectedItemCommandProperty);
        set => SetValue(DeSelectedItemCommandProperty, value);
    }

    /// <summary>
    /// The <see cref="SelectionMode"/> to control how many segments people can pick from.
    /// </summary>
    public SelectionMode SelectionMode
    {
        get => (SelectionMode)GetValue(SelectionModeProperty);
        set => SetValue(SelectionModeProperty, value);
    }

    /// <summary>
    /// The list of items that people can select from when using the <see cref="SegmentedControl"/>.
    /// </summary>
    public IEnumerable? ItemsSource
    {
        get => (IEnumerable)GetValue(ItemsSourceProperty);
        set => SetValue(ItemsSourceProperty, value);
    }

    /// <summary>
    /// The name of the property of <see cref="SelectedItem"/> to use when displaying the item for people in the <see cref="SegmentedControl"/>.
    /// </summary>
    /// <remarks>
    /// When this is not set, it fall back to <code>.ToString()</code> of the <see cref="SelectedItem"/>./>
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
    /// The color of the items that is de-selected for people to see.
    /// </summary>
    public Color DeSelectedColor
    {
        get => (Color)GetValue(DeSelectedColorProperty);
        set => SetValue(DeSelectedColorProperty, value);
    }

    /// <summary>
    /// The color of the border for the segments in the <see cref="SegmentedControl"/>
    /// </summary>
    public Color SegmentBorderColor
    {
        get => (Color)GetValue(SegmentBorderColorProperty);
        set => SetValue(SegmentBorderColorProperty, value);
    }
    
    /// <summary>
    /// The item that was selected by people when using the picker.
    /// </summary>
    /// /// <remarks>Will in use when <see cref="SelectionMode"/> is <see cref="SelectionMode.Single"/></remarks>
    public object? SelectedItem
    {
        get => GetValue(SelectedItemProperty);
        set => SetValue(SelectedItemProperty, value);
    }
    
    /// <summary>
    /// The items that was selected by people when using the picker.
    /// </summary>
    /// <remarks>Only active when <see cref="SelectionMode"/> is <see cref="SelectionMode.Multi"/></remarks>
    public IEnumerable? SelectedItems
    {
        get => (IEnumerable)GetValue(SelectedItemsProperty);
        set => SetValue(SelectedItemsProperty, value);
    }

    public static readonly BindableProperty DeSelectedItemCommandProperty = BindableProperty.Create(
        nameof(DeSelectedItemCommand),
        typeof(ICommand),
        typeof(SegmentedControl));

    public static readonly BindableProperty SelectedItemCommandProperty = BindableProperty.Create(
        nameof(SelectedItemCommand),
        typeof(ICommand),
        typeof(SegmentedControl));

    public static readonly BindableProperty SelectionModeProperty = BindableProperty.Create(
        nameof(SelectionMode),
        typeof(SelectionMode),
        typeof(SegmentedControl));

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

    public static readonly BindableProperty DeSelectedColorProperty = BindableProperty.Create(
        nameof(DeSelectedColor),
        typeof(Color),
        typeof(SegmentedControl),
        defaultValue: Colors.GetColor(ColorName.color_system_white));
    
    public static readonly BindableProperty SegmentBorderColorProperty = BindableProperty.Create(
        nameof(SegmentBorderColor),
        typeof(Color),
        typeof(SegmentedControl), defaultValue:Colors.GetColor(ColorName.color_neutral_30));
}