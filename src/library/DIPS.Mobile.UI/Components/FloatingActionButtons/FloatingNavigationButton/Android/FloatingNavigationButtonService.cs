using DIPS.Mobile.UI.Components.FloatingActionButtons.FloatingNavigationButton.Android;

// ReSharper disable once CheckNamespace
namespace DIPS.Mobile.UI.Components.FloatingActionButtons.FloatingNavigationButton;

public partial class FloatingNavigationButtonService
{
    private static async partial void AttachToRootWindow(FloatingNavigationButton fab)
    {
        var fragment = new FloatingNavigationButtonMenuFragment(fab);

        // Small delay so that FragmentManager is initialized
        await Task.Delay(10);
        var fragmentManager = Platform.CurrentActivity.FragmentManager;

        fragmentManager.BeginTransaction()
            .Add(global::Android.Resource.Id.Content, fragment)
            .Commit();

    }
}