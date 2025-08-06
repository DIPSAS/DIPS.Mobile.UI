namespace DIPS.Mobile.UI.Components.Loading;

public partial class ActivityIndicator : Microsoft.Maui.Controls.ActivityIndicator
{
    internal const ColorName LoadingIndicatorColorName = ColorName.color_icon_subtle;

    public ActivityIndicator()
    {
        HeightRequest = Sizes.GetSize(SizeName.size_6);
        WidthRequest = Sizes.GetSize(SizeName.size_6);
        this.SetAppThemeColor(ColorProperty, LoadingIndicatorColorName);
    }
}