using Android.Text;
using Android.Views;
using Android.Widget;
using AndroidX.AppCompat.Graphics.Drawable;
using AndroidX.Core.View;
using AndroidX.Fragment.App;
using DIPS.Mobile.UI.API.Library.Android;
using DIPS.Mobile.UI.API.Tip;
using DIPS.Mobile.UI.Internal.Logging;
using Google.Android.Material.AppBar;
using Google.Android.Material.BottomSheet;
using Microsoft.Maui.Platform;
using Color = Android.Graphics.Color;
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
                TryInheritStatusBarColorOnModal(dialogFragment);
                TryInheritWindowFlags(dialogFragment);
            }

            TryEnableCustomHideSoftInputOnTappedImplementation(dialogFragment);
        }
     
        base.OnFragmentStarted(fm, f);
    }   
    
    // TODO: Workaround, remove when MAUI supports this out of the box
    // Inspiration: https://github.com/dotnet/maui/issues/32987
    private static void TryInheritStatusBarColorOnModal(DialogFragment dialogFragment)
    {
        try
        {
            // Only apply if the modal is inside a NavigationPage
            if (Microsoft.Maui.Controls.Shell.Current?.CurrentPage is not { Parent: Microsoft.Maui.Controls.NavigationPage })
            {
                return;
            }

            var activity = Platform.CurrentActivity;
            if (activity is null)
            {
                return;
            }

            var statusBarColor = activity.Window!.StatusBarColor;
            var platformColor = new Color(statusBarColor);

            var window = dialogFragment?.Dialog?.Window;
            if (window is null)
            {
                return;
            }

            var isColorTransparent = platformColor == global::Android.Resource.Color.Transparent;
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

            var controller = WindowCompat.GetInsetsController(window!, window!.DecorView);
            if (controller != null)
            {
                // Light status bars = dark icons (for light backgrounds)
                controller.AppearanceLightStatusBars = Application.Current?.RequestedTheme == AppTheme.Light;
                controller.AppearanceLightNavigationBars = Application.Current?.RequestedTheme == AppTheme.Light;
            }

            var customStatusBarColor = Colors.GetColor(ContentPage.BackgroundColorName);
            
            // Set the status bar color for the modal dialog
            if (OperatingSystem.IsAndroidVersionAtLeast(35))
            {
                var statusBarScrimId = View.GenerateViewId();
                if (window.DecorView is not ViewGroup decor)
                {
                    return;
                }

                var scrim = decor.FindViewById(statusBarScrimId);
                if (scrim == null)
                {
                    scrim = new View(activity) { Id = statusBarScrimId };
                    scrim.LayoutParameters = new FrameLayout.LayoutParams(
                        ViewGroup.LayoutParams.MatchParent, 0, GravityFlags.Top);
                    decor.AddView(scrim);
                }

                // Apply color
                scrim.SetBackgroundColor(customStatusBarColor.ToPlatform());

                // Size it to the status bar inset
                ViewCompat.SetOnApplyWindowInsetsListener(decor, new InsetsListener(scrim));
                ViewCompat.RequestApplyInsets(decor);
            }
            else
            {
                window.SetStatusBarColor(customStatusBarColor.ToPlatform());
                if (OperatingSystem.IsAndroidVersionAtLeast(30))
                {
                    window.SetDecorFitsSystemWindows(!isColorTransparent);
                }
            }
        }
        catch (Exception ex)
        {
            DUILogService.LogError<FragmentLifeCycleCallback>($"Failed to inherit status bar color on modal: {ex.Message}");
        }
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