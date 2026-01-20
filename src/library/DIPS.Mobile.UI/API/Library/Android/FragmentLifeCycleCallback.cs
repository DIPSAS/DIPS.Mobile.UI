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
                TryInheritStatusBarColorOnModal(dialogFragment);
                SetColorsOnModal(dialogFragment);
                TryInheritWindowFlags(dialogFragment);
            }

            TryEnableCustomHideSoftInputOnTappedImplementation(dialogFragment);
        }
     
        base.OnFragmentStarted(fm, f);
    }
    
    /// <summary>
    /// In edge-to-edge mode, Android adds a "statusBarBackground" view to the DecorView
    /// This view shows a default blue background color unless we set it to match our page background color
    /// </summary>
    /// <param name="dialogFragment"></param>
    /// <remarks>
    /// TODO: Workaround, remove when MAUI supports this out of the box
    /// Inspiration: https://github.com/dotnet/maui/issues/32987 and https://github.com/CommunityToolkit/Maui/pull/2939/changes
    /// </remarks>
    private static void TryInheritStatusBarColorOnModal(DialogFragment dialogFragment)
    {
        try
        {
            var window = dialogFragment.Dialog?.Window;
            if (window is null)
            {
                return;
            }

            var customStatusBarColor = Colors.GetColor(ContentPage.BackgroundColorName);
            var platformColor = customStatusBarColor.ToPlatform();
            
            // Set light/dark status bar icons based on background color luminance
            var windowInsetsController = WindowCompat.GetInsetsController(window, window.DecorView);
            if (windowInsetsController is not null)
            {
                // Calculate if the background is light or dark to determine icon color
                var isLightBackground = customStatusBarColor.GetLuminosity() > 0.5;
                windowInsetsController.AppearanceLightStatusBars = isLightBackground;
            }
            
            // Only apply if the modal is inside a NavigationPage
            if (Microsoft.Maui.Controls.Shell.Current?.CurrentPage is not { Parent: NavigationPage })
            {
                return;
            }
            
            var coordinatorLayout = FindCoordinatorLayout(window.DecorView);
            if (coordinatorLayout is null)
                return;

            coordinatorLayout.SetBackgroundColor(platformColor);
                
            // The AppBarLayout inside the CoordinatorLayout is likely what's showing the blue color
            var appBarLayout = coordinatorLayout.FindViewById<AppBarLayout>(_Microsoft.Android.Resource.Designer.Resource.Id.navigationlayout_appbar);
            appBarLayout?.SetBackgroundColor(platformColor);
        }
        catch (Exception ex)
        {
            DUILogService.LogError<FragmentLifeCycleCallback>($"Failed to inherit status bar color on modal: {ex.Message}");
        }
    }

    private static AndroidX.CoordinatorLayout.Widget.CoordinatorLayout? FindCoordinatorLayout(View? view)
    {
        if (view is null)
            return null;

        if (view is AndroidX.CoordinatorLayout.Widget.CoordinatorLayout coordinatorLayout)
            return coordinatorLayout;

        if (view is ViewGroup viewGroup)
        {
            for (int i = 0; i < viewGroup.ChildCount; i++)
            {
                var result = FindCoordinatorLayout(viewGroup.GetChildAt(i));
                if (result is not null)
                    return result;
            }
        }

        return null;
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
    sealed class InsetsListener : Java.Lang.Object, IOnApplyWindowInsetsListener
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