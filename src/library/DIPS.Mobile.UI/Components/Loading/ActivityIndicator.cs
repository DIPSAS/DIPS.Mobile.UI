namespace DIPS.Mobile.UI.Components.Loading;

public class ActivityIndicator : Microsoft.Maui.Controls.ActivityIndicator
{
    // TODO: Lisa
    internal const ColorName LoadingIndicatorColorName = ColorName.color_neutral_40;

    public ActivityIndicator()
    {
        HeightRequest = Sizes.GetSize(SizeName.size_6);
        WidthRequest = Sizes.GetSize(SizeName.size_6);
        this.SetAppThemeColor(ColorProperty, LoadingIndicatorColorName);
    }
}