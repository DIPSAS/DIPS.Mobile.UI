using DIPS.Mobile.UI.Exceptions;

namespace DIPS.Mobile.UI.Components.Pickers.DatePicker.Service;

public partial class TimePickerService
{
    public static partial void Open(DatePicker datePicker, View? sourceView) => throw new Only_Here_For_UnitTests();

    internal static partial bool IsOpen() => throw new Only_Here_For_UnitTests();

    public static partial void Close() => throw new Only_Here_For_UnitTests();
}