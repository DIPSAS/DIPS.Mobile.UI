using UIKit;

namespace DIPS.Mobile.UI.API.Tip;

internal class TipUIPopoverPresentationControllerDelegate : UIPopoverPresentationControllerDelegate
{
    public override UIModalPresentationStyle GetAdaptivePresentationStyle(UIPresentationController controller,
        UITraitCollection traitCollection)
    {
        return UIModalPresentationStyle.None;
    }
}