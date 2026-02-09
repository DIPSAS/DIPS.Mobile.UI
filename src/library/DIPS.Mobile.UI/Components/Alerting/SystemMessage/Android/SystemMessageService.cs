using AndroidX.Fragment.App;
using DIPS.Mobile.UI.API.Library;
using DIPS.Mobile.UI.Components.Alerting.SystemMessage.Android;
using DIPS.Mobile.UI.Extensions.Android;
using Java.Lang;
using Microsoft.Maui.Platform;

namespace DIPS.Mobile.UI.Components.Alerting.SystemMessage;
public static partial class SystemMessageService
{
    private static async partial void PlatformShow(SystemMessage systemMessage)
    {
        var fragment = new SystemMessageFragment(systemMessage);

        // Small delay so that FragmentManager is initialized
        await Task.Delay(10);

        var isModal = Microsoft.Maui.Controls.Shell.Current.Navigation.ModalStack.Count > 0;
        var fragmentManager = isModal ? FragmentLifeCycleCallback.CurrentFragmentManager : Platform.CurrentActivity!.GetFragmentManager();
        
        try
        {
            fragmentManager!.BeginTransaction()
                .Add(global::Android.Resource.Id.Content, fragment, SystemMessageTagId.ToString())
                .Commit();
        }
        catch (IllegalStateException ignored) //https://stackoverflow.com/a/27854077, to reproduce this : Debug (start) the app but keep the phone locked (blackscreen).
        {
            fragmentManager!.BeginTransaction()
                .Add(global::Android.Resource.Id.Content, fragment, SystemMessageTagId.ToString())
                .CommitAllowingStateLoss();
        }
    }

    private static partial void PlatformRemove()
    {
        Platform.CurrentActivity!.GetFragmentManager()!.RemoveFragmentWithTag(SystemMessageTagId.ToString());
    }
}