using Colors = DIPS.Mobile.UI.Resources.Colors.Colors;

namespace DIPS.Mobile.UI.Components.TextFields.Editor;

public partial class Editor : Microsoft.Maui.Controls.Editor
{
    public Editor()
    {
        PlaceholderColor = Colors.GetColor(ColorName.color_text_placeholder);
        FontFamily = "Body";
        FontSize = 16;
        TextColor = Colors.GetColor(ColorName.color_text_default);
        Keyboard = Keyboard.Text;
    }
}