using Android.App;
using Android.Views;
using Android.Widget;
using AndroidX.Core.View;
using DIPS.Mobile.UI.Internal.Logging;
using Google.Android.Material.AppBar;
using Microsoft.Maui.Platform;
using AView = Android.Views.View;
using ContentPage = DIPS.Mobile.UI.Components.Pages.ContentPage;
using DialogFragment = AndroidX.Fragment.App.DialogFragment;

namespace DIPS.Mobile.UI.API.Library.Android;

public static class StatusBarHandler
{
    private static Color? s_statusBarColorOverride;
    
    /// <summary>
    /// Set this to override the status bar color globally
    /// </summary>
    public static Color? StatusBarColorOverride
    {
        get => s_statusBarColorOverride;
        set
        {
            s_statusBarColorOverride = value;

            if (Shell.Current.CurrentPage is ContentPage page)
            { 
                TrySetStatusBarColor(page, s_statusBarColorOverride ?? page.StatusBarColor);
            }
        }
    }

    private static Activity Activity => Platform.CurrentActivity ?? throw new InvalidOperationException("Android Activity can't be null.");
    
    // Maps ContentPages to their DialogFragments when presented as modals
    private static readonly Dictionary<WeakReference<ContentPage>, DialogFragment> s_pageToDialogFragmentMap = new();
    

    /// <summary>
    /// Registers a DialogFragment for a page when the fragment is created
    /// Called from FragmentLifeCycleCallback.OnFragmentStarted
    /// </summary>
    internal static void RegisterDialogFragmentForPage(DialogFragment dialogFragment)
    {
        try
        {
            if (Shell.Current?.CurrentPage is ContentPage page)
            {
                // Clean up old entries
                CleanupStaleReferences();
                
                // Store the mapping
                s_pageToDialogFragmentMap[new WeakReference<ContentPage>(page)] = dialogFragment;
                
                // Immediately set the status bar color since OnAppearing may have already been called
                // before the DialogFragment was registered
                SetStatusBarColorOnModalWindow(dialogFragment, page.StatusBarColor);
            }
        }
        catch (Exception ex)
        {
            DUILogService.LogError<DialogFragment>($"Failed to register DialogFragment: {ex.Message}");
        }
    }
    
    /// <summary>
    /// Unregisters a DialogFragment when it's dismissed
    /// </summary>
    internal static void UnregisterDialogFragment(DialogFragment dialogFragment)
    {
        try
        {
            var keyToRemove = s_pageToDialogFragmentMap.FirstOrDefault(kvp => kvp.Value == dialogFragment).Key;
            if (keyToRemove != null)
            {
                s_pageToDialogFragmentMap.Remove(keyToRemove);
            }
        }
        catch (Exception ex)
        {
            DUILogService.LogError<DialogFragment>($"Failed to unregister DialogFragment: {ex.Message}");
        }
    }
    
    private static void CleanupStaleReferences()
    {
        var staleKeys = s_pageToDialogFragmentMap.Keys
            .Where(weakRef => !weakRef.TryGetTarget(out _))
            .ToList();
            
        foreach (var key in staleKeys)
        {
            s_pageToDialogFragmentMap.Remove(key);
        }
    }

    /// <summary>
    /// Sets the status bar color for a page, automatically detecting if it's modal or not
    /// </summary>
    /// <param name="page">The ContentPage requesting the status bar color change</param>
    /// <param name="color">The color to set</param>
    public static void TrySetStatusBarColor(ContentPage page, Color color)
    {
        // Check if this is a modal page by looking for DialogFragment
        var dialogFragment = TryGetDialogFragmentForPage(page);

        // If we can't find a DialogFragment but there are modals in the stack, it means this page is part of a modal that hasn't registered its DialogFragment yet, so we skip setting the color for now
        if (dialogFragment is null && Shell.Current.Navigation.ModalStack.Count > 0)
            return;
        
        if(StatusBarColorOverride is not null)
            color = StatusBarColorOverride;
        
        if (dialogFragment != null)
        {
            // Modal page - set color on DialogFragment's window
            SetStatusBarColorOnModalWindow(dialogFragment, color);
        }
        else
        {
            // Non-modal page - set color on Activity window
            SetStatusBarColorOnActivityWindow(color);
        }
    }
    
