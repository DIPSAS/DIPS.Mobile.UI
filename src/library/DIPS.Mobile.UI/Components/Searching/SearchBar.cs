using System.Threading;
using DIPS.Mobile.UI.Extensions;
using DIPS.Mobile.UI.Resources.Colors;
using Xamarin.Forms;

namespace DIPS.Mobile.UI.Components.Searching
{
    public partial class SearchBar : Xamarin.Forms.SearchBar
    {
        public SearchBar()
        {
            this.SetAppThemeColor(BackgroundColorProperty, ColorName.color_neutral_05);
            this.SetAppThemeColor(IconsColorProperty, ColorName.color_neutral_50);
            this.SetAppThemeColor(TextColorProperty, ColorName.color_neutral_70);
            this.CornerRadius = 8; //TODO: Use DesignSystem
        }

        public void SendCancelTapped()
        {
            CancelCommand?.Execute(CancelCommandParameter);
        }
    }
}