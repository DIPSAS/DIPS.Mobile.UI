using DIPS.Mobile.UI.Components.BottomSheets;
using Colors = DIPS.Mobile.UI.Resources.Colors.Colors;

namespace DIPS.Mobile.UI.Components.TextFields.Entry;

public partial class Entry : Microsoft.Maui.Controls.Entry
{
    public Entry()
    {
        // TODO: Wait until we have semantic color
        PlaceholderColor = Colors.GetColor(ColorName.color_neutral_60);
        FontFamily = "Body";
        FontSize = 16;
        TextColor = Colors.GetColor(ColorName.color_text_default);
        Keyboard = Keyboard.Text;
        ReturnType = ReturnType.Done;
    }
}