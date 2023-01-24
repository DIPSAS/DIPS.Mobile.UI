using System;
using Android.App;
using Android.Content.PM;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;
using DIPS.Mobile.UI.Resources.Colors;
using Xamarin.Forms.Platform.Android;

namespace DIPS.Mobile.UI.Samples.Droid
{
    [Activity(Label = "DIPS.Mobile.UI.Samples", Theme = "@style/DIPS.Mobile.UI.Style", MainLauncher = true,
        ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation)]
    public class MainActivity : global::Xamarin.Forms.Platform.Android.FormsAppCompatActivity
    {
        protected override void OnCreate(Bundle savedInstanceState)
        {
            TabLayoutResource = Resource.Layout.Tabbar;
            ToolbarResource = Resource.Layout.Toolbar;
            DIPS.Mobile.UI.Droid.DUI.Init(); //Initialize DIPS.Mobile.UI
            base.OnCreate(savedInstanceState);
            global::Xamarin.Forms.Forms.Init(this, savedInstanceState);
            LoadApplication(new App());
        }
    }
}