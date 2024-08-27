using DIPS.Mobile.UI.API.Library;
using DIPS.Mobile.UI.Components.Pickers.DatePickerShared.iOS;
using Microsoft.Maui.Platform;

namespace DIPS.Mobile.UI.Components.Pickers.DateAndTimePicker.iOS;

public class DateAndTimePickerService
{
    internal static DateAndTimePickerPopoverViewController? PresentedViewController { get; set; }
    
    public static async void Open(DateAndTimePicker dateAndTimePicker, View chipTapped, bool datePickerTapped)
    {
        if (Platform.GetCurrentUIViewController() is DateAndTimePickerPopoverViewController viewController)
        {
            if (viewController.IsBeingDismissed)
            {
                await viewController.DismissViewControllerAsync(false);
            }
            else
            {
                viewController.OnTappedChip(datePickerTapped, chipTapped);
                return;
            }
        }

        PresentedViewController = new DateAndTimePickerPopoverViewController();
        PresentedViewController.Setup(dateAndTimePicker, chipTapped, datePickerTapped);

        if (PresentedViewController.PopoverPresentationController is not null)
        {
            PresentedViewController.PopoverPresentationController.PassthroughViews = [dateAndTimePicker.DateChip.ToPlatform(DUI.GetCurrentMauiContext!), dateAndTimePicker.TimeChip.ToPlatform(DUI.GetCurrentMauiContext!)];
        }
        
        var currentViewController = Platform.GetCurrentUIViewController();
        
        _ = currentViewController?.PresentViewControllerAsync(PresentedViewController, true);
    }

    internal static bool IsOpen() => PresentedViewController is not null;

    public static void Close() => _ = PresentedViewController?.DismissViewControllerAsync(true);
}