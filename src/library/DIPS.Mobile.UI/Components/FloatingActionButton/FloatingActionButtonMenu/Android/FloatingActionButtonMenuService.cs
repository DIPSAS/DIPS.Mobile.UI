using DIPS.Mobile.UI.Components.FloatingActionButton.FloatingActionButtonMenu.Android;

// ReSharper disable once CheckNamespace
namespace DIPS.Mobile.UI.Components.FloatingActionButton.FloatingActionButtonMenu;

public partial class FloatingActionButtonMenuService
{
    public static partial void Create()
    {
        var fragment = new FloatingActionButtonMenuFragment();

        var fragmentManager = Platform.CurrentActivity.FragmentManager;

        fragmentManager.BeginTransaction()
            .Add(global::Android.Resource.Id.Content, fragment)
            .Commit();
    }

    public static partial void Hide()
    {
        
    }
    
}