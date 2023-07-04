using AndroidX.Fragment.App;
using DIPS.Mobile.UI.Components.Alerting.SystemMessage.Android;
using DIPS.Mobile.UI.Extensions.Android;
using Microsoft.Maui.Platform;

namespace DIPS.Mobile.UI.Components.Alerting.SystemMessage;
public static partial class SystemMessageService
{
    private static async partial void PlatformShow(SystemMessage systemMessage)
    {
        var fragment = new SystemMessageFragment(systemMessage);

        // Small delay so that FragmentManager is initialized
        await Task.Delay(10);
        var fragmentManager = Platform.CurrentActivity!.GetFragmentManager();
        

        fragmentManager!.BeginTransaction()
            .Add(global::Android.Resource.Id.Content, fragment, SystemMessageTagId.ToString())
            .Commit();
    }

    private static partial void PlatformRemove()
    {
        Platform.CurrentActivity!.GetFragmentManager()!.RemoveFragmentWithTag(SystemMessageTagId.ToString());
    }
}