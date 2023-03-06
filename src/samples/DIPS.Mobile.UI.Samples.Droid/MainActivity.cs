using System;
using Android.App;
using Android.Content;
using Android.Content.PM;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;
using DIPS.Mobile.UI.Resources.Colors;
using Xamarin.Forms.Platform.Android;
using Fragment = AndroidX.Fragment.App.Fragment;
using FragmentContainer = AndroidX.Fragment.App.FragmentContainer;

namespace DIPS.Mobile.UI.Samples.Droid
{
    [Activity(Label = "DIPS.Mobile.UI.Samples", Theme = "@style/DIPS.Mobile.UI.Style", MainLauncher = true,
        ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation)]
    [MetaData("android.app.default_searchable", Value = ".DUISearchableActivity")]
    public class MainActivity : global::Xamarin.Forms.Platform.Android.FormsAppCompatActivity
    {
        protected override void OnCreate(Bundle savedInstanceState)
        {
            TabLayoutResource = Resource.Layout.Tabbar;
            ToolbarResource = Resource.Layout.Toolbar;
            DIPS.Mobile.UI.Droid.DUI.Init(this); //Initialize DIPS.Mobile.UI
            base.OnCreate(savedInstanceState);
            Xamarin.Forms.Forms.Init(this, savedInstanceState);
            LoadApplication(new App());
            AppDomain.CurrentDomain.UnhandledException += (sender, args) =>
            {
                
            };
        }

        protected override void OnStart()
        {
            base.OnStart();
        }

        protected override void OnResume()
        {
            base.OnResume();
        }
    }
}