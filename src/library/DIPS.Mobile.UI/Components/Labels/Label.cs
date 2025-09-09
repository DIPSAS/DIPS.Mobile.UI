using DIPS.Mobile.UI.Resources.Styles;
using DIPS.Mobile.UI.Resources.Styles.Label;

namespace DIPS.Mobile.UI.Components.Labels;

public partial class Label : Microsoft.Maui.Controls.Label
{
    private static Style? s_defaultLabelStyle;
    public static Style DefaultLabelStyle => s_defaultLabelStyle ??= Styles.GetLabelStyle(LabelStyle.Body300);
        
    public Label()
    {
        this.SetAppThemeColor(TextColorProperty, ColorName.color_text_default);
        MaxLines = int.MaxValue;
        Style = DefaultLabelStyle;
    }
}