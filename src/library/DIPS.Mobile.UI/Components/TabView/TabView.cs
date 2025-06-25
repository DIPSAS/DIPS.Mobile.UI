using DIPS.Mobile.UI.Resources.Styles;
using DIPS.Mobile.UI.Resources.Styles.Label;
using Microsoft.Maui.Layouts;
using Colors = DIPS.Mobile.UI.Resources.Colors.Colors;

namespace DIPS.Mobile.UI.Components.TabView;

public partial class TabView : ContentView
{
    private TabViewItem m_selectedItem;

    private ScrollView m_scrollView = new () { Orientation = ScrollOrientation.Horizontal };
    private HorizontalStackLayout m_stackLayout = new();
    private readonly List<TabViewItem> m_tabItems = [];

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
            SetTabToggledBasedOnSelectedItem();   
        }
    }

    private void SetTabToggledBasedOnSelectedItem()
    {
        var selectedItem = SelectedItem ?? m_tabItems.FirstOrDefault();
        var test = m_selectedItem;
        
        var tabItem = m_tabItems.FirstOrDefault(tabItem => tabItem == selectedItem);
        tabItem.Tab.IsSelected = true;
        
        m_selectedItem = tabItem;
        TabToggled(tabItem, false);
    }
    
    private void OnItemsSourceChanged()
    {
        ClearItems();
        var list = ItemsSource?.Cast<object?>().ToList();
        if (list is null || list.Count == 0)
        {
            return;
        }

        list.ForEach(obj =>
        {
            var tab = new DIPS.Mobile.UI.Components.Tabs.Tab
            {
                Title = obj.GetPropertyValue(ItemDisplayProperty)?.ToString() ?? string.Empty,
                Counter = list.Count.ToString(),
            };
            tab.Tapped += (sender, args) =>
            {
                var item = new TabViewItem(tab, obj!);
                TabToggled(item);
            };
            
            var item = new TabViewItem(tab, obj!);
            tab.Command = new Command(_ => TabToggled(item));

            m_tabItems.Add(item);
            m_stackLayout.Add(tab);
        });
    }
    
    private void TabToggled(TabViewItem tabViewItem, bool didTap = true)
    {
        if (m_selectedItem.Obj.Equals(tabViewItem.Obj))
        {
            m_selectedItem.Tab.IsSelected = true;
            return;
        } else
        {
            m_selectedItem.Tab.IsSelected = false;
        }
        tabViewItem.Tab.TextColor = Colors.GetColor(ColorName.color_text_action);
        tabViewItem.Tab.TextStyle = Styles.GetLabelStyle(LabelStyle.UI300);
        foreach (var tabItem in m_tabItems)
        {
            if (tabItem != tabViewItem)
            {
                tabItem.Tab.TextColor = Colors.GetColor(ColorName.color_text_default);
                tabViewItem.Tab.TextStyle = Styles.GetLabelStyle(LabelStyle.Body200);   
            }
        }
        m_selectedItem = tabViewItem;
        m_selectedItem.Tab.IsSelected = true;
        
        if (didTap)
        {
            OnSelectedItemChanged?.Invoke(this, new TabViewEventArgs(m_selectedItem));
            SelectedItem = m_selectedItem;
        }
    }

    private void ClearItems()
    {
        m_tabItems.Clear();
        m_stackLayout.Clear();
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