using Colors = DIPS.Mobile.UI.Resources.Colors.Colors;

namespace DIPS.Mobile.UI.Components.Selection;

public class Switch : Microsoft.Maui.Controls.Switch
{
    public Switch()
    {
#if __ANDROID__
        HeightRequest = 0; //Bug: On Android, the component takes more space than it actually is.
#endif
        OnColor = Colors.GetColor(ColorName.color_fill_input_toggle_selected);
    }
}