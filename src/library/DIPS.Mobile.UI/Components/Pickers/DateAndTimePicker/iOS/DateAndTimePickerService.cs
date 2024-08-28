using DIPS.Mobile.UI.API.Library;
using DIPS.Mobile.UI.Components.Pickers.DatePickerShared.iOS;
using Microsoft.Maui.Platform;

namespace DIPS.Mobile.UI.Components.Pickers.DateAndTimePicker.iOS;

public class DateAndTimePickerService
{
    public static async void Open(DateAndTimePicker dateAndTimePicker, View chipTapped, bool datePickerTapped)
    {
        if (Platform.GetCurrentUIViewController() is DateOrTimePickerPopoverViewController dateOrTimePickerPopoverViewController)
        {
            await dateOrTimePickerPopoverViewController.DismissViewControllerAsync(true);
            return;
        }
        
        if (Platform.GetCurrentUIViewController() is DateAndTimePickerPopoverViewController dateAndTimePickerPopoverViewController)
        {
            if (dateAndTimePickerPopoverViewController.IsBeingDismissed)
            {
                await dateAndTimePickerPopoverViewController.DismissViewControllerAsync(true);
            }
            else
            {
                dateAndTimePickerPopoverViewController.OnTappedChip(datePickerTapped, chipTapped);
                return;
            }
        }

        var presentedViewController = new DateAndTimePickerPopoverViewController();
        presentedViewController.Setup(dateAndTimePicker, chipTapped, datePickerTapped);

        if (presentedViewController.PopoverPresentationController is not null)
        {
            presentedViewController.PopoverPresentationController.PassthroughViews = [dateAndTimePicker.DateChip.ToPlatform(DUI.GetCurrentMauiContext!), dateAndTimePicker.TimeChip.ToPlatform(DUI.GetCurrentMauiContext!)];
        }
        
        var currentViewController = Platform.GetCurrentUIViewController();
        
        _ = currentViewController?.PresentViewControllerAsync(presentedViewController, true);
    }

    internal static bool IsOpen()
    {
        return Platform.GetCurrentUIViewController() is DateAndTimePickerPopoverViewController;
    }

    public static void Close()
    {
        if (Platform.GetCurrentUIViewController() is DateAndTimePickerPopoverViewController viewController)
        {
            _ = viewController.DismissViewControllerAsync(true);
        }
    }
}