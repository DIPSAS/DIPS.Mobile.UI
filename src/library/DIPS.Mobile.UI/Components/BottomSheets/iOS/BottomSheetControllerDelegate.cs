using UIKit;
using UIModalPresentationStyle = UIKit.UIModalPresentationStyle;

namespace DIPS.Mobile.UI.Components.BottomSheets.iOS;

internal class BottomSheetControllerDelegate : UISheetPresentationControllerDelegate
{
    public BottomSheetViewController? BottomSheetViewController { get; set; }

    public override void WillPresent(UIPresentationController presentationController, UIModalPresentationStyle style,
        IUIViewControllerTransitionCoordinator? transitionCoordinator)
    {
        BottomSheetViewController?.Opened();
    }

    public override void DidChangeSelectedDetentIdentifier(UISheetPresentationController sheetPresentationController)
    {
        BottomSheetViewController!.BottomSheet.Positioning =
            BottomSheetViewController.GetCurrentPosition(sheetPresentationController);
    }

    public override void DidDismiss(UIPresentationController presentationController)
    {
        BottomSheetViewController?.Dispose();
    }
    
}