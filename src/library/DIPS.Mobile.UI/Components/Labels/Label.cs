using DIPS.Mobile.UI.Extensions;
using DIPS.Mobile.UI.Resources.Colors;
using Xamarin.Forms;

namespace DIPS.Mobile.UI.Components.Labels
{
    public class Label : Xamarin.Forms.Label
    {
        public Label()
        {
            this.SetAppThemeColor(TextColorProperty, ColorName.color_system_black);
        }
    }
}