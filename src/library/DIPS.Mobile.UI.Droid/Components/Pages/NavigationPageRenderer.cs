using Android.App;
using Android.Content;
using AndroidX.AppCompat.App;
using DIPS.Mobile.UI.Droid.Components.Pages;
using Google.Android.Material.Navigation;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;
using NavigationPage = DIPS.Mobile.UI.Components.Pages.NavigationPage;

[assembly: ExportRenderer(typeof(NavigationPage), typeof(NavigationPageRenderer))]

namespace DIPS.Mobile.UI.Droid.Components.Pages
{
    public class NavigationPageRenderer : Xamarin.Forms.Platform.Android.AppCompat.NavigationPageRenderer
    {
        public NavigationPageRenderer(Context context):base(context)
        {
            
        }
        protected override void OnElementChanged(ElementChangedEventArgs<Xamarin.Forms.NavigationPage> e)
        {
            base.OnElementChanged(e);
        }
    }
}