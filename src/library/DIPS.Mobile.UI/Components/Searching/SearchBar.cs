using DIPS.Mobile.UI.Extensions;
using DIPS.Mobile.UI.Resources.Colors;

namespace DIPS.Mobile.UI.Components.Searching
{
    public partial class SearchBar : View
    {
        public SearchBar()
        {
            this.SetAppThemeColor(IconsColorProperty, ColorName.color_neutral_60);
            this.SetAppThemeColor(TextColorProperty, ColorName.color_neutral_60);
            this.SetAppThemeColor(BarColorProperty, ColorName.color_neutral_05);
            this.SetAppThemeColor(TextFieldColorProperty, ColorName.color_neutral_05);
        }
    }
}