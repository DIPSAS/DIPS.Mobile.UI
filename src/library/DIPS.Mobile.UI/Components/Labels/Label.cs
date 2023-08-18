using DIPS.Mobile.UI.Extensions;
using DIPS.Mobile.UI.Resources.Colors;

namespace DIPS.Mobile.UI.Components.Labels
{
    public class Label : Microsoft.Maui.Controls.Label
    {
        public Label()
        {
            this.SetAppThemeColor(TextColorProperty, ColorName.color_neutral_90);
        }
    }
}