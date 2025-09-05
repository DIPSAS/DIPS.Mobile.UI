namespace DIPS.Mobile.UI.Components.Labels;

public partial class CustomTruncationLabelHandler
{
    protected override MauiLabel CreatePlatformView()
    {
        return new MauiLabel(VirtualView as CustomTruncationLabel);
    }
}