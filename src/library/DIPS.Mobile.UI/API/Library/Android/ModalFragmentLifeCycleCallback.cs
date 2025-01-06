using Android.Views;
using AndroidX.Fragment.App;
using Microsoft.Maui.Controls.PlatformConfiguration;

namespace DIPS.Mobile.UI.API.Library;

public class ModalFragmentLifeCycleCallback : FragmentManager.FragmentLifecycleCallbacks
{
    public override void OnFragmentStarted(FragmentManager fm, Fragment f)
    {
        SetStatusBarColor(f);
        
        base.OnFragmentStarted(fm, f);
    }

    private void SetStatusBarColor(Fragment f)
    {
        if (f is DialogFragment dialogFragment)
        {
            var activity = Platform.CurrentActivity;

            if (activity is null)
                return;

            var statusBarColor = activity.Window!.StatusBarColor;
            var platformColor = new Android.Graphics.Color(statusBarColor);

            var dialog = dialogFragment.Dialog;

            var window = dialog.Window;
            dialog.Window.SetStatusBarColor(platformColor);

            bool isColorTransparent = platformColor == Android.Graphics.Color.Transparent;
            if (isColorTransparent)
            {
                window.ClearFlags(WindowManagerFlags.DrawsSystemBarBackgrounds);
                window.SetFlags(WindowManagerFlags.LayoutNoLimits, WindowManagerFlags.LayoutNoLimits);
            }
            else
            {
                window.ClearFlags(WindowManagerFlags.LayoutNoLimits);
                window.SetFlags(WindowManagerFlags.DrawsSystemBarBackgrounds, WindowManagerFlags.DrawsSystemBarBackgrounds);
            }

            window.SetDecorFitsSystemWindows(!isColorTransparent);
        }
    }
}