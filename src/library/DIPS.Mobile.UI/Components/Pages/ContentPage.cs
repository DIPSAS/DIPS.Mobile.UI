using DIPS.Mobile.UI.Extensions;
using DIPS.Mobile.UI.Resources.Colors;
using Xamarin.Forms;

namespace DIPS.Mobile.UI.Components.Pages
{
    public class ContentPage : Xamarin.Forms.ContentPage
    {
        public ContentPage()
        {
            this.SetAppThemeColor(BackgroundProperty, ColorName.color_primary_light_primary_80);
            Padding = 15; //TODO:Change to design tokens
        }
    }
}