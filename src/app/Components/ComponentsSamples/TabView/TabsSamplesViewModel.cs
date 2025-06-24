using DIPS.Mobile.UI.MVVM;

namespace Components.ComponentsSamples.TabView;

public class TabsSamplesViewModel : ViewModel
{
    private string m_selectedItem = "Tab 1";
    private List<string>? m_tabItemsTexts = ["Tab 1", "Tab 2"];

    public TabsSamplesViewModel()
    {
    }
    

    public string? SelectedItem
    {
        get => m_selectedItem;
        set => RaiseWhenSet(ref m_selectedItem, value);
    }
    
    public List<string> TabItemsTexts
    {
        get => m_tabItemsTexts ??= new List<string> { "Tab 1", "Tab 2" };
        set => RaiseWhenSet(ref m_tabItemsTexts, value);
    }
}