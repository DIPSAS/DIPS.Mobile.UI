using DIPS.Mobile.UI.Platforms.iOS;
using Microsoft.Maui.Platform;
using UIKit;

namespace DIPS.Mobile.UI.Components.Buttons;

public partial class ButtonHandler : Microsoft.Maui.Handlers.ButtonHandler
{
    protected override UIButton CreatePlatformView()
    {
        return new UIButtonWithExtraTappableArea();
    }
    private partial void AppendPropertyMapper()
    {
    }

    private static partial void MapAdditionalHitBoxSize(ButtonHandler handler, Button button)
    {
        if (handler.PlatformView is UIButtonWithExtraTappableArea uiButton)
            uiButton.AdditionalHitBoxSize = button.AdditionalHitBoxSize;
    }
}