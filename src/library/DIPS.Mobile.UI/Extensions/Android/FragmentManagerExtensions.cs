using Android.App;
using FragmentManager = AndroidX.Fragment.App.FragmentManager;

namespace DIPS.Mobile.UI.Extensions.Android;

public static class FragmentManagerExtensions
{
    public static void RemoveFragmentWithTag(this FragmentManager fragmentManager, string tag)
    {
        var fragment = fragmentManager.FindFragmentByTag(tag);
        if (fragment is not null)
            fragmentManager.BeginTransaction().Remove(fragment).Commit();
    }
}