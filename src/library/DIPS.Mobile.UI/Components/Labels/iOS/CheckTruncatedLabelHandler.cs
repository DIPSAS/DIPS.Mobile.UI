namespace DIPS.Mobile.UI.Components.Labels;

public partial class CheckTruncatedLabelHandler
{
    protected override MauiLabel CreatePlatformView()
    {
        return new MauiLabel(VirtualView as CheckTruncatedLabel);
    }
}
