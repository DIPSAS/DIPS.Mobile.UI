using Foundation;

namespace DIPS.Mobile.UI.Components.Pickers.Platforms.iOS;

public static class DateTimeToNSDateExtensions
{
    public static NSDate ConvertDate(this DateTime date)
    {
        if (date.Kind == DateTimeKind.Unspecified)
        {
            date = DateTime.SpecifyKind(date, DateTimeKind.Local);
        }

        return (NSDate)date;
    } 

}