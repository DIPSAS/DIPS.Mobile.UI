using Android.App;
using Android.Content.PM;
using Android.OS;

namespace Components;

[Activity(Theme = "@style/DIPS.Mobile.UI.Style", MainLauncher = true,
    ScreenOrientation = ScreenOrientation.Portrait,
    ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation | ConfigChanges.UiMode |
                           ConfigChanges.ScreenLayout | ConfigChanges.SmallestScreenSize | ConfigChanges.Density)]
public class MainActivity : MauiAppCompatActivity
{
    public MainActivity()
    {
        
    }

    public override void OnCreate(Bundle? savedInstanceState, PersistableBundle? persistentState)
    {
        // RequestedOrientation = ScreenOrientation.Portrait;
        base.OnCreate(savedInstanceState, persistentState);
    }
}