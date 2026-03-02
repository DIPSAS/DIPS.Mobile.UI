using Android.App;
using Android.Content;
using Android.Content.PM;
using Android.OS;
using AndroidX.Activity;
using AndroidX.Core.View;
using DIPS.Mobile.UI.API.PictureInPicture;

namespace Components;

[Activity(Theme = "@style/DIPS.Mobile.UI.Style", MainLauncher = true,
    ScreenOrientation = ScreenOrientation.Portrait,
    SupportsPictureInPicture = true,
    ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation | ConfigChanges.UiMode |
                           ConfigChanges.ScreenLayout | ConfigChanges.SmallestScreenSize | ConfigChanges.Density)]
public class MainActivity : MauiAppCompatActivity
{
    public override void OnPictureInPictureModeChanged(bool isInPictureInPictureMode, Configuration newConfig)
    {
        base.OnPictureInPictureModeChanged(isInPictureInPictureMode, newConfig);
        PipService.NotifyPipModeChanged(isInPictureInPictureMode);
    }
}