    /// <summary>
    /// Try to retrieve the DialogFragment for a given page if it's presented as a modal
    /// </summary>
    private static DialogFragment? TryGetDialogFragmentForPage(ContentPage page)
    {
        try
        {
            // First check if the page is in a modal by checking its parent
            var parentNavPage = page.Parent as NavigationPage;
            var isModal = parentNavPage != null && 
                          Shell.Current?.Navigation?.ModalStack?.Contains(parentNavPage) == true;
            
            if (!isModal)
            {
                return null;
            }
            
            // Look up the DialogFragment from our mapping
            // Since all pages in the same modal NavigationPage share the same DialogFragment,
            // we need to find ANY page that belongs to the same NavigationPage
            CleanupStaleReferences();
            
            foreach (var kvp in s_pageToDialogFragmentMap)
            {
                if (kvp.Key.TryGetTarget(out var mappedPage))
                {
                    // Check if the mapped page belongs to the same modal NavigationPage
                    if (mappedPage.Parent == parentNavPage)
                    {
                        return kvp.Value;
                    }
                }
            }
        }
        catch (Exception ex)
        {
            DUILogService.LogError<DialogFragment>($"Failed to get DialogFragment: {ex.Message}");
        }
        
        return null;
    }
    
    /// <summary>
    /// Sets status bar color on a modal page's DialogFragment window
    /// </summary>
    /// <remarks>
    /// TODO: Workaround, remove when MAUI supports this out of the box
    /// Inspiration: https://github.com/dotnet/maui/issues/32987 and https://github.com/CommunityToolkit/Maui/pull/2939/changes
    /// </remarks>
    private static void SetStatusBarColorOnModalWindow(DialogFragment dialogFragment, Color color)
    {
        try
        {
            if (OperatingSystem.IsAndroidVersionAtLeast(35))
            {
                var window = dialogFragment.Dialog?.Window;
                if (window is null)
                {
                    return;
                }

                var platformColor = color.ToPlatform();

                // Set light/dark status bar icons based on background color luminance
                var windowInsetsController = WindowCompat.GetInsetsController(window, window.DecorView);
                if (windowInsetsController is not null)
                {
                    var isLightBackground = color.GetLuminosity() > 0.5;
                    windowInsetsController.AppearanceLightStatusBars = isLightBackground;
                }

                var coordinatorLayout = FindCoordinatorLayout(window.DecorView);
                if (coordinatorLayout is null)
                    return;

                coordinatorLayout.SetBackgroundColor(platformColor);

                // The AppBarLayout inside the CoordinatorLayout is likely what's showing the blue color
                var appBarLayout =
                    coordinatorLayout.FindViewById<AppBarLayout>(_Microsoft.Android.Resource.Designer.Resource.Id
                        .navigationlayout_appbar);
                appBarLayout?.SetBackgroundColor(platformColor);
            }
            else
            {
                dialogFragment.Dialog?.Window?.SetStatusBarColor(color.ToPlatform());
            }
        }
        catch (Exception ex)
        {
            DUILogService.LogError<DialogFragment>($"Failed to set status bar color on modal: {ex.Message}");
        }
    }
    
    /// <summary>
    /// Sets status bar color on the Activity window (non-modal pages)
    /// </summary>
    private static void SetStatusBarColorOnActivityWindow(Color color)
    {
        if (OperatingSystem.IsAndroidVersionAtLeast(35))
        {
            const string statusBarOverlayTag = "StatusBarOverlay";

            var decorGroup = (ViewGroup?)Activity.Window?.DecorView;
            var statusBarOverlay = decorGroup?.FindViewWithTag(statusBarOverlayTag);

            if (statusBarOverlay is null)
            {
                var statusBarHeight = Activity.Resources?.GetIdentifier("status_bar_height", "dimen", "android") ?? 0;
                var statusBarPixelSize = statusBarHeight > 0 ? Activity.Resources?.GetDimensionPixelSize(statusBarHeight) ?? 0 : 0;

                statusBarOverlay = new AView(Activity)
                {
                    LayoutParameters = new FrameLayout.LayoutParams(ViewGroup.LayoutParams.MatchParent, statusBarPixelSize + 3)
                    {
                        Gravity = GravityFlags.Top
                    }
                };

                decorGroup?.AddView(statusBarOverlay);
                statusBarOverlay.SetZ(0);
            }
            
            statusBarOverlay.SetBackgroundColor(color.ToPlatform());
        }
        else
        {
            Activity.Window?.SetStatusBarColor(color.ToPlatform());
        }
        
        var windowInsetsController = WindowCompat.GetInsetsController(Activity.Window, Activity.Window?.DecorView);
        if (windowInsetsController is not null)
        {
            // Calculate if the background is light or dark to determine icon color
            var isLightBackground = color.GetLuminosity() > 0.5;
            windowInsetsController.AppearanceLightStatusBars = isLightBackground;
        }
    }

    private static AndroidX.CoordinatorLayout.Widget.CoordinatorLayout? FindCoordinatorLayout(AView? view)
    {
        if (view is null)
            return null;

        if (view is AndroidX.CoordinatorLayout.Widget.CoordinatorLayout coordinatorLayout)
            return coordinatorLayout;

        if (view is ViewGroup viewGroup)
        {
            for (var i = 0; i < viewGroup.ChildCount; i++)
            {
                var result = FindCoordinatorLayout(viewGroup.GetChildAt(i));
                if (result is not null)
                    return result;
            }
        }

        return null;
    }
}