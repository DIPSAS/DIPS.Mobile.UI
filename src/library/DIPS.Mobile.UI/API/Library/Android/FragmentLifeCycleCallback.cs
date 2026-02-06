using Android.OS;
using Android.Text;
using Android.Views;
using Android.Widget;
using AndroidX.Core.View;
using AndroidX.Fragment.App;
using DIPS.Mobile.UI.API.Library.Android;
using DIPS.Mobile.UI.Internal.Logging;
using Google.Android.Material.AppBar;
using Google.Android.Material.BottomSheet;
using Microsoft.Maui.Platform;
using Colors = DIPS.Mobile.UI.Resources.Colors.Colors;
using ContentPage = DIPS.Mobile.UI.Components.Pages.ContentPage;
using Shell = DIPS.Mobile.UI.Components.Shell.Shell;
using View = Android.Views.View;

namespace DIPS.Mobile.UI.API.Library;

public class FragmentLifeCycleCallback : FragmentManager.FragmentLifecycleCallbacks
{
    public override void OnFragmentStarted(FragmentManager fm, Fragment f)
    {
        if (f is DialogFragment dialogFragment)
        {
            if (f is not BottomSheetDialogFragment)
            {
                SetColorsOnModal(dialogFragment);
                TryInheritWindowFlags(dialogFragment);
                // Register the DialogFragment so ContentPage can find it
                // Also immediately set the status bar color since OnAppearing may have already been called
                StatusBarHandler.RegisterDialogFragmentForPage(dialogFragment);
            }
            

            TryEnableCustomHideSoftInputOnTappedImplementation(dialogFragment);
        }
     
        base.OnFragmentStarted(fm, f);
    }
    
    public override void OnFragmentStopped(FragmentManager fm, Fragment f)
    {
        if (f is DialogFragment dialogFragment && f is not BottomSheetDialogFragment)
        {
            // Unregister the DialogFragment when it's stopped/dismissed
            StatusBarHandler.UnregisterDialogFragment(dialogFragment);
        }
        
        base.OnFragmentStopped(fm, f);
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
    /// TODO: Workaround: .NET MAUI does not inherit the color from the Shell, so we need to set it manually.
    /// Inspiration from: https://stackoverflow.com/questions/75596420/how-do-i-add-a-listener-to-the-android-toolbar-in-maui/76056039#76056039
    /// Sets the toolbar item's tint color on icon and title
    /// </summary>
    private static void SetColorsOnModal(DialogFragment dialogFragment)
    {
        var linearLayout = dialogFragment.Dialog?.Window?.FindViewById<LinearLayout>(_Microsoft.Android.Resource.Designer.Resource.Id.navigationlayout_appbar);
        
        var child1 = linearLayout?.GetChildAt(0);

        if (child1 is not MaterialToolbar materialToolbar)
            return;
        
        var stateListColor = Colors.GetColor(Shell.ForegroundColorName)
            .ToDefaultColorStateList();
        
        const float shadowDp = 6f;
        var shadowPx = materialToolbar.Context?.Resources?.DisplayMetrics?.Density * shadowDp ?? 0;

        materialToolbar.Elevation = shadowPx; 
        
        for (var i = 0; i < materialToolbar.Menu?.Size(); i++)
        {
            var menuItem = materialToolbar.Menu.GetItem(i);
            menuItem?.SetIconTintList(stateListColor);

            var item = materialToolbar.Menu.GetItem(i);
            var span = new SpannableString(item?.TitleFormatted);
            span.SetSpan(new global::Android.Text.Style.ForegroundColorSpan(Colors.GetColor(Shell.ForegroundColorName).ToPlatform()), 0, span.Length(), 0);
            item?.SetTitle(span);
        }
    }

    private void TryInheritWindowFlags(DialogFragment dialogFragment)
    {
        var activity = Platform.CurrentActivity;
        var window = activity?.Window;
        
        if (window is
            {
                Attributes: not null
            }) //Make sure the dialog inherits window flag from the activity, useful when the activity is set as secured.
        {
            var flags = window.Attributes.Flags;
            dialogFragment.Dialog?.Window?.SetFlags(flags, flags);
        }
    }

#if ANDROID
    public sealed class InsetsListener : Java.Lang.Object, IOnApplyWindowInsetsListener
    {
        readonly View m_scrim;
        public InsetsListener(View scrim) => m_scrim = scrim;

        public WindowInsetsCompat? OnApplyWindowInsets(View? v, WindowInsetsCompat? insets)
        {
            var ins = insets?.GetInsets(WindowInsetsCompat.Type.StatusBars());
            if (ins == null)
            {
                return insets;
            }

            var top = ins.Top;
            var lp = m_scrim.LayoutParameters;
            if (lp == null)
            {
                return insets;
            }

            lp.Height = top;
            m_scrim.LayoutParameters = lp;

            return insets;
        }
    }
#endif
}