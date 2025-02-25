using Microsoft.Maui.Platform;
using UIKit;
using Colors = DIPS.Mobile.UI.Resources.Colors.Colors;

namespace DIPS.Mobile.UI.Components.Selection.iOS;

public class SwitchHandler : Microsoft.Maui.Handlers.SwitchHandler
{
    protected override void ConnectHandler(UISwitch platformView)
    {
        base.ConnectHandler(platformView);

        // If the Switch is over a background color, this will ensure the Switch keeps its "off" color
        // https://stackoverflow.com/questions/10348869/change-color-of-uiswitch-in-off-state
        platformView.Layer.CornerRadius = platformView.Frame.Height / 2;
        platformView.ClipsToBounds = true;
        platformView.BackgroundColor = Colors.GetColor(ColorName.color_stroke_default).ToPlatform();
    }
}