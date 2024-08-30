using DIPS.Mobile.UI.Components.Pickers.DatePickerShared.iOS;
using Foundation;
using UIKit;

namespace DIPS.Mobile.UI.Components.Pickers.DatePicker.Inline.iOS;

public class InlineDateAndTimePickerHandler() : BaseDatePickerHandler(BasePropertyMapper)
{
    public static readonly IPropertyMapper<DateAndTimePicker.DateAndTimePicker, InlineDateAndTimePickerHandler> DateAndTimePickerPropertyMapper = new PropertyMapper<DateAndTimePicker.DateAndTimePicker, InlineDateAndTimePickerHandler>(BasePropertyMapper)
    {
        [nameof(DateAndTimePicker.DateAndTimePicker.SelectedDateTime)] = MapSelectedDate,
        [nameof(DateAndTimePicker.DateAndTimePicker.IgnoreLocalTime)] = MapIgnoreLocalTime,
        [nameof(DateAndTimePicker.DateAndTimePicker.MaximumDate)] = MapMaximumDate,
        [nameof(DateAndTimePicker.DateAndTimePicker.MinimumDate)] = MapMinimumDate
    };

    private static void MapMinimumDate(InlineDateAndTimePickerHandler handler, DateAndTimePicker.DateAndTimePicker dateAndTimePicker)
    {
        if (dateAndTimePicker.MinimumDate is null)
            return;

        handler.PlatformView.MinimumDate =
            ((DateTime)dateAndTimePicker.MinimumDate).ConvertAndCastToNsDate(dateAndTimePicker.IgnoreLocalTime);
    }

    private static void MapMaximumDate(InlineDateAndTimePickerHandler handler, DateAndTimePicker.DateAndTimePicker dateAndTimePicker)
    {
        if (dateAndTimePicker.MaximumDate is null)
            return;

        handler.PlatformView.MaximumDate =
            ((DateTime)dateAndTimePicker.MaximumDate).ConvertAndCastToNsDate(dateAndTimePicker.IgnoreLocalTime);
    }
    
    private static void MapIgnoreLocalTime(InlineDateAndTimePickerHandler handler, DateAndTimePicker.DateAndTimePicker dateAndTimePicker)
    {
        handler.PlatformView.TimeZone = dateAndTimePicker.IgnoreLocalTime ? new NSTimeZone("UTC") : NSTimeZone.LocalTimeZone;
    }

    private static void MapSelectedDate(InlineDateAndTimePickerHandler handler,
        DateAndTimePicker.DateAndTimePicker dateAndTimePicker)
    {
        handler.PlatformView.SetDate(dateAndTimePicker.SelectedDateTime.ConvertAndCastToNsDate(dateAndTimePicker.IgnoreLocalTime), true);
    }
    
    protected override UIDatePicker CreatePlatformView()
    {
        return new UIDatePicker
        {
            Mode = UIDatePickerMode.DateAndTime, PreferredDatePickerStyle = UIDatePickerStyle.Inline
        };
    }
}