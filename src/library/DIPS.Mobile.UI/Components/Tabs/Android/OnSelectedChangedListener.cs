using Google.Android.Material.Tabs;

namespace DIPS.Mobile.UI.Components.Tabs.Android;

internal class OnSelectedChangedListener
{
    private readonly TabHandler m_handler;
    
    public OnSelectedChangedListener(TabHandler handler)
    {
        m_handler = handler;
    }
    
    public void OnTabSelected(TabLayout.Tab? tab) {
        // Handle tab select
    }

    public void OnTabReselected(TabLayout.Tab? tab) {
        // Handle tab reselect
    }

    public void OnTabUnselected(TabLayout.Tab? tab) {
        // Handle tab unselect
    }
}
