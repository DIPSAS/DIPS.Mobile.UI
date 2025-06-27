using DIPS.Mobile.UI.Resources.Styles;
using DIPS.Mobile.UI.Resources.Styles.Label;
using Microsoft.Maui.Layouts;
using Colors = DIPS.Mobile.UI.Resources.Colors.Colors;
using Tab = DIPS.Mobile.UI.Components.Tabs.Tab;

namespace DIPS.Mobile.UI.Components.TabView;

[ContentProperty(nameof(ItemsSource))]
public partial class TabView : ContentView
{
    private Tab m_selectedItem;

    private ScrollView m_scrollView = new () { Orientation = ScrollOrientation.Horizontal };
    private HorizontalStackLayout m_stackLayout = new(){ 
        Padding = new Thickness(Sizes.GetSize(SizeName.size_3), 0, Sizes.GetSize(SizeName.size_3), 0),            
        Spacing = Sizes.GetSize(SizeName.size_1),
    };
    private readonly List<Tab> m_tabItems = [];

    public TabView()
    { 
        Content = m_scrollView;
        m_scrollView.Content = m_stackLayout;
    }

    protected override void OnHandlerChanging(HandlerChangingEventArgs args)
    {
        base.OnHandlerChanging(args);

        if (args.NewHandler is not null)
        {
            OnItemsSourceChanged();
            SetTabToggledBasedOnSelectedItem();   
        }
    }

    private void SetTabToggledBasedOnSelectedItem()
    {
        var selectedItem = SelectedItem ?? m_tabItems.FirstOrDefault();
        
        var tabItem = m_tabItems.FirstOrDefault(tabItem => tabItem == selectedItem);
        tabItem.IsSelected = true;
        
        m_selectedItem = tabItem;
        TabToggled(tabItem, false);
    }
    
    private void OnItemsSourceChanged()
    {
        ClearItems();
        var list = ItemsSource;
        if (list is null || list.Count == 0)
        {
            return;
        }

        list.ForEach(obj =>
        {
            obj.Tapped += (sender, args) =>
            {
                TabToggled(obj);
            };
            
            var item = obj;
            obj.Command = new Command(_ => TabToggled(item));

            m_tabItems.Add(item);
            m_stackLayout.Add(obj);
        });
    }
    
    private void TabToggled(Tab tabViewItem, bool didTap = true)
    {
        if (m_selectedItem.Equals(tabViewItem) && didTap)
        {
            m_selectedItem.IsSelected = true;
            return;
        }
        m_selectedItem.IsSelected = false;
        
        m_selectedItem = tabViewItem;
        m_selectedItem.IsSelected = true;
        
        if (didTap)
        {
            OnSelectedItemChanged?.Invoke(this, new TabViewEventArgs(m_selectedItem));
            SelectedItem = m_selectedItem;
        }
        else
        {
#if __IOS__
            foreach (var tabItem in m_tabItems)
            {
                tabItem.SetTextStyle(tabItem.IsSelected);

            }
#endif
        }
    }

    private void ClearItems()
    {
        m_tabItems.Clear();
        m_stackLayout.Clear();
    }
}