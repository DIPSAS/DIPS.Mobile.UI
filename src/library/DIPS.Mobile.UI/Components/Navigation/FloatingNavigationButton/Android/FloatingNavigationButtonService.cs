using DIPS.Mobile.UI.Components.Navigation.FloatingNavigationButton.Android;
using DIPS.Mobile.UI.Extensions.Android;
using DIPS.Mobile.UI.Internal.Logging;
using Java.IO;
using Java.Lang;
using Microsoft.Maui.Platform;
using Exception = System.Exception;

// ReSharper disable once CheckNamespace
namespace DIPS.Mobile.UI.Components.Navigation.FloatingNavigationButton;

public partial class FloatingNavigationButtonService
{
    private static async partial void AttachToRootWindow(FloatingNavigationButton fab)
    {
        // Small delay so that FragmentManager is initialized
        await Task.Delay(10);
        var fragmentManager = Platform.CurrentActivity!.GetFragmentManager();
        if (fragmentManager == null)
        {
            DUILogService.LogError(nameof(FloatingNavigationButtonService), $"{nameof(fragmentManager)} was null, this is not good.");
            return;
        }

        if (fragmentManager.FindFragmentByTag(FloatingNavigationButtonIdentifier.ToString()) != null)
        {
            DUILogService.LogDebug(nameof(FloatingNavigationButtonService), $"Fragment is already added to FragmentManager.");
            return;
        }
        var fragment = new FloatingNavigationButtonMenuFragment(fab);   
        try
        {
            fragmentManager!.BeginTransaction()
                .Add(global::Android.Resource.Id.Content, fragment, FloatingNavigationButtonIdentifier.ToString())
                .Commit();
        }
        catch (Exception e) //https://stackoverflow.com/a/27854077, to reproduce this : Debug (start) the app but keep the phone locked (blackscreen).
        {
            DUILogService.LogError(nameof(FloatingNavigationButtonService), e.Message);
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