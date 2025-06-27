namespace DIPS.Mobile.UI.Components.TabView;

public partial class TabView : ContentView
{
    private void OnItemsSourceChanged()
    {
#if __IOS__
        ItemsSourceChanged();
#endif
#if __ANDROID__
        //UpdateItemsSource();
#endif
    }
    
    private void SetTabToggledBasedOnSelectedItem()
    {
#if __IOS__
        SetTabToggled();
#endif
#if __ANDROID__
        
#endif
    }
}