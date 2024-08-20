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

        var dateOnOpen = datePicker.SetSelectedDateOnPopoverOpen();
        
        var inlineDatePicker = new InlineDatePicker
        {
            MaximumDate = datePicker.MaximumDate,
            MinimumDate = datePicker.MinimumDate,
            SelectedDate = dateOnOpen,
            IgnoreLocalTime = datePicker.IgnoreLocalTime,
            DisplayTodayButton = datePicker.DisplayTodayButton
        };

        var presentedViewController = new InlineDatePickerPopoverViewController();
        PresentedViewControllerReference =
            new WeakReference<InlineDatePickerPopoverViewController>(presentedViewController);
        presentedViewController.Setup(inlineDatePicker, datePicker, sourceView);
        
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