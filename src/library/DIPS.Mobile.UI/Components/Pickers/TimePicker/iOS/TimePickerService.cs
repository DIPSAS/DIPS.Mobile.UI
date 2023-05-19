using DIPS.Mobile.UI.Components.Pickers.DateTimePickers.TimePicker.iOS;

// ReSharper disable once CheckNamespace
namespace DIPS.Mobile.UI.Components.Pickers.TimePicker;

/// <summary>
/// Will implement later
/// </summary>
public partial class TimePickerService
{
    private const string DatePickerRestorationIdentifier = nameof(UITimePickerViewController);

    public static partial void OpenTimePicker(TimePicker timePicker)
    {
        
        /*var currentViewController = API.Library.iOS.DUI.CurrentViewController;
        if (currentViewController?.View == null) return;
        
        UITimePickerViewController viewController = new UITimePickerViewController(timePicker);

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

        
        currentViewController.PresentViewController(viewController, true, null);*/
    }

    public static partial Task Close()
    {
        return Task.CompletedTask;
    }
}