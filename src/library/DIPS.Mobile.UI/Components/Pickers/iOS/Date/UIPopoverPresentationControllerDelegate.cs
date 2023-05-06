using UIKit;

namespace DIPS.Mobile.UI.Components.Pickers.iOS.Date
{
    internal class UIPopoverPresentationControllerDelegate : UIKit.UIPopoverPresentationControllerDelegate
    {
        public override UIModalPresentationStyle GetAdaptivePresentationStyle(UIPresentationController controller,
            UITraitCollection traitCollection)
        {
            return UIModalPresentationStyle.None;
        }
    }
}