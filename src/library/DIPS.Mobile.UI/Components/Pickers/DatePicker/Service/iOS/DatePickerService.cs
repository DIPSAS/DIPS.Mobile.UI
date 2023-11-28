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
       
        BottomSheetService.Open(new DatePickerBottomSheet(datePicker));
    }

    internal static partial bool IsOpen() => BottomSheetService.IsOpen();

    public static partial void Close() => BottomSheetService.CloseAll(true);
}