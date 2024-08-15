using DIPS.Mobile.UI.Components.Pickers.DatePicker.Inline.iOS;
using DIPS.Mobile.UI.Components.Pickers.DatePickerShared.iOS;

namespace DIPS.Mobile.UI.Components.Pickers.DateAndTimePicker.iOS;

public class DateAndTimePickerService
{
    internal static InlineDatePickerPopoverViewController? PresentedViewController { get; set; }
    
    public static void Open(DateAndTimePicker datePicker, View? sourceView = null)
    {
        if (IsOpen())
        {
            Close();
        }
        
        var inlineDateAndTimePicker = new InlineDateAndTimePicker
        {
            MaximumDate = datePicker.MaximumDate,
            MinimumDate = datePicker.MinimumDate,
            SelectedDateTime = datePicker.SelectedDateTime,
            IgnoreLocalTime = datePicker.IgnoreLocalTime
        };

        inlineDateAndTimePicker.SelectedDateTimeCommand = new Command(() =>
        {
            datePicker.SelectedDateTime = inlineDateAndTimePicker.SelectedDateTime;
            datePicker.SelectedDateTimeCommand?.Execute(null);
        });
       
        PresentedViewController = new InlineDatePickerPopoverViewController();
        PresentedViewController.Setup(inlineDateAndTimePicker, sourceView);
        
        var currentViewController = Platform.GetCurrentUIViewController();
        
        _ = currentViewController?.PresentViewControllerAsync(PresentedViewController, true);
    }

    internal static bool IsOpen() => PresentedViewController is not null;

    public static void Close() => _ = PresentedViewController?.DismissViewControllerAsync(true);
}