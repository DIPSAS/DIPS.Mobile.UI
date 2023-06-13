using Microsoft.Maui.Controls.Shapes;
using Colors = DIPS.Mobile.UI.Resources.Colors.Colors;

namespace DIPS.Mobile.UI.Components.Dividers;

public class Divider : ContentView
{
    public Divider()
    {
        BackgroundColor = Colors.GetColor(ColorName.color_neutral_40);

        var line = new Line();
        line.SetBinding(BackgroundProperty, new Binding(nameof(BackgroundProperty), source: this));
        Content = line;
    }

}