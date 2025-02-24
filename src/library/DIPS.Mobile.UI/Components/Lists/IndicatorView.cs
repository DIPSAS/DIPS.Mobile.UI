namespace DIPS.Mobile.UI.Components.Lists;

public class IndicatorView : Microsoft.Maui.Controls.IndicatorView
{
    public IndicatorView()
    {
        IndicatorSize = Sizes.GetSize(SizeName.content_margin_medium);
        // TODO: Lisa
        this.SetAppThemeColor(SelectedIndicatorColorProperty, ColorName.color_primary_90);
        this.SetAppThemeColor(IndicatorColorProperty, ColorName.color_neutral_40);
    }
}