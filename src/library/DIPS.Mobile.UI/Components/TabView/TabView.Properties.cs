using System.Collections;

namespace DIPS.Mobile.UI.Components.TabView;

public partial class TabView
{
    public Object? SelectedItem
    {
        get => (Object?)GetValue(SelectedItemProperty);
        set => SetValue(SelectedItemProperty, value);
    }

    public List<Tabs.Tab>? ItemsSource
    {
        get => (List<Tabs.Tab>?)GetValue(ItemsSourceProperty);
        set => SetValue(ItemsSourceProperty, value);
    }
    public event EventHandler<TabViewEventArgs>? OnSelectedItemChanged;
    
    public class TabViewEventArgs : EventArgs
    {
        public TabViewEventArgs(TabViewItem selectedItem)
        {
            SelectedItem = selectedItem;
        }
    
        public TabViewItem SelectedItem { get; }
    }
    public string? ItemDisplayProperty { get; set; }

    public static readonly BindableProperty SelectedItemProperty = BindableProperty.Create(
        nameof(SelectedItem),
        typeof(Object),
        typeof(TabView),
        defaultValue: null,
        defaultBindingMode: BindingMode.TwoWay,
        propertyChanged: (bindable, _, _) => ((TabView)bindable).SetTabToggledBasedOnSelectedItem());
    
    public static readonly BindableProperty ItemsSourceProperty = BindableProperty.Create(
        nameof(ItemsSource),
        typeof(List<Tabs.Tab>),
        typeof(TabView),
        defaultValueCreator:(bindable => new List<Tabs.Tab>()),
        propertyChanged: (bindable, _, _) => ((TabView)bindable).OnItemsSourceChanged());
}