using Android.Provider;
using Android.Views;
using Android.Widget;
using AndroidX.CoordinatorLayout.Widget;
using AndroidX.Fragment.App;
using DIPS.Mobile.UI.API.Library.Android;
using DIPS.Mobile.UI.Components.Buttons.Android;
using Google.Android.Material.AppBar;
using Google.Android.Material.BottomSheet;
using Microsoft.Maui.Controls.PlatformConfiguration;
using Microsoft.Maui.Platform;
using Colors = DIPS.Mobile.UI.Resources.Colors.Colors;
using Shell = DIPS.Mobile.UI.Components.Shell.Shell;

namespace DIPS.Mobile.UI.API.Library;

public class FragmentLifeCycleCallback : FragmentManager.FragmentLifecycleCallbacks
{
    public override void OnFragmentStarted(FragmentManager fm, Fragment f)
    {
        if (f is DialogFragment dialogFragment)
        {
            if (f is not BottomSheetDialogFragment)
            {
                SetStatusBarColorOnModalAppearing(dialogFragment);
                SetIconColorsOnModal(dialogFragment);
            }

            TryEnableCustomHideSoftInputOnTappedImplementation(dialogFragment);
        }
     
        base.OnFragmentStarted(fm, f);
    }

    private static void TryEnableCustomHideSoftInputOnTappedImplementation(DialogFragment dialogFragment)
    {
        if(!DUI.ShouldUseCustomHideSoftInputOnTappedImplementation)
            return;
        
        // Enable HideSoftInputOnTapped for Modals and BottomSheet
        // Does not work out of the box in MAUI yet..
        var originalWindow = dialogFragment.Dialog?.Window;
        var originalCallback = originalWindow?.Callback;

        if (originalWindow is not null && originalCallback is not null)
        {
            if (dialogFragment.Dialog is { Window: not null })
            {
                dialogFragment.Dialog.Window.Callback = new UnfocusWindowCallback(originalWindow, originalCallback);
            }
        }
    }


    /// <summary>
    /// Inspiration from: https://stackoverflow.com/questions/75596420/how-do-i-add-a-listener-to-the-android-toolbar-in-maui/76056039#76056039
    /// Sets the toolbar item icon's tint color
    /// </summary>
    private static void SetIconColorsOnModal(DialogFragment dialogFragment)
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

    /// <summary>
    /// To set the status bar color when a modal is shown
    /// MAUI has a bug after rewriting modals to use DialogFragment
    /// Workaround found here: https://github.com/CommunityToolkit/Maui/issues/2370#issuecomment-2552701081 
    /// </summary>
    private static void SetStatusBarColorOnModalAppearing(DialogFragment dialogFragment)
    {
        var activity = Platform.CurrentActivity;

        if (activity is null)
            return;

        var statusBarColor = activity.Window!.StatusBarColor;
        var platformColor = new global::Android.Graphics.Color(statusBarColor);

        var window = dialogFragment.Dialog?.Window;
        if (window is null)
            return;

        dialogFragment.Dialog.Window?.SetStatusBarColor(platformColor);

        var isColorTransparent = platformColor == global::Android.Graphics.Color.Transparent;
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