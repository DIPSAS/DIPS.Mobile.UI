using DIPS.Mobile.UI.Components.FloatingActionButton.FloatingActionButtonMenu.Android;

// ReSharper disable once CheckNamespace
namespace DIPS.Mobile.UI.Components.FloatingActionButton.FloatingActionButtonMenu;

public partial class FloatingActionButtonMenuService
{
    public static async partial void AttachToRootWindow(FloatingActionButtonMenu fab)
    {
        var fragment = new FloatingActionButtonMenuFragment(fab);

        // Small delay so that FragmentManager is initialized
        await Task.Delay(10);
        var fragmentManager = Platform.CurrentActivity.FragmentManager;

        fragmentManager.BeginTransaction()
            .Add(global::Android.Resource.Id.Content, fragment)
            .Commit();

    }

    public static partial void Hide()
    {
        
    }
    
}