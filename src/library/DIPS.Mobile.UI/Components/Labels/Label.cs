using DIPS.Mobile.UI.Resources.Styles;
using DIPS.Mobile.UI.Resources.Styles.Label;

namespace DIPS.Mobile.UI.Components.Labels;

public partial class Label : Microsoft.Maui.Controls.Label
{
    public static Style DefaultLabelStyle = Styles.GetLabelStyle(LabelStyle.Body300);
        
    public Label()
    {
        /*this.SetAppThemeColor(TextColorProperty, ColorName.color_text_default);*/
        MaxLines = int.MaxValue;
        Style = DefaultLabelStyle;
    }
}