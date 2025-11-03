using Microsoft.Maui.Platform;

namespace DIPS.Mobile.UI.Components.Labels.CheckTruncatedLabel;

public partial class CheckTruncatedLabelHandler
{
    protected override MauiLabel CreatePlatformView()
    {
        return new iOS.MauiLabel(VirtualView as CheckTruncatedLabel);
    }
}
