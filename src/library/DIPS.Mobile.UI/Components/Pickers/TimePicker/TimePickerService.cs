namespace DIPS.Mobile.UI.Components.Pickers.TimePicker;

public partial class TimePickerService
{
    public static partial void Open(TimePicker timePicker, View? sourceView = null);

    internal static partial bool IsOpen();

    public static partial void Close();
}
