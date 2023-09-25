using Android.OS;
using Android.Views;
using DIPS.Mobile.UI.API.Library;
using Microsoft.Maui.Platform;
using Fragment = Android.App.Fragment;
using View = Android.Views.View;

namespace DIPS.Mobile.UI.Components.Navigation.FloatingNavigationButton.Android;

internal class FloatingNavigationButtonMenuFragment : AndroidX.Fragment.App.Fragment
{
    private readonly FloatingNavigationButton m_fab;

    public FloatingNavigationButtonMenuFragment(FloatingNavigationButton fab)
    {
        m_fab = fab;
    }
    
    public override View OnCreateView(LayoutInflater inflater, ViewGroup? container, Bundle? savedInstanceState)
    {
        var view =  m_fab.ToPlatform(DUI.GetCurrentMauiContext!);
        return view;
    }
    
}