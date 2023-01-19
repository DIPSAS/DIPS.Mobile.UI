using DIPS.Mobile.UI.Extensions;
using DIPS.Mobile.UI.Resources.Colors;
using Xamarin.Forms;

namespace DIPS.Mobile.UI.Components.Buttons
{
    public class Button : Xamarin.Forms.Button
    {
        public Button()
        {
            this.SetAppThemeColor(BackgroundColorProperty, ColorName.color_primary_light_primary_100);
            TextColor = Color.White;
            ContentLayout = new ButtonContentLayout(ButtonContentLayout.ImagePosition.Left, 5);
        }
    }
}