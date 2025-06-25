using System.Collections.ObjectModel;
using DIPS.Mobile.UI.MVVM;

namespace Components.ComponentsSamples.TabView;

public class TabsSamplesViewModel : ViewModel
{
    private List<TabItem>? m_tabItemsTexts = [];

    public TabsSamplesViewModel()
    {
        TabItemsTexts = new List<TabItem>
        {
            new TabItem { Title = "Tab 1", Counter = "1" },
            new TabItem { Title = "Tab 2", Counter = "2" },
            new TabItem { Title = "Tab 3", Counter = "3" }
        };
    }
    
    public List<TabItem> TabItemsTexts
    {
        get => m_tabItemsTexts ?? [];
        set => RaiseWhenSet(ref m_tabItemsTexts, value);
    }
}

public class TabItem
{
    public string Title { get; set; }
    public string Counter { get; set; }
}