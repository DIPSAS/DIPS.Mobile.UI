using Colors = DIPS.Mobile.UI.Resources.Colors.Colors;

namespace DIPS.Mobile.UI.Components.Dividers;

public class Divider : View
{
    public Divider()
    {
        BackgroundColor = Colors.GetColor(ColorName.color_neutral_40);
    }
}