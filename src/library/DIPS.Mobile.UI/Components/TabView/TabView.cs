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
}

public class TabItem : BindableObject
{
    public string Title { get; set; }
    public int? Counter { get; set; }
}