using DIPS.Mobile.UI.Exceptions;

namespace DIPS.Mobile.UI.Components.Pickers.TimePicker;

public partial class TimePickerService
{
    public static partial void Open(TimePicker timePicker, View? sourceView = null) =>
        throw new Only_Here_For_UnitTests();

    internal static partial bool IsOpen() =>
        throw new Only_Here_For_UnitTests();

    public static partial void Close() =>
        throw new Only_Here_For_UnitTests();
}