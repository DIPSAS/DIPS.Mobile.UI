using DIPS.Mobile.UI.Internal;

namespace DIPS.Mobile.UI.Components.Dividers;

public class Divider : BoxView
{
    internal static string s_automationId = "Divider".ToDUIAutomationId<Divider>();
    
    public Divider()
    {
        this.SetAppThemeColor(BackgroundColorProperty, ColorName.color_stroke_default);

        HeightRequest = 1;

        AutomationId = s_automationId;
    }
}