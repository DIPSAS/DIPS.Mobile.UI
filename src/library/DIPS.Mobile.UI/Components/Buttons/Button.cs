using DIPS.Mobile.UI.Resources.Colors;
using DIPS.Mobile.UI.Extensions;

namespace DIPS.Mobile.UI.Components.Buttons
{
    public partial class Button : Microsoft.Maui.Controls.Button
    {
        public Button()
        {
            this.SetAppThemeColor(BackgroundColorProperty, ColorName.color_primary_90);
            this.SetAppThemeColor(TextColorProperty, ColorName.color_neutral_05);
            //TODO: Use DesignSystem for Padding
            Margin = new Thickness(10, 5);
            CornerRadius = 8;
        }
    }
}