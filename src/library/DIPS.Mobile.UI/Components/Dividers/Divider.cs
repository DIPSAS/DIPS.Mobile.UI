using Colors = DIPS.Mobile.UI.Resources.Colors.Colors;

namespace DIPS.Mobile.UI.Components.Dividers;

public class Divider : ContentView
{
    public Divider()
    {
        BackgroundColor = Colors.GetColor(ColorName.color_neutral_40);
#if __IOS__
        var line = new Microsoft.Maui.Controls.Shapes.Line();
        line.SetBinding(BackgroundColorProperty, new Binding(nameof(BackgroundColor), source: this));
        Content = line;
#endif
    }
}