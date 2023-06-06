using DIPS.Mobile.UI.Exceptions;

namespace DIPS.Mobile.UI.Components.Pickers.DatePicker.Service;

public partial class DatePickerService
{
    public static partial void Open(DatePicker datePicker);

    internal static partial bool IsOpen();

    public static partial void Close();
}
