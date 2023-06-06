using Android.OS;
using Android.Views;
using DIPS.Mobile.UI.API.Library;
using Microsoft.Maui.Platform;
using Fragment = Android.App.Fragment;
using View = Android.Views.View;

namespace DIPS.Mobile.UI.Components.Navigation.FloatingNavigationButton.Android;

internal class FloatingNavigationButtonMenuFragment : Fragment
{
    private readonly Navigation.FloatingNavigationButton.FloatingNavigationButton m_fab;

    public FloatingNavigationButtonMenuFragment(Navigation.FloatingNavigationButton.FloatingNavigationButton fab)
    {
        m_fab = fab;
    }
    
    public override View OnCreateView(LayoutInflater inflater, ViewGroup? container, Bundle? savedInstanceState)
    {
        return m_fab.ToPlatform(DUI.GetCurrentMauiContext!);
    }
    
}