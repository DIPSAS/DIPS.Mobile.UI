using Microsoft.Maui.Controls.Shapes;
using Colors = DIPS.Mobile.UI.Resources.Colors.Colors;

namespace DIPS.Mobile.UI.Components.Border;

public class Border : Microsoft.Maui.Controls.Border
{
    public Border()
    {
        StrokeThickness = 1;
        Stroke = Colors.GetColor(ColorName.color_border_default);
        StrokeShape = new RoundRectangle { CornerRadius = Sizes.GetSize(SizeName.size_2) };
    }
}