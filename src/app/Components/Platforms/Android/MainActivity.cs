using Android.App;
using Android.Content.PM;
using Android.OS;
using AndroidX.Activity;
using AndroidX.Core.View;

namespace Components;

[Activity(Theme = "@style/DIPS.Mobile.UI.Style", MainLauncher = true,
    ScreenOrientation = ScreenOrientation.Portrait,
    ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation | ConfigChanges.UiMode |
                           ConfigChanges.ScreenLayout | ConfigChanges.SmallestScreenSize | ConfigChanges.Density)]
public class MainActivity : MauiAppCompatActivity
{
    protected override void OnCreate(Bundle? savedInstanceState)
    {
        base.OnCreate(savedInstanceState);
        
        // Enable edge-to-edge and let MAUI SafeAreaEdges handle the insets
        if (Window != null)
        {
            // When using edge-to-edge, the XML windowLightStatusBar is ignored
            // Must explicitly set status bar icon colors via WindowInsetsController
            var insetsController = WindowCompat.GetInsetsController(Window, Window.DecorView);
            if (insetsController != null)
            {
                // Light status bars = dark icons (for light backgrounds)
                insetsController.AppearanceLightStatusBars = true;
            }
            
        }
    }
}