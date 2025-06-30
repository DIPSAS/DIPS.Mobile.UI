using Tab = DIPS.Mobile.UI.Components.Tabs.Tab;

namespace DIPS.Mobile.UI.Components.TabView;

public partial class TabView : ContentView
{
    private Tab? m_selectedItem;

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

    private void SetTabToggled()
    {
        var selectedItem = m_tabItems.FirstOrDefault();
        if (SelectedItem is not null) selectedItem = new Tab() { Title = SelectedItem.Title, Counter = SelectedItem?.Counter is null || SelectedItem.Counter == 0 ? "" : SelectedItem.Counter.ToString() };;
        
         var tabItem = m_tabItems.FirstOrDefault(tabItem => tabItem.Title == selectedItem.Title);
         if (tabItem is null) return;
         tabItem.IsSelected = true;
        
         m_selectedItem = tabItem;
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
            SelectedItem = new TabItem(){ Title = m_selectedItem.Title, Counter = String.IsNullOrEmpty(m_selectedItem?.Counter) ? null : int.Parse(m_selectedItem.Counter) };
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
    }
}