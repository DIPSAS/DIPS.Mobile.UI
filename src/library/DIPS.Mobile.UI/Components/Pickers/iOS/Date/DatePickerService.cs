using CoreGraphics;
using DIPS.Mobile.UI.Components.Pickers.iOS.Date;
using UIKit;
using UIPopoverPresentationControllerDelegate = DIPS.Mobile.UI.Components.Pickers.iOS.Date.UIPopoverPresentationControllerDelegate;

namespace DIPS.Mobile.UI.Components.Pickers;

public static partial class DatePickerService
{
    internal const string DatePickerRestorationIdentifier = nameof(UIDatePickerViewController);

    public static partial void OpenDatePicker(DatePicker datePicker)
    {
        if (datePicker is not {IsOpen: true}) return;

        var currentViewController = API.Library.iOS.DUI.CurrentViewController;
        if (currentViewController?.View == null) return;
        
        var viewController = new UIDatePickerViewController(datePicker);
        viewController.RestorationIdentifier = DatePickerRestorationIdentifier;
        var screenWidth = UIScreen.MainScreen.Bounds.Width;
        var screenHeight = UIScreen.MainScreen.Bounds.Height;
        var sizeOfPopover = new CGSize(screenWidth - 30, screenHeight / 2);
        viewController.PreferredContentSize = sizeOfPopover;
        viewController.ModalPresentationStyle = UIModalPresentationStyle.Popover;
        var popoverPresentationController = viewController.PopoverPresentationController;
        if (popoverPresentationController != null)
        {
            // set up the popover presentation controller
            popoverPresentationController.PermittedArrowDirections =
                0; //This allows the popover to display over / under the native view bounds depending on where the native view is placed in the page
            popoverPresentationController.Delegate = new UIPopoverPresentationControllerDelegate();
            
            popoverPresentationController.SourceView = currentViewController.View; //This is to make the popover relative to the entire page and not a particular view in the page for it to be placed correctly on the screen;
            popoverPresentationController.SourceRect = new CGRect(screenWidth - sizeOfPopover.Width,
                screenHeight-sizeOfPopover.Height, 0, 0);
        }

        
        currentViewController.PresentViewController(viewController, true, null);
    }
        
    internal static async Task Close()
    {
        var potentialBottomSheetUiViewController = API.Library.iOS.DUI.CurrentViewController;
        if (potentialBottomSheetUiViewController != null)
        {
            await potentialBottomSheetUiViewController.DismissViewControllerAsync(false);
            await Task.Delay(100); //Small delay before its actually removed.    
        }
    }

    internal static bool IsOpen()
    {
        return API.Library.iOS.DUI.CurrentViewController?.RestorationIdentifier == DatePickerRestorationIdentifier;
    }
    
}