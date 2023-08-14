using DIPS.Mobile.UI.Components.Navigation.FloatingNavigationButton.Android;
using DIPS.Mobile.UI.Extensions.Android;
using Java.Lang;
using Microsoft.Maui.Platform;

// ReSharper disable once CheckNamespace
namespace DIPS.Mobile.UI.Components.Navigation.FloatingNavigationButton;

public partial class FloatingNavigationButtonService
{
    private static async partial void AttachToRootWindow(FloatingNavigationButton fab)
    {
        var fragment = new FloatingNavigationButtonMenuFragment(fab);

        // Small delay so that FragmentManager is initialized
        await Task.Delay(10);
        var fragmentManager = Platform.CurrentActivity!.GetFragmentManager();
        try
        {

            fragmentManager!.BeginTransaction()
                .Add(global::Android.Resource.Id.Content, fragment, FloatingNavigationButtonIdentifier.ToString())
                .Commit();
        }
        catch (IllegalStateException ignored) //https://stackoverflow.com/a/27854077, to reproduce this : Debug (start) the app but keep the phone locked (blackscreen).
        {
            fragmentManager!.BeginTransaction()
                .Add(global::Android.Resource.Id.Content, fragment, FloatingNavigationButtonIdentifier.ToString())
                .CommitAllowingStateLoss();
            // There's no way to avoid getting this if saveInstanceState has already been called.
        }
    }

    private static partial void PlatformRemove()
    {
        Platform.CurrentActivity!.GetFragmentManager()!.RemoveFragmentWithTag(FloatingNavigationButtonIdentifier.ToString());
    }
}