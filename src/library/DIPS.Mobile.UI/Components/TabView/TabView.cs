using Microsoft.Maui.Layouts;

namespace DIPS.Mobile.UI.Components.TabView;

public partial class TabView : ContentView
{
    private TabViewItem m_selectedItem = new TabViewItem(new DIPS.Mobile.UI.Components.Tabs.Tab(), new object());
    private readonly FlexLayout m_flexLayout = new () { Wrap = FlexWrap.Wrap, Direction = FlexDirection.Row, AlignItems = FlexAlignItems.Start };
    private readonly List<TabViewItem> m_tabItems = [];

    public TabView()
    { 
        Content = m_flexLayout;
    }

    protected override void OnHandlerChanging(HandlerChangingEventArgs args)
    {
        base.OnHandlerChanging(args);
        
        if(args.NewHandler is not null)
            SetTabToggledBasedOnSelectedItem();
    }

    private void SetTabToggledBasedOnSelectedItem()
    {
        if (m_selectedItem is null)
        {
            m_selectedItem.Tab.IsSelected = false;
            m_selectedItem = null;
            return;
        }
        
        // var tabItem = m_tabItems.FirstOrDefault(tabItem => tabItem.Obj.GetPropertyValue(ItemDisplayProperty)!.Equals(selectedItem.GetPropertyValue(ItemDisplayProperty)));
        // tabItem!.Tab.IsSelected = true;
        // TabToggled(tabItem!, false);
    }

    private void OnItemsSourceChanged()
    {
        ClearItems();
        var list = ItemsSource?.Cast<object?>().ToList();
        if (list is null)
        {
            return;
        }

        var count = list.Count;
        if (count is 0)
        {
            return;
        }

        list.ForEach(obj =>
        {
            var tab = new DIPS.Mobile.UI.Components.Tabs.Tab
            {
                Title = obj.GetPropertyValue(ItemDisplayProperty)?.ToString() ?? string.Empty,
                Counter = list.Count.ToString()
            };
            var item = new TabViewItem(tab, obj!);
            tab.Command = new Command(_ => TabToggled(item));

            m_tabItems.Add(item);
            m_flexLayout.Add(tab);
        });
    }
    
    private void TabToggled(TabViewItem tabViewItem, bool didTap = true)
    {
        if (m_selectedItem.Tab.Title.Equals(tabViewItem.Tab.Title))
        {
            m_selectedItem.Tab.IsSelected = true;
            return;
        }
        
        m_selectedItem.Tab.IsSelected = false;
        m_selectedItem = tabViewItem;
        m_selectedItem.Tab.IsSelected = true;
    }

    private void ClearItems()
    {
        m_tabItems.Clear();
        m_flexLayout.Children.Clear();
    }
}

public class TabViewItem
{
    public TabViewItem(DIPS.Mobile.UI.Components.Tabs.Tab tab, object obj)
    {
        Tab = tab;
        Obj = obj;
    }

    public DIPS.Mobile.UI.Components.Tabs.Tab Tab { get; }
    public object Obj { get; }
}