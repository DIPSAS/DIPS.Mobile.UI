namespace DIPS.Mobile.UI.Components.TabView;

[ContentProperty(nameof(ItemsSource))]
public partial class TabView : ContentView
{
    
    protected override void OnHandlerChanging(HandlerChangingEventArgs args)
    {
        base.OnHandlerChanging(args);

        if (args.NewHandler is not null)
        {
            OnItemsSourceChanged();
#if __IOS__
            SetTabToggled();   
#endif
        }
    }
    
    private void OnItemsSourceChanged()
    {
        foreach (var tab in ItemsSource)
        {
            tab.BindingContext = BindingContext;
        }
#if __IOS__
        ItemsSourceChanged();
#endif
    }

    internal void SetSelectedTabIndex(int index)
    {
        OnSelectedTabIndexChanged?.Invoke(this, new TabViewEventArgs(index));
        SelectedTabIndex = index;
    }
}

public class TabItem : BindableObject
{
    public static readonly BindableProperty CounterProperty =
        BindableProperty.Create(
            nameof(Counter),
            typeof(int?),
            typeof(TabItem),
            defaultValue: null,
            defaultBindingMode: BindingMode.TwoWay);

    public int? Counter
    {
        get => (int?)GetValue(CounterProperty);
        set => SetValue(CounterProperty, value);
    }

    public static readonly BindableProperty TitleProperty =
        BindableProperty.Create(
            nameof(Title),
            typeof(string),
            typeof(TabItem),
            defaultValue: string.Empty);

    public string Title
    {
        get => (string)GetValue(TitleProperty);
        set => SetValue(TitleProperty, value);
    }
}