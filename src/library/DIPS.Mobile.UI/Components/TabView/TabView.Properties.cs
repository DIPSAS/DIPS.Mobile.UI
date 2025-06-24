using System.Collections;

namespace DIPS.Mobile.UI.Components.TabView;

public partial class TabView
{
    public Object? SelectedItem
    {
        get => (Object?)GetValue(SelectedItemProperty);
        set => SetValue(SelectedItemProperty, value);
    }
    
    public IEnumerable? ItemsSource
    {
        get => (IEnumerable?)GetValue(ItemsSourceProperty);
        set => SetValue(ItemsSourceProperty, value);
    }
        
    public string? ItemDisplayProperty { get; set; }

    public static readonly BindableProperty SelectedItemProperty = BindableProperty.Create(
        nameof(SelectedItem),
        typeof(Object),
        typeof(TabView),
        defaultValue: new object(),
        defaultBindingMode: BindingMode.TwoWay,
        propertyChanged: (bindable, _, _) => ((TabView)bindable).SetTabToggledBasedOnSelectedItem());
    
    public static readonly BindableProperty ItemsSourceProperty = BindableProperty.Create(
        nameof(ItemsSource),
        typeof(IEnumerable),
        typeof(TabView),
        propertyChanged: (bindable, _, _) => ((TabView)bindable).OnItemsSourceChanged());
}