using System.Collections;
using System.Windows.Input;
using Colors = DIPS.Mobile.UI.Resources.Colors.Colors;

namespace DIPS.Mobile.UI.Components.Pickers.SegmentedControl;

public partial class SegmentedControl
{

    public static readonly BindableProperty HasHapticsProperty = BindableProperty.Create(
        nameof(HasHaptics),
        typeof(bool),
        typeof(SegmentedControl), defaultValue:true);

    /// <summary>
    /// Determines if the phone should stimulate the sense of touch and motion by the use of vibration when people tap the segment of the segmented control
    /// </summary>
    public bool HasHaptics
    {
        get => (bool)GetValue(HasHapticsProperty);
        set => SetValue(HasHapticsProperty, value);
    }
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
    public ICommand? DeSelectedItemCommand
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
    /// This property must be placed before <see cref="ItemsSource"/> is set for it to work properly.
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
    
    /// <summary>
    /// Determines if the item should be de-selected when the same item is tapped again.
    /// </summary>
    /// <remarks>Only active when <see cref="SelectionMode"/> is <see cref="SelectionMode.Single"/></remarks>
    public bool ShouldDeSelectOnSameItemTapped
    {
        get => (bool)GetValue(ShouldDeSelectOnSameItemTappedProperty);
        set => SetValue(ShouldDeSelectOnSameItemTappedProperty, value);
    }
    
    public static readonly BindableProperty ShouldDeSelectOnSameItemTappedProperty = BindableProperty.Create(
        nameof(ShouldDeSelectOnSameItemTapped),
        typeof(bool),
        typeof(SegmentedControl));

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
        // TODO: Wait until we have a semantic color for this
        defaultValue: Colors.GetColor(ColorName.color_neutral_20));

    public static readonly BindableProperty DeSelectedColorProperty = BindableProperty.Create(
        nameof(DeSelectedColor),
        typeof(Color),
        typeof(SegmentedControl),
        defaultValue: Colors.GetColor(ColorName.color_surface_default));

    public static readonly BindableProperty SegmentBorderColorProperty = BindableProperty.Create(
        nameof(SegmentBorderColor),
        typeof(Color),
        typeof(SegmentedControl),
        // TODO: Wait until we have a semantic color for this
        defaultValue: Colors.GetColor(ColorName.color_neutral_30));

    public static readonly BindableProperty SelectedItemProperty = BindableProperty.Create(
        nameof(SelectedItem),
        typeof(object),
        typeof(SegmentedControl),
        propertyChanged: ((bindable, _, _) => ((SegmentedControl)bindable).SelectedItemChanged()),
        defaultBindingMode: BindingMode.TwoWay);

    public static readonly BindableProperty SelectedItemsProperty = BindableProperty.Create(
        nameof(SelectedItems),
        typeof(IEnumerable),
        typeof(SegmentedControl),
        propertyChanged: (bindable, _, _) => ((SegmentedControl)bindable).OnSelectedItemsChanged(),
        defaultBindingMode: BindingMode.TwoWay);
}