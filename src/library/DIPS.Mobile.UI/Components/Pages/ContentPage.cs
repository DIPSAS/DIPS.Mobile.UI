using DIPS.Mobile.UI.Extensions;
using DIPS.Mobile.UI.Resources.Colors;
using Xamarin.Forms;

namespace DIPS.Mobile.UI.Components.Pages
{
    public class ContentPage : Xamarin.Forms.ContentPage
    {
        public static readonly ColorName BackgroundColorName = NavigationPage.BackgroundColorName; 
        
        public ContentPage()
        {
            this.SetAppThemeColor(BackgroundProperty, BackgroundColorName);
            Padding = 15; //TODO:Change to design tokens
        }
    }
}