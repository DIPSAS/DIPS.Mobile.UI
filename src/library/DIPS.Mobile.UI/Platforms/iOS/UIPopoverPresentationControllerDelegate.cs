using UIKit;

namespace DIPS.Mobile.UI.Platforms.iOS
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