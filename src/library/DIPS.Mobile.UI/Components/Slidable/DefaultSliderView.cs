namespace DIPS.Mobile.UI.Components.Slideable
{
    internal class DefaultSliderView : BoxView
    {
        public DefaultSliderView()
        {
            Margin = 0;
            WidthRequest = 1;
            this.SetAppThemeColor(ColorProperty, ColorName.color_primary_90);
            CornerRadius = 0;
            HorizontalOptions = LayoutOptions.Center;
            VerticalOptions = LayoutOptions.Center;
        }
    }
}
