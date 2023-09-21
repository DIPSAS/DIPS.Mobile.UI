using System.Windows.Input;

namespace DIPS.Mobile.UI.Components.Sorting;

public partial class SortControl
{
    public string ItemDisplayProperty { get; set; }

    internal SortOrder CurrentSortOrder
    {
        get => m_currentSortOrder;
        private set
        {
            m_currentSortOrder = value;
            OnSortOrderChanged();
        }
    }

    internal object? SelectedItem
    {
        get => m_selectedItem;
        private set
        {
            m_selectedItem = value;
            OnSelectedItemChanged();   
        }
    }
    
    public static readonly BindableProperty HasHapticsProperty = BindableProperty.Create(
        nameof(HasHaptics),
        typeof(bool),
        typeof(SortControl), defaultValue:true);

    /// <summary>
    /// Determines if the phone should stimulate the sense of touch and motion by the use of vibration when people select sort options.
    /// </summary>
    public bool HasHaptics
    {
        get => (bool)GetValue(HasHapticsProperty);
        set => SetValue(HasHapticsProperty, value);
    }

    public object? InitialSelectedItem
    {
        get => (object)GetValue(InitialSelectedItemProperty);
        set => SetValue(InitialSelectedItemProperty, value);
    }

    public SortOrder? InitialSortOrder
    {
        get => (SortOrder)GetValue(InitialSortOrderProperty);
        set => SetValue(InitialSortOrderProperty, value);
    }
    
    public Command<(object, SortOrder)> SelectedItemCommand
    {
        get => (Command<(object, SortOrder)>)GetValue(SelectedItemCommandProperty);
        set => SetValue(SelectedItemCommandProperty, value);
    }
    
    public IEnumerable<object>? ItemsSource
    {
        get => (IEnumerable<object>)GetValue(ItemsSourceProperty);
        set => SetValue(ItemsSourceProperty, value);
    }
    
    public static readonly BindableProperty ItemsSourceProperty = BindableProperty.Create(
            nameof(ItemsSource),
            typeof(IEnumerable<object>),
            typeof(SortControl),
            propertyChanged: (bindable, _, _) => ((SortControl)bindable).OnItemsSourceChanged());
    
    public static readonly BindableProperty SelectedItemCommandProperty = BindableProperty.Create(
        nameof(SelectedItemCommand),
        typeof(Command<(object, SortOrder)>),
        typeof(SortControl));
    
    public static readonly BindableProperty InitialSelectedItemProperty = BindableProperty.Create(
        nameof(InitialSelectedItem),
        typeof(object),
        typeof(SortControl),
        propertyChanged: (bindable, _, newValue) => ((SortControl)bindable).SelectedItem = newValue);
    
    public static readonly BindableProperty InitialSortOrderProperty = BindableProperty.Create(
        nameof(InitialSortOrder),
        typeof(SortOrder),
        typeof(SortControl),
        propertyChanged: (bindable, _, newValue) => ((SortControl)bindable).CurrentSortOrder = (SortOrder)newValue);
}