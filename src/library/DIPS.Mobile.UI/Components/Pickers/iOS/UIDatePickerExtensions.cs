using DIPS.Mobile.UI.Components.Pickers.DateTimePickers;
using Foundation;
using UIKit;
using DatePicker = DIPS.Mobile.UI.Components.Pickers.DateTimePickers.DatePicker;

namespace DIPS.Mobile.UI.Components.Pickers.iOS;

public static class UIDatePickerExtensions
{
    public static void SetDate(this UIDatePicker uiDatePicker, System.DateTime dateTime, IDateTimePicker picker)
    {
        bool ignoreLocalTime;
        if (picker is DateAndTimePicker dateAndTimePicker)
        {
            ignoreLocalTime = dateAndTimePicker.IgnoreLocalTime;
        }
        else if (picker is DatePicker datePicker)
        {
            ignoreLocalTime = datePicker.IgnoreLocalTime;
        }
        else
        {
            return;
        }
        
        if (dateTime.Kind == DateTimeKind.Unspecified)
        {
            // Defaults to local time if kind is not set. Setting the kind is required by iOS.
            dateTime = dateTime.ToLocalTime();
        }

        uiDatePicker.TimeZone = ignoreLocalTime ? new NSTimeZone("UTC") : NSTimeZone.LocalTimeZone;
        
        uiDatePicker.SetDate((NSDate)dateTime, true);
    }
}