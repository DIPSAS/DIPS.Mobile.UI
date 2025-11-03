using Microsoft.Maui.Platform;

namespace DIPS.Mobile.UI.Components.Labels.CheckTruncatedLabel;

public partial class CheckTruncatedLabelHandler
{
    protected override MauiLabel CreatePlatformView()
    {
        return new iOS.MauiLabel(VirtualView as CheckTruncatedLabel);
    }

    public override Size GetDesiredSize(double widthConstraint, double heightConstraint)
    {
        return base.GetDesiredSize(widthConstraint, heightConstraint);
    }
}
