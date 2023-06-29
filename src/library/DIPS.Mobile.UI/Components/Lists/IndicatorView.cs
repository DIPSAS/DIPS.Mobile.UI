namespace DIPS.Mobile.UI.Components.Lists;

public class IndicatorView : Microsoft.Maui.Controls.IndicatorView
{
    public IndicatorView()
    {
        IndicatorSize = Sizes.GetSize(SizeName.size_2);
        this.SetAppThemeColor(SelectedIndicatorColorProperty, ColorName.color_primary_90);
        this.SetAppThemeColor(IndicatorColorProperty, ColorName.color_neutral_40);
    }
}