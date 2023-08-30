using Android.App;
using Java.Lang;
using FragmentManager = AndroidX.Fragment.App.FragmentManager;

namespace DIPS.Mobile.UI.Extensions.Android;

public static class FragmentManagerExtensions
{
    public static void RemoveFragmentWithTag(this FragmentManager fragmentManager, string tag)
    {
        var fragment = fragmentManager.FindFragmentByTag(tag);
        if (fragment is null)
        {
            return;
        }

        try
        {
            fragmentManager.BeginTransaction().Remove(fragment).Commit();
        }
        catch (IllegalStateException ignored) //https://stackoverflow.com/a/27854077, to reproduce this : Debug (start) the app but keep the phone locked (blackscreen).
        {
            fragmentManager.BeginTransaction().Remove(fragment).CommitAllowingStateLoss();
        }

    }
}