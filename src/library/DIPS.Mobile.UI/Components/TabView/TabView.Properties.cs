namespace DIPS.Mobile.UI.Components.TabView;

public partial class TabView
{
    public Object? SelectedItem
    {
        get => (Object?)GetValue(SelectedItemProperty);
        set => SetValue(SelectedItemProperty, value);
    }
        
    public static readonly BindableProperty SelectedItemProperty = BindableProperty.Create(
        nameof(SelectedItem),
        typeof(Object),
        typeof(TabView),
        defaultValue: new object(),
        defaultBindingMode: BindingMode.TwoWay,
        propertyChanged: (bindable, _, _) => ((TabView)bindable).SetTabToggledBasedOnSelectedItem());
}