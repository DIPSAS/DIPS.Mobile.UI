using DIPS.Mobile.UI.Resources.Colors;
using DIPS.Mobile.UI.Extensions;
using DIPS.Mobile.UI.Sizes.Sizes;

namespace DIPS.Mobile.UI.Components.Buttons
{
    public partial class Button : Microsoft.Maui.Controls.Button
    {
        public Button()
        {
            this.SetAppThemeColor(BackgroundColorProperty, ColorName.color_primary_90);
            this.SetAppThemeColor(TextColorProperty, ColorName.color_neutral_05);
            Padding = new Thickness(UI.Resources.Sizes.Sizes.GetSize(SizeName.size_3), UI.Resources.Sizes.Sizes.GetSize(SizeName.size_1));
            CornerRadius = UI.Resources.Sizes.Sizes.GetSize(SizeName.size_2);
        }
        
        
    }
}