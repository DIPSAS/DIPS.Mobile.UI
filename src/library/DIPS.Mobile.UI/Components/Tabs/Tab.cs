namespace DIPS.Mobile.UI.Components.Tabs;

public partial class Tab : ContentView
{
    private static Style? m_tabSelectedStyle;
    private void OnIsSelectedChanged()
    {
        if (!IsSelected)
        {
            if (m_tabSelectedStyle is not null)
            {
                Style = m_tabSelectedStyle;
            }
        }
        else
        {
            m_tabSelectedStyle = Style;
            Style = Selected;
        }
    }
    
    private static Style Selected =>
        new(typeof(Components.Tabs.Tab))
        {

        };
}
