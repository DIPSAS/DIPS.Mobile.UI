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

        var nullableDateAndTimePicker = dateAndTimePicker as NullableDateAndTimePicker.NullableDateAndTimePicker;
        var dateOnOpen = dateAndTimePicker.SelectedDateTime;
        if (nullableDateAndTimePicker is not null)
        {
            dateOnOpen = nullableDateAndTimePicker.SelectedDateTime ?? DateTime.Now;

            if (nullableDateAndTimePicker.SelectedDateTime is null)
            {
                nullableDateAndTimePicker.SelectedDateTime = dateOnOpen;
                dateAndTimePicker.SelectedDateTimeCommand?.Execute(null);
            }
        }
        
        var inlineDateAndTimePicker = new InlineDateAndTimePicker
        {
            MaximumDate = dateAndTimePicker.MaximumDate,
            MinimumDate = dateAndTimePicker.MinimumDate,
            SelectedDateTime = dateOnOpen,
            IgnoreLocalTime = dateAndTimePicker.IgnoreLocalTime,
            DisplayTodayButton = dateAndTimePicker.DisplayTodayButton
        };

        inlineDateAndTimePicker.SelectedDateTimeCommand = new Command(() =>
        {
            if (nullableDateAndTimePicker is not null)
            {
                nullableDateAndTimePicker.SelectedDateTime = inlineDateAndTimePicker.SelectedDateTime;
            }
            else
            {
                dateAndTimePicker.SelectedDateTime = inlineDateAndTimePicker.SelectedDateTime;
            }
            dateAndTimePicker.SelectedDateTimeCommand?.Execute(null);
        });
       
        PresentedViewController = new InlineDatePickerPopoverViewController();
        PresentedViewController.Setup(inlineDateAndTimePicker, sourceView, nullableDateAndTimePicker);
        
        var currentViewController = Platform.GetCurrentUIViewController();
        
        _ = currentViewController?.PresentViewControllerAsync(PresentedViewController, true);
    }

    internal static bool IsOpen() => PresentedViewController is not null;

    public static void Close() => _ = PresentedViewController?.DismissViewControllerAsync(true);
}