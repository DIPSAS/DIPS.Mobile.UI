using UIKit;

namespace DIPS.Mobile.UI.API.Tip;

internal class TipUIPopoverPresentationControllerDelegate : UIPopoverPresentationControllerDelegate
{
    public TipUIViewController? TipUiViewController { get; set; }
    
    public override UIModalPresentationStyle GetAdaptivePresentationStyle(UIPresentationController controller,
        UITraitCollection traitCollection)
    {
        return UIModalPresentationStyle.None;
    }

    public override void DidDismiss(UIPresentationController presentationController)
    {
        TipUiViewController?.Dispose();
    }
}