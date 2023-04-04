using Android.App;
using Android.Content.PM;
using Android.OS;

namespace DIPS.Mobile.UI.Components;

[Activity(Theme = "@style/DIPS.Mobile.UI.Style", MainLauncher = true,
    ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation | ConfigChanges.UiMode |
                           ConfigChanges.ScreenLayout | ConfigChanges.SmallestScreenSize | ConfigChanges.Density)]
public class MainActivity : MauiAppCompatActivity
{
}