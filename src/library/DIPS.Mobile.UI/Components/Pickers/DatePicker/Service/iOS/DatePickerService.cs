using DIPS.Mobile.UI.Components.Pickers.DatePicker.Inline.iOS;
using DIPS.Mobile.UI.Components.Pickers.DatePickerShared.iOS;

namespace DIPS.Mobile.UI.Components.Pickers.DatePicker.Service;

public partial class DatePickerService
{
    internal static WeakReference<InlineDatePickerPopoverViewController>? PresentedViewControllerReference { get; set; }
    
    public static partial void Open(DatePicker datePicker, View? sourceView = null)
    {
        if (IsOpen())
        {
            Close();
        }
        
        var inlineDatePicker = new InlineDatePicker
        {
            MaximumDate = datePicker.MaximumDate,
            MinimumDate = datePicker.MinimumDate,
            SelectedDate = datePicker.SelectedDate,
            IgnoreLocalTime = datePicker.IgnoreLocalTime,
            DisplayTodayButton = datePicker.DisplayTodayButton,
        };

        inlineDatePicker.SelectedDateCommand = new Command(() =>
        {
            if(datePicker.ShouldCloseOnDateSelected)
                Close();
            datePicker.SelectedDate = inlineDatePicker.SelectedDate;
            datePicker.SelectedDateCommand?.Execute(null);
        });

        var presentatedViewController = new InlineDatePickerPopoverViewController();
        PresentedViewControllerReference =
            new WeakReference<InlineDatePickerPopoverViewController>(presentatedViewController);
        presentatedViewController.Setup(inlineDatePicker, sourceView);
        
        var currentViewController = Platform.GetCurrentUIViewController();
        
        _ = currentViewController?.PresentViewControllerAsync(presentatedViewController, true);
    }

    internal static partial bool IsOpen() => PresentedViewControllerReference is not null;

    public static partial void Close()
    {
        if (PresentedViewControllerReference is not null && PresentedViewControllerReference.TryGetTarget(out var target))
        {
            _ = target.DismissViewControllerAsync(true);
        }
    }
}