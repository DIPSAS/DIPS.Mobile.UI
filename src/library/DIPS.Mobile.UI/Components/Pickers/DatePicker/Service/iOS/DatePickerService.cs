using DIPS.Mobile.UI.Components.BottomSheets;
using DIPS.Mobile.UI.Components.Pickers.DatePicker.Inline;
using DIPS.Mobile.UI.Components.Pickers.DatePicker.Inline.iOS;

namespace DIPS.Mobile.UI.Components.Pickers.DatePicker.Service;

public partial class DatePickerService
{
    public static partial void Open(DatePicker datePicker)
    {
        if (IsOpen())
        {
            Close();
        }
        
        var inlineDatePicker = new InlineDatePicker()
        {
             MaximumDate = datePicker.MaximumDate,
             MinimumDate = datePicker.MinimumDate,
            SelectedDate = datePicker.SelectedDate,
            IgnoreLocalTime = datePicker.IgnoreLocalTime
        };
        inlineDatePicker.SelectedDateCommand = new Command(() =>
        {
            Close();
            datePicker.SelectedDate = inlineDatePicker.SelectedDate;
            datePicker.SelectedDateCommand?.Execute(null);
        });
        BottomSheetService.OpenBottomSheet(new BottomSheet() {Content = inlineDatePicker, ShouldFitToContent = true});
    }

    internal static partial bool IsOpen() => BottomSheetService.IsBottomSheetOpen();

    public static partial void Close() => BottomSheetService.CloseCurrentBottomSheet(true);
}