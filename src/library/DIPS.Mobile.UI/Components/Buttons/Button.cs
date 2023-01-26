using DIPS.Mobile.UI.Extensions;
using DIPS.Mobile.UI.Resources.Colors;
using Xamarin.Forms;

namespace DIPS.Mobile.UI.Components.Buttons
{
    public class Button : Xamarin.Forms.Button
    {
        public Button()
        {
            this.SetAppThemeColor(BackgroundColorProperty, ColorName.color_primary_90);
            this.SetAppThemeColor(TextColorProperty, ColorName.color_neutral_05);
            //TODO: Use DesignSystem for Padding
            Padding = new Thickness(10, 5);
            ContentLayout = new ButtonContentLayout(ButtonContentLayout.ImagePosition.Left, 5);
        }
    }
}