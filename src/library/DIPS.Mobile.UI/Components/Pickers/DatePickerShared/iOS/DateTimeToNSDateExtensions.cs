using Foundation;
using Microsoft.Maui.Platform;

namespace DIPS.Mobile.UI.Components.Pickers.DatePickerShared.iOS;

public static class DateTimeToNSDateExtensions
{
    public static NSDate ConvertAndCastToNsDate(this DateTime date, bool ignoreLocalTime)
    {
        if (date.Kind == DateTimeKind.Unspecified)
            return date.ToNSDate();
        
        var dateTime = ignoreLocalTime ? date.ToUniversalTime() : date.ToLocalTime();
        
        return (NSDate)dateTime;
    }
    
    public static DateTime ConvertDate(this NSDate date, bool ignoreLocalTime, DateTimeKind kind)
    {
        var dateTime = (DateTime)date;

        if (kind is DateTimeKind.Unspecified)
            return dateTime;
        
        return kind is DateTimeKind.Local ? dateTime.ToLocalTime() : dateTime.ToUniversalTime();
    }

}