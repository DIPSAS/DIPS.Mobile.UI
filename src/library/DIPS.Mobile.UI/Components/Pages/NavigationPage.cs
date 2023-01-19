using DIPS.Mobile.UI.Extensions;
using DIPS.Mobile.UI.Resources.Colors;
using Xamarin.Forms;

namespace DIPS.Mobile.UI.Components.Pages
{
    public class NavigationPage : Xamarin.Forms.NavigationPage
    {
        public NavigationPage(Xamarin.Forms.ContentPage contentPage) : base(contentPage)
        {
            this.SetAppThemeColor(BarBackgroundProperty, ColorName.color_primary_light_primary_100);
            BarTextColor = Color.White;
        }
    }
}