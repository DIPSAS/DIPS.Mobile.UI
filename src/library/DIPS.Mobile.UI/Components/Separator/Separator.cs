using Colors = DIPS.Mobile.UI.Resources.Colors.Colors;

namespace DIPS.Mobile.UI.Components.Separator;

public class Separator : View
{
    public Separator()
    {

        this.BackgroundColor = Colors.GetColor(ColorName.color_system_black);
    }
}