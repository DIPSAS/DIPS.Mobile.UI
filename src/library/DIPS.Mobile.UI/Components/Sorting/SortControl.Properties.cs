using System.Collections;
using System.Windows.Input;

namespace DIPS.Mobile.UI.Components.Sorting;

public partial class SortControl
{
    public static readonly BindableProperty ItemsSourceProperty = BindableProperty.Create(
        nameof(ItemsSource),
        typeof(IEnumerable),
        typeof(SortControl));

    public static readonly BindableProperty DoneCommandProperty = BindableProperty.Create(
        nameof(DoneCommand),
        typeof(ICommand),
        typeof(SortControl));

    public string ItemDisplayProperty { get; set; }

    private static readonly BindableProperty CurrentSortOrderProperty = BindableProperty.Create(
        nameof(CurrentSortOrder),
        typeof(SortOrder),
        typeof(SortControl));

    internal SortOrder CurrentSortOrder
    {
        get => (SortOrder)GetValue(CurrentSortOrderProperty);
        set => SetValue(CurrentSortOrderProperty, value);
    }

    public ICommand DoneCommand
    {
        get => (ICommand)GetValue(DoneCommandProperty);
        set => SetValue(DoneCommandProperty, value);
    }
    
    public IEnumerable ItemsSource
    {
        get => (IEnumerable)GetValue(ItemsSourceProperty);
        set => SetValue(ItemsSourceProperty, value);
    }
}