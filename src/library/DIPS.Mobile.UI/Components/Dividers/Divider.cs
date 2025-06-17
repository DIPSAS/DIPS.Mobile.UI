using DIPS.Mobile.UI.Internal;
using Microsoft.Maui.Controls.Shapes;

namespace DIPS.Mobile.UI.Components.Dividers;

public class Divider : ContentView
{
    internal static string s_automationId = "Divider".ToDUIAutomationId<Divider>();
    
    public Divider()
    {
        this.SetAppThemeColor(BackgroundColorProperty, ColorName.color_stroke_default);

        var line = new Line{ AutomationId = "Line".ToDUIAutomationId<Divider>()};
        line.SetBinding(BackgroundProperty, static (Divider divider) => divider.BackgroundColor, source: this);
        Content = line;
    }
}