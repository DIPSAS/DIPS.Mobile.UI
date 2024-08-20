using DIPS.Mobile.UI.Components.Pickers.DatePicker.Inline.iOS;
using DIPS.Mobile.UI.Components.Pickers.DatePicker.Service;
using DIPS.Mobile.UI.Components.Pickers.DatePickerShared.iOS;

namespace DIPS.Mobile.UI.Components.Pickers.TimePicker;

public partial class TimePickerService
{
    internal static InlineDatePickerPopoverViewController? PresentedViewController { get; set; }
    
    public static async partial void Open(TimePicker timePicker, View? sourceView = null)
    {
        if (Platform.GetCurrentUIViewController() is InlineDatePickerPopoverViewController viewController)
        {
            await viewController.DismissViewControllerAsync(false);
        }
        
        if (IsOpen())
        {
            Close();
        }

        var timeOnOpen = timePicker.SelectedTime;
        
        var nullableTimePicker = timePicker as NullableTimePicker.NullableTimePicker;
        if (nullableTimePicker is not null)
        {
            timeOnOpen = nullableTimePicker.SelectedTime ?? new TimeSpan(DateTime.Now.Hour, DateTime.Now.Minute, DateTime.Now.Second);

            // If the date is null, set it to the current date and execute the SelectedDateCommand
            if (nullableTimePicker.SelectedTime is null)
            {
                nullableTimePicker.SelectedTime = timeOnOpen;
                timePicker.SelectedTimeCommand?.Execute(null);
            }
        }
        
        var inlineDatePicker = new InlineTimePicker
        {
            SelectedTime = timeOnOpen
        };

        inlineDatePicker.SelectedTimeCommand = new Command(() =>
        {
            timePicker.SelectedTime = inlineDatePicker.SelectedTime;
            timePicker.SelectedTimeCommand?.Execute(null);
        });
       
        PresentedViewController = new InlineDatePickerPopoverViewController();
        PresentedViewController.Setup(inlineDatePicker, sourceView, null);
        
        var currentViewController = Platform.GetCurrentUIViewController();

        _ = currentViewController?.PresentViewControllerAsync(PresentedViewController, true);
    }

    internal static partial bool IsOpen() => PresentedViewController is not null;

    public static partial void Close() => _ = PresentedViewController?.DismissViewControllerAsync(true);
}