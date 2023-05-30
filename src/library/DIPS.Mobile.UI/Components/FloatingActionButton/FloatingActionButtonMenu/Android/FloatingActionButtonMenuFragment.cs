using Android.OS;
using Android.Views;
using Android.Widget;
using AndroidX.Fragment.App;
using DIPS.Mobile.UI.API.Library;
using Microsoft.Maui.Platform;
using Fragment = Android.App.Fragment;
using View = Android.Views.View;

namespace DIPS.Mobile.UI.Components.FloatingActionButton.FloatingActionButtonMenu.Android;

public class FloatingActionButtonMenuFragment : Fragment
{
    private readonly FloatingActionButtonMenu m_fab;

    public FloatingActionButtonMenuFragment(FloatingActionButtonMenu fab)
    {
        m_fab = fab;
    }
    
    public override View OnCreateView(LayoutInflater inflater, ViewGroup? container, Bundle? savedInstanceState)
    {
        return m_fab.ToPlatform(DUI.GetCurrentMauiContext!);
    }
    
}