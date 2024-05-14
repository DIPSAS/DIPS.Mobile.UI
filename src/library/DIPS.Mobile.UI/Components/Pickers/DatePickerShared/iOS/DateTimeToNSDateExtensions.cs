using Foundation;

namespace DIPS.Mobile.UI.Components.Pickers.DatePickerShared.iOS;

public static class DateTimeToNSDateExtensions
{
    public static NSDate ConvertDate(this DateTime date, bool ignoreLocalTime)
    {
        var dateTime =  ignoreLocalTime ? date.ToUniversalTime() : date.ToLocalTime();
        if (dateTime.Kind == DateTimeKind.Unspecified) 
        {
            dateTime = DateTime.SpecifyKind(dateTime, ignoreLocalTime ? DateTimeKind.Utc : DateTimeKind.Local);
        }

        return (NSDate)dateTime;
    }
    
    public static DateTime ConvertDate(this NSDate date, bool ignoreLocalTime)
    {
        var dateTime =  ignoreLocalTime ? ((DateTime)date).ToUniversalTime() : ((DateTime)date).ToLocalTime();
        if (dateTime.Kind == DateTimeKind.Unspecified) 
        {
            dateTime = DateTime.SpecifyKind(dateTime, ignoreLocalTime ? DateTimeKind.Utc : DateTimeKind.Local);
        }

        return dateTime;
    }

}