using DIPS.Mobile.UI.Components.Alerts.SystemMessage.Android;

namespace DIPS.Mobile.UI.Components.Alerts.SystemMessage;

public static partial class SystemMessageService
{
    private static async partial void PlatformShow(SystemMessage systemMessage)
    {
        var fragment = new SystemMessageFragment(systemMessage);

        // Small delay so that FragmentManager is initialized
        await Task.Delay(10);
        var fragmentManager = Platform.CurrentActivity.FragmentManager;

        fragmentManager.BeginTransaction()
            .Add(global::Android.Resource.Id.Content, fragment, SystemMessageTagId.ToString())
            .Commit();
    }

    private static partial void PlatformRemove()
    {
        var fragment = Platform.CurrentActivity.FragmentManager.FindFragmentByTag(SystemMessageTagId.ToString());
        if(fragment is not null)
            Platform.CurrentActivity.FragmentManager.BeginTransaction().Remove(fragment).Commit();
    }
}