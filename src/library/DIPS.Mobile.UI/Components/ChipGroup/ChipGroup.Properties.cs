using System.Collections;
using System.Windows.Input;

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
        defaultValue: new List<object>(),
        defaultBindingMode: BindingMode.TwoWay,
        propertyChanged: (bindable, _, _) => ((ChipGroup)bindable).OnSelectedItemsHasBeenChanged());
    
    public event EventHandler<ChipGroupEventArgs>? OnSelectedItemsChanged;
    
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

public class ChipGroupEventArgs : EventArgs
{
    public ChipGroupEventArgs(IEnumerable selectedItems)
    {
        SelectedItems = selectedItems;
    }
    
    public IEnumerable SelectedItems { get; }
}