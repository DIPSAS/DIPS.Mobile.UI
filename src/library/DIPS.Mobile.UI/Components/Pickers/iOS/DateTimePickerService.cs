using CoreGraphics;
using DIPS.Mobile.UI.Components.Pickers.DateTimePickers;
using UIKit;
using DIPS.Mobile.UI.Components.Pickers.iOS;
using DIPS.Mobile.UI.Components.Pickers.iOS.Date;
using DIPS.Mobile.UI.Components.Pickers.iOS.DateTime;
using DIPS.Mobile.UI.Components.Pickers.iOS.Time;
using DatePicker = DIPS.Mobile.UI.Components.Pickers.DateTimePickers.DatePicker;
using TimePicker = DIPS.Mobile.UI.Components.Pickers.DateTimePickers.TimePicker;
using UIModalPresentationStyle = UIKit.UIModalPresentationStyle;
using UIPopoverPresentationControllerDelegate = DIPS.Mobile.UI.Components.Pickers.iOS.UIPopoverPresentationControllerDelegate;

namespace DIPS.Mobile.UI.Components.Pickers;

public static partial class DateTimePickerService
{
    private const string DatePickerRestorationIdentifier = nameof(UIDatePickerViewController);

    public static partial void OpenDateTimePicker(IDateTimePicker dateTimePicker)
    {
        if (dateTimePicker is not {IsOpen: true}) return;
        
        var currentViewController = API.Library.iOS.DUI.CurrentViewController;
        if (currentViewController?.View == null) return;
        
        UIDateTimePickerViewController? viewController = null;

        if (dateTimePicker is DatePicker datePicker)
        {
            viewController = new UIDatePickerViewController(datePicker);
        }
        else if (dateTimePicker is TimePicker timePicker)
        {
            viewController = new UITimePickerViewController(timePicker);
        }
        else if (dateTimePicker is DateAndTimePicker dateAndTimePicker)
        {
            viewController = new UIDateAndTimePickerViewController(dateAndTimePicker);
        }
        
        viewController!.RestorationIdentifier = DatePickerRestorationIdentifier;
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

    public static partial Task Close()
    {
        return Task.CompletedTask;
    }
}
