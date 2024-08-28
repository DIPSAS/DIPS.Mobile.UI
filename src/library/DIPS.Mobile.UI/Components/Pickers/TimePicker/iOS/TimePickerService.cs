using DIPS.Mobile.UI.Components.Pickers.DatePicker.Inline.iOS;
using DIPS.Mobile.UI.Components.Pickers.DatePickerShared.iOS;
using UIKit;

namespace DIPS.Mobile.UI.Components.Pickers.TimePicker;

public partial class TimePickerService
{
    
    public static async partial void Open(TimePicker timePicker, View? sourceView = null)
    {
        if (Platform.GetCurrentUIViewController() is IDatePickerPopoverViewController and UIViewController uiViewController)
        {
            await uiViewController.DismissViewControllerAsync(true);
            return;
        }

        var timeOnOpen = timePicker.SetSelectedTimeOnPopoverOpen();
        
        var inlineDatePicker = new InlineTimePicker
        {
            SelectedTime = timeOnOpen
        };

        var presentedViewController = new DateOrTimePickerPopoverViewController();
        presentedViewController.Setup(inlineDatePicker, timePicker, sourceView);
        
        var currentViewController = Platform.GetCurrentUIViewController();

        _ = currentViewController?.PresentViewControllerAsync(presentedViewController, true);
    }

    internal static partial bool IsOpen()
    {
        return Platform.GetCurrentUIViewController() is DateOrTimePickerPopoverViewController;
    }

    public static partial void Close()
    {
        if (Platform.GetCurrentUIViewController() is DateOrTimePickerPopoverViewController viewController)
        {
            _ = viewController.DismissViewControllerAsync(true);
        }
    }
}