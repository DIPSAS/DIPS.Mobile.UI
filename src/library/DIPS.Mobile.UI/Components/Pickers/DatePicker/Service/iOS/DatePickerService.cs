using DIPS.Mobile.UI.Components.Pickers.DatePicker.Inline.iOS;
using DIPS.Mobile.UI.Components.Pickers.DatePickerShared.iOS;
using DIPS.Mobile.UI.Components.Pickers.NullableDatePickerShared;

namespace DIPS.Mobile.UI.Components.Pickers.DatePicker.Service;

public partial class DatePickerService
{
    internal static WeakReference<InlineDatePickerPopoverViewController>? PresentedViewControllerReference { get; set; }
    
    public static async partial void Open(DatePicker datePicker, View? sourceView = null)
    {
        if (Platform.GetCurrentUIViewController() is InlineDatePickerPopoverViewController viewController)
        {
            await viewController.DismissViewControllerAsync(false);
        }
        
        if (IsOpen())
        {
            Close();
        }
        
        var nullableDatePicker = datePicker as NullableDatePicker.NullableDatePicker;
        
        var dateOnOpen = datePicker.SelectedDate;
        if (nullableDatePicker is not null)
        {
            dateOnOpen = nullableDatePicker.SelectedDate ?? DateTime.Now;

            // If the date is null, set it to the current date and execute the SelectedDateCommand
            if (nullableDatePicker.SelectedDate is null)
            {
                nullableDatePicker.SelectedDate = dateOnOpen;
                datePicker.SelectedDateCommand?.Execute(null);
            }
        }
        
        var inlineDatePicker = new InlineDatePicker
        {
            MaximumDate = datePicker.MaximumDate,
            MinimumDate = datePicker.MinimumDate,
            SelectedDate = dateOnOpen,
            IgnoreLocalTime = datePicker.IgnoreLocalTime,
            DisplayTodayButton = datePicker.DisplayTodayButton
        };

        inlineDatePicker.SelectedDateCommand = new Command(() =>
        {
            if(datePicker.ShouldCloseOnDateSelected)
                Close();

            if (nullableDatePicker is not null)
            {
                nullableDatePicker.SelectedDate = inlineDatePicker.SelectedDate;
            }
            else
            {
                datePicker.SelectedDate = inlineDatePicker.SelectedDate;
            }
            datePicker.SelectedDateCommand?.Execute(null);
        });

        var presentedViewController = new InlineDatePickerPopoverViewController();
        PresentedViewControllerReference =
            new WeakReference<InlineDatePickerPopoverViewController>(presentedViewController);
        presentedViewController.Setup(inlineDatePicker, sourceView, nullableDatePicker);
        
        var currentViewController = Platform.GetCurrentUIViewController();
        
        _ = currentViewController?.PresentViewControllerAsync(presentedViewController, true);
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