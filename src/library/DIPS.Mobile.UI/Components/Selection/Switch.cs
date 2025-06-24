using Colors = DIPS.Mobile.UI.Resources.Colors.Colors;

namespace DIPS.Mobile.UI.Components.Selection;

public class Switch : Microsoft.Maui.Controls.Switch
{
#if __ANDROID__
    public Switch()
    {
        HeightRequest = 0; //Bug: On Android, the component takes more space than it actually is.
    }
#endif

#if __IOS__
    public Switch()
    {
        OnColor = Colors.GetColor(ColorName.color_fill_action);
    }
#endif
}