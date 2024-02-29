using System.Collections;

namespace DIPS.Mobile.UI.Components.ChipGroup;

public partial class ChipGroup
{
    public static readonly BindableProperty ItemsSourceProperty = BindableProperty.Create(
        nameof(ItemsSource),
        typeof(IEnumerable),
        typeof(ChipGroup),
        propertyChanged: (bindable, _, _) => ((ChipGroup)bindable).OnItemsSourceChanged());


    public static readonly BindableProperty SelectionModeProperty = BindableProperty.Create(
        nameof(SelectionMode),
        typeof(ChipGroupSelectionMode),
        typeof(ChipGroup));


    public static readonly BindableProperty SelectedItemsProperty = BindableProperty.Create(
        nameof(SelectedItems),
        typeof(IEnumerable),
        typeof(ChipGroup),
        defaultBindingMode: BindingMode.TwoWay);
    
    public string? ItemDisplayProperty { get; set; }
    public IEnumerable? SelectedItems
    {
        get => (IEnumerable?)GetValue(SelectedItemsProperty);
        set => SetValue(SelectedItemsProperty, value);
    }

    public ChipGroupSelectionMode SelectionMode
    {
        get => (ChipGroupSelectionMode)GetValue(SelectionModeProperty);
        set => SetValue(SelectionModeProperty, value);
    }

    public IEnumerable? ItemsSource
    {
        get => (IEnumerable?)GetValue(ItemsSourceProperty);
        set => SetValue(ItemsSourceProperty, value);
    }
}

public enum ChipGroupSelectionMode
{
    Single,
    Multi
}