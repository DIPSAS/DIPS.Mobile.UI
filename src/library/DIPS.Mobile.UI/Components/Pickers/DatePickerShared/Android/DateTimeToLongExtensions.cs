namespace DIPS.Mobile.UI.Components.Pickers.DatePickerShared.Android;

public static class DateTimeToLongExtensions
{
    public static long ToLong(this DateTime dateTime) => (long)(dateTime - DateTime.UnixEpoch).TotalMilliseconds;
}