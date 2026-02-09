using Android.Views;
using Android.Widget;
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
        // Small delay so that FragmentManager is initialized
        await Task.Delay(10);

        var isModal = Microsoft.Maui.Controls.Shell.Current.Navigation.ModalStack.Count > 0;
        
        // For modals, add the view directly to the dialog window to appear on top
        if (isModal && FragmentLifeCycleCallback.CurrentDialogFragment?.Dialog?.Window?.DecorView is ViewGroup decorView)
        {
            var systemMessageView = systemMessage.ToPlatform(DUI.GetCurrentMauiContext!);
            systemMessageView.Tag = SystemMessageTagId.ToString();
            systemMessageView.Elevation = 100f; // Ensure it's on top
            
            decorView.Post(() =>
            {
                decorView.AddView(systemMessageView, new ViewGroup.LayoutParams(
                    ViewGroup.LayoutParams.MatchParent,
                    ViewGroup.LayoutParams.WrapContent));
            });
        }
        else
        {
            // For non-modal pages, use fragment transaction as before
            var fragment = new SystemMessageFragment(systemMessage);
            var fragmentManager = Platform.CurrentActivity!.GetFragmentManager();
            
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
    }

    private static partial void PlatformRemove()
    {
        var isModal = Microsoft.Maui.Controls.Shell.Current.Navigation.ModalStack.Count > 0;
        
        if (isModal && FragmentLifeCycleCallback.CurrentDialogFragment?.Dialog?.Window?.DecorView is ViewGroup decorView)
        {
            // Remove the view from the dialog window
            var viewToRemove = decorView.FindViewWithTag(SystemMessageTagId.ToString());
            if (viewToRemove != null)
            {
                decorView.RemoveView(viewToRemove);
            }
        }
        else
        {
            // For non-modal pages, remove fragment as before
            Platform.CurrentActivity!.GetFragmentManager()!.RemoveFragmentWithTag(SystemMessageTagId.ToString());
        }
    }
}