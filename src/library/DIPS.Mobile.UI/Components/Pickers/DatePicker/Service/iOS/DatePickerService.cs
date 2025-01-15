using DIPS.Mobile.UI.Components.Pickers.DatePicker.Inline.iOS;
using DIPS.Mobile.UI.Components.Pickers.DatePickerShared.iOS;
using UIKit;

namespace DIPS.Mobile.UI.Components.Pickers.DatePicker.Service;

public partial class DatePickerService
{
    public static async partial void Open(DatePicker datePicker, View? sourceView = null)
    {
        if (Platform.GetCurrentUIViewController() is IDatePickerPopoverViewController and UIViewController uiViewController)
        {
            await uiViewController.DismissViewControllerAsync(true);
            return;
        }

        var dateOnOpen = datePicker.GetDateOnOpen();
        datePicker.SetSelectedDateTime(dateOnOpen);
        
        var inlineDatePicker = new InlineDatePicker
        {
            MaximumDate = datePicker.MaximumDate,
            MinimumDate = datePicker.MinimumDate,
            SelectedDate = dateOnOpen,
            IgnoreLocalTime = datePicker.IgnoreLocalTime,
            ShouldDisplayTodayButton = datePicker.ShouldDisplayTodayButton
        };

        var presentedViewController = new DateOrTimePickerPopoverViewController();
        presentedViewController.Setup(inlineDatePicker, datePicker, sourceView);
        
        var currentViewController = Platform.GetCurrentUIViewController();
        
        _ = currentViewController?.PresentViewControllerAsync(presentedViewController, true);
        
        datePicker.HandlerChanging += DatePickerOnHandlerChanging;
    }

    private static void DatePickerOnHandlerChanging(object? sender, HandlerChangingEventArgs e)
    {
        if (e.NewHandler is null)
        {
            Close();
        }
        
        if(sender is DatePicker datePicker)
        {
            datePicker.HandlerChanging -= DatePickerOnHandlerChanging;
        }
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