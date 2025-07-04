using DIPS.Mobile.UI.Resources.LocalizedStrings.LocalizedStrings;
using Tab = DIPS.Mobile.UI.Components.Tabs.Tab;

namespace DIPS.Mobile.UI.Components.TabView;

public partial class TabView : ContentView
{
    private ScrollView m_scrollView = new () { 
        Orientation = ScrollOrientation.Horizontal,     
        HorizontalScrollBarVisibility = ScrollBarVisibility.Never
    };
    private HorizontalStackLayout m_stackLayout = new(){ 
        Padding = new Thickness(0, 0, 0, Sizes.GetSize(SizeName.size_2)),            
        Spacing = Sizes.GetSize(SizeName.size_1),
    };
    private readonly List<Tab> m_tabItems = [];
    private int m_previouslySelectedTabIndex = -1;

    public TabView()
    { 
        Content = m_scrollView;
        m_scrollView.Content = m_stackLayout;
    }

    private void SetTabToggled()
    {
        if (m_previouslySelectedTabIndex == SelectedTabIndex) return;
        
        var tabItem = m_tabItems[SelectedTabIndex];
        tabItem.IsSelected = true;
        
        _ = TabToggled(tabItem, false);
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
            var tab = new Tab() { Title = obj.Title, Counter = obj.Counter };
            tab.Tapped += (sender, args) =>
            {
                _ = TabToggled(tab);
            };
            
            var item = tab;
            
            SemanticProperties.SetHint(tab, DUILocalizedStrings.Accessibility_TapToChangeView);
            var counterString = tab.Counter is null ? "" : ", " + tab.Counter + DUILocalizedStrings.Elements;
            var semanticDescriptionCombinedText = tab.Title + counterString;
            SemanticProperties.SetDescription(tab, semanticDescriptionCombinedText);
            
            m_tabItems.Add(item);
            m_stackLayout.Add(tab);
        });
    }
    
    private async Task TabToggled(Tab tabViewItem, bool didTap = true)
    {
        if (didTap && CanSwitchTab != null)
        {
            if (!await CanSwitchTab(m_previouslySelectedTabIndex))
            {
                return;
            }
        }
        
        if (m_previouslySelectedTabIndex != -1)
        {
            var previousTab = m_tabItems[m_previouslySelectedTabIndex];
            if (previousTab.Equals(tabViewItem) && didTap)
            {
                previousTab.IsSelected = true;
                return;
            }
        
            previousTab.IsSelected = false;
        }
        
        var index = m_tabItems.IndexOf(tabViewItem);
        m_previouslySelectedTabIndex = index;
        
        tabViewItem.IsSelected = true;
        
        if (didTap)
        {
            SetSelectedTabIndex(index);
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
        m_previouslySelectedTabIndex = -1;
    }
}