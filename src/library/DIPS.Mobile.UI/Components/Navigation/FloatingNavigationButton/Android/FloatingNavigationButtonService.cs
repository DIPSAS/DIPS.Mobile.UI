using DIPS.Mobile.UI.Components.Navigation.FloatingNavigationButton.Android;
using DIPS.Mobile.UI.Extensions.Android;
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

        fragmentManager!.BeginTransaction()
            .Add(global::Android.Resource.Id.Content, fragment, FloatingNavigationButtonIdentifier.ToString())
            .Commit();

    }

    private static partial void PlatformRemove()
    {
        Platform.CurrentActivity!.GetFragmentManager()!.RemoveFragmentWithTag(FloatingNavigationButtonIdentifier.ToString());
    }
}