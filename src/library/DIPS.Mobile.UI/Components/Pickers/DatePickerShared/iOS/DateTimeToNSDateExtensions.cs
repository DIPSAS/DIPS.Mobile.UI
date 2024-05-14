using Foundation;

namespace DIPS.Mobile.UI.Components.Pickers.DatePickerShared.iOS;

public static class DateTimeToNSDateExtensions
{
    public static NSDate ConvertDate(this DateTime date, bool ignoreLocalTime)
    {
        if (date.Kind == DateTimeKind.Unspecified && !ignoreLocalTime) 
        {
            date = DateTime.SpecifyKind(date, DateTimeKind.Local);
        }

        return (NSDate)date;
    } 

}