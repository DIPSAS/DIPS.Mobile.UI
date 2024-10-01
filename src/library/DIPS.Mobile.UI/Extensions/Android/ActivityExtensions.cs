using Android.App;
using Android.Content;
using Microsoft.Maui.Platform;

namespace DIPS.Mobile.UI.Extensions.Android;

public static class ActivityExtensions
{
    /// <summary>
    /// Will change the colors of the status and navigation bar the application.
    /// </summary>
    /// <param name="activity"></param>
    /// <param name="color"></param>
    /// <returns></returns>
    /// <remarks>Remember to reset the colors using the return value from this method along with <see cref="ResetStatusAndNavigationBarColor"/></remarks>
    public static StatusAndNavigationBarColors? SetStatusAndNavigationBarColor(this Activity activity, Color color)
    {
        var statusBarColor = 0;
        var navigationBarColor = 0;
        if (activity.Window != null)
        {
            statusBarColor = activity.Window.StatusBarColor;
            navigationBarColor = activity.Window.NavigationBarColor;
        }

        activity.Window?.SetStatusBarColor(color.ToPlatform());
        activity.Window?.SetNavigationBarColor(color.ToPlatform());
        return new StatusAndNavigationBarColors(statusBarColor, navigationBarColor);
    }
    
    public static StatusAndNavigationBarColors? SetStatusAndNavigationBarColor(this Context? context, Color color)
    {
        return context is not Activity activity ? null : SetStatusAndNavigationBarColor(activity, color);
    }
    
    public static void ResetStatusAndNavigationBarColor(this Context? context, StatusAndNavigationBarColors statusAndNavigationBarColors)
    {
        if (context is not Activity activity) return;
        activity.Window?.SetStatusBarColor(new global::Android.Graphics.Color(statusAndNavigationBarColors.StatusBarColorInt));
        activity.Window?.SetNavigationBarColor(new global::Android.Graphics.Color(statusAndNavigationBarColors.NavigationBarColorInt));
    }
}