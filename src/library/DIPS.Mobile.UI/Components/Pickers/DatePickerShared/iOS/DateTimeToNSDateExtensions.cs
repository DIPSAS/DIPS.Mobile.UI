using Foundation;

namespace DIPS.Mobile.UI.Components.Pickers.DatePickerShared.iOS;

public static class DateTimeToNSDateExtensions
{
    public static NSDate ConvertDate(this DateTime date, bool ignoreLocalTime)
    {
        if (date.Kind == DateTimeKind.Unspecified) 
        {
            date = DateTime.SpecifyKind(date, ignoreLocalTime ? DateTimeKind.Utc : DateTimeKind.Local);
        }

        return (NSDate)date;
    }
    
    public static DateTime ConvertDate(this NSDate date, bool ignoreLocalTime)
    {
        return ignoreLocalTime ? ((DateTime)date).ToUniversalTime() : ((DateTime)date).ToLocalTime();
    }

}