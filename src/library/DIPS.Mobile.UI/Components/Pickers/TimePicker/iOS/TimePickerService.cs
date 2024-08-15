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
        
        var inlineDatePicker = new InlineTimePicker
        {
            SelectedTime = timePicker.SelectedTime
        };

        inlineDatePicker.SelectedTimeCommand = new Command(() =>
        {
            timePicker.SelectedTime = inlineDatePicker.SelectedTime;
            timePicker.SelectedTimeCommand?.Execute(null);
        });
       
        PresentedViewController = new InlineDatePickerPopoverViewController();
        PresentedViewController.Setup(inlineDatePicker, sourceView);
        
        var currentViewController = Platform.GetCurrentUIViewController();

        _ = currentViewController?.PresentViewControllerAsync(PresentedViewController, true);
    }

    internal static partial bool IsOpen() => PresentedViewController is not null;

    public static partial void Close() => _ = PresentedViewController?.DismissViewControllerAsync(true);
}