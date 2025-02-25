namespace DIPS.Mobile.UI.Components.Lists;

public class IndicatorView : Microsoft.Maui.Controls.IndicatorView
{
    public IndicatorView()
    {
        IndicatorSize = Sizes.GetSize(SizeName.content_margin_medium);
        
        this.SetAppThemeColor(SelectedIndicatorColorProperty, ColorName.color_icon_action);
        this.SetAppThemeColor(IndicatorColorProperty, ColorName.color_icon_neutral);
    }
}