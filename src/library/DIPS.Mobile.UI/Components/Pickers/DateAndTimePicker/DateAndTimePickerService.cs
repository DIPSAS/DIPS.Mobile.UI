namespace DIPS.Mobile.UI.Components.Pickers.DateAndTimePicker;

public partial class DateAndTimePickerService
{
    public static partial void Open(DateAndTimePicker dateAndTimePicker, View chipTapped, bool datePickerTapped);

    internal static partial bool IsOpen();

    public static partial void Close();   
}