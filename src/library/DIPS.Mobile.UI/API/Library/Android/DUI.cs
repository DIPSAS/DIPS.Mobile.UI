using Android.App;
using AndroidX.Core.SplashScreen;

namespace DIPS.Mobile.UI.API.Library.Android;

// ReSharper disable once InconsistentNaming
public static class DUI
{
    public static void Init(Activity activity)
    {
        SplashScreen.InstallSplashScreen(activity);
    }
}