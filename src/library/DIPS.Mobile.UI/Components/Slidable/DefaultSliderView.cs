namespace DIPS.Mobile.UI.Components.Slidable
{
    internal class DefaultSliderView : BoxView
    {
        public DefaultSliderView()
        {
            Margin = 0;
            WidthRequest = 1;
            this.SetAppThemeColor(ColorProperty, ColorName.color_fill_neutral);
            CornerRadius = 0;
            HorizontalOptions = LayoutOptions.Center;
            VerticalOptions = LayoutOptions.Center;
        }
    }
}
