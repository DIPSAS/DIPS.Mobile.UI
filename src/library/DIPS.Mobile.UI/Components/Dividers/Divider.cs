using DIPS.Mobile.UI.Internal;
using Microsoft.Maui.Controls.Shapes;

namespace DIPS.Mobile.UI.Components.Dividers;

public class Divider : ContentView
{
    public Divider()
    {
        this.SetAppThemeColor(BackgroundColorProperty, ColorName.color_neutral_20);

        var line = new Line(){ AutomationId = "Line".ToDUIAutomationId<Divider>()};
        line.SetBinding(BackgroundProperty, new Binding(nameof(BackgroundColor), source: this));
        Content = line;
    }

}