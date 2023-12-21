using DIPS.Mobile.UI.Components.BottomSheets;
using Colors = DIPS.Mobile.UI.Resources.Colors.Colors;

namespace DIPS.Mobile.UI.Components.TextFields.Entry;

public partial class Entry : Microsoft.Maui.Controls.Entry
{
    public Entry()
    {
        PlaceholderColor = Colors.GetColor(ColorName.color_neutral_60);
        FontFamily = "Body";
        FontSize = 16;
        TextColor = Colors.GetColor(ColorName.color_neutral_90);
        Keyboard = Keyboard.Text;
    }
}