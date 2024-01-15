using Microsoft.Maui.Controls.Shapes;

namespace DIPS.Mobile.UI.Components.ListItems;

public partial class ListItem
{
    private partial void SetCornerRadiusPlatform()
    {
        Border.StrokeShape = new RoundRectangle {CornerRadius = CornerRadius, StrokeThickness = 0};
    }
}