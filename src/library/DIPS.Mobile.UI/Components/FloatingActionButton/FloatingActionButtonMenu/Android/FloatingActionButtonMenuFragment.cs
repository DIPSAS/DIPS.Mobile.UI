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
    public FloatingActionButtonMenuFragment()
    {
        
    }
    
    public override View OnCreateView(LayoutInflater inflater, ViewGroup? container, Bundle? savedInstanceState)
    {
        return new FloatingActionButtonMenu().ToPlatform(DUI.GetCurrentMauiContext!);
    }
    
}