using Android.Provider;
using Android.Views;
using Android.Widget;
using AndroidX.Fragment.App;
using Google.Android.Material.AppBar;
using Microsoft.Maui.Controls.PlatformConfiguration;
using Microsoft.Maui.Platform;
using Colors = DIPS.Mobile.UI.Resources.Colors.Colors;
using Shell = DIPS.Mobile.UI.Components.Shell.Shell;

namespace DIPS.Mobile.UI.API.Library;

public class ModalFragmentLifeCycleCallback : FragmentManager.FragmentLifecycleCallbacks
{
    public override void OnFragmentStarted(FragmentManager fm, Fragment f)
    {
        if (f is DialogFragment dialogFragment)
        {
            SetStatusBarColor(dialogFragment);
            SetIconColors(dialogFragment);
        }
        
        base.OnFragmentStarted(fm, f);
    }

    /// <summary>
    /// Inspiration from: https://stackoverflow.com/questions/75596420/how-do-i-add-a-listener-to-the-android-toolbar-in-maui/76056039#76056039
    /// Sets the toolbar item icon's tint color
    /// </summary>
    private void SetIconColors(DialogFragment dialogFragment)
    {
        var linearLayout = dialogFragment.Dialog?.Window?.FindViewById<LinearLayout>(_Microsoft.Android.Resource.Designer.Resource.Id.navigationlayout_appbar);
        
        var child1 = linearLayout?.GetChildAt(0);

        if (child1 is not MaterialToolbar materialToolbar)
            return;
        
        var stateListColor = Colors.GetColor(Shell.ToolbarTitleTextColorName)
            .ToDefaultColorStateList();
                
        for (var i = 0; i < materialToolbar.Menu?.Size(); i++)
        {
            materialToolbar.Menu.GetItem(i)?.SetIconTintList(stateListColor);
        }
    }

    private void SetStatusBarColor(DialogFragment dialogFragment)
    {
        var activity = Platform.CurrentActivity;

        if (activity is null)
            return;

        var statusBarColor = activity.Window!.StatusBarColor;
        var platformColor = new Android.Graphics.Color(statusBarColor);

        var window = dialogFragment.Dialog?.Window;
        if (window is null)
            return;

        dialogFragment.Dialog.Window?.SetStatusBarColor(platformColor);

        var isColorTransparent = platformColor == Android.Graphics.Color.Transparent;
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