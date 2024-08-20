using DIPS.Mobile.UI.Components.Pickers.DatePicker.Inline.iOS;
using DIPS.Mobile.UI.Components.Pickers.DatePickerShared.iOS;

namespace DIPS.Mobile.UI.Components.Pickers.DateAndTimePicker.iOS;

public class DateAndTimePickerService
{
    internal static InlineDatePickerPopoverViewController? PresentedViewController { get; set; }
    
    public static async void Open(DateAndTimePicker dateAndTimePicker, View? sourceView = null)
    {
        if (Platform.GetCurrentUIViewController() is InlineDatePickerPopoverViewController viewController)
        {
            await viewController.DismissViewControllerAsync(false);
        }
        
        if (IsOpen())
        {
            Close();
        }

        var dateTimeOnOpen = dateAndTimePicker.SetSelectedDateTimeOnPopoverOpen();
        
        var inlineDateAndTimePicker = new InlineDateAndTimePicker
        {
            MaximumDate = dateAndTimePicker.MaximumDate,
            MinimumDate = dateAndTimePicker.MinimumDate,
            SelectedDateTime = dateTimeOnOpen,
            IgnoreLocalTime = dateAndTimePicker.IgnoreLocalTime
        };

        PresentedViewController = new InlineDatePickerPopoverViewController();
        PresentedViewController.Setup(inlineDateAndTimePicker, dateAndTimePicker, sourceView);
        
        var currentViewController = Platform.GetCurrentUIViewController();
        
        _ = currentViewController?.PresentViewControllerAsync(PresentedViewController, true);
    }

    internal static bool IsOpen() => PresentedViewController is not null;

    public static void Close() => _ = PresentedViewController?.DismissViewControllerAsync(true);
}