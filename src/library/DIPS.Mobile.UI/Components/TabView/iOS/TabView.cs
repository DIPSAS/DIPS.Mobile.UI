using Tab = DIPS.Mobile.UI.Components.Tabs.Tab;

namespace DIPS.Mobile.UI.Components.TabView;

public partial class TabView : ContentView
{
    private ScrollView m_scrollView = new () { 
        Orientation = ScrollOrientation.Horizontal,     
        HorizontalScrollBarVisibility = ScrollBarVisibility.Never
    };
    private HorizontalStackLayout m_stackLayout = new(){ 
        Padding = new Thickness(Sizes.GetSize(SizeName.size_3), 0, Sizes.GetSize(SizeName.size_3), 0),            
        Spacing = Sizes.GetSize(SizeName.size_1),
    };
    private readonly List<Tab> m_tabItems = [];
    private int m_previouslySelectedTabIndex = 0;

    public TabView()
    { 
        Content = m_scrollView;
        m_scrollView.Content = m_stackLayout;
    }

    private void SetTabToggled()
    {
        var selectedItem = m_tabItems.FirstOrDefault();
        if (SelectedTabIndex is not null)
        {
            selectedItem = m_tabItems[(int)SelectedTabIndex];
        }
        
        var tabItem = m_tabItems.FirstOrDefault(tabItem => tabItem.Title == selectedItem.Title);
        if (tabItem is null) return;
        tabItem.IsSelected = true;
        
        TabToggled(tabItem, false);
    }
    
    private void ItemsSourceChanged()
    {
        ClearItems();
        var list = ItemsSource;
        if (list is null || list.Count == 0)
        {
            return;
        }

        list.ForEach(obj =>
        {
            var tab = new Tab() { Title = obj.Title, Counter = obj?.Counter is null || obj.Counter == 0 ? "" : obj.Counter.ToString() };
            tab.Tapped += (sender, args) =>
            {
                TabToggled(tab);
            };
            
            var item = tab;
            tab.Command = new Command(_ => TabToggled(item));
            
            m_tabItems.Add(item);
            m_stackLayout.Add(tab);
        });
    }
    
    private void TabToggled(Tab tabViewItem, bool didTap = true)
    {
        var previousTab = m_tabItems[m_previouslySelectedTabIndex];
        if (previousTab.Equals(tabViewItem) && didTap)
        {
            previousTab.IsSelected = true;
            return;
        }
        
        if (SelectedTabIndex is null) return;
        
        previousTab.IsSelected = false;
        var index = m_tabItems.IndexOf(tabViewItem);
        m_previouslySelectedTabIndex = index;
        
        var selectedItem = tabViewItem;
        selectedItem.IsSelected = true;
        
        if (didTap)
        {
            OnSelectedItemChanged?.Invoke(this, new TabViewEventArgs(index));
            SelectedTabIndex = index;
        }
        SetTextStyleForAllTabs();
    }
    
    private void SetTextStyleForAllTabs()
    {
        foreach (var tabItem in m_tabItems)
        {
            var textStyle = tabItem.IsSelected ? SelectedTextStyle : DefaultTextStyle;
            var textColor = tabItem.IsSelected ? SelectedTextColor : DefaultTextColor;
            tabItem.SetTextStyle(textStyle, textColor);
        }
    }

    private void ClearItems()
    {
        m_tabItems.Clear();
        m_stackLayout.Clear();
        m_previouslySelectedTabIndex = 0;
    }
}