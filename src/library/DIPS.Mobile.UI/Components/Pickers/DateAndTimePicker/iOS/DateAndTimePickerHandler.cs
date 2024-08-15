using DIPS.Mobile.UI.Components.Pickers.DatePickerShared.iOS;
using DIPS.Mobile.UI.Platforms.iOS;
using Foundation;
using UIKit;

// ReSharper disable once CheckNamespace
namespace DIPS.Mobile.UI.Components.Pickers.DateAndTimePicker;

public partial class DateAndTimePickerHandler : BaseDatePickerHandler
{
    protected override UIDatePicker CreatePlatformView()
    {
        return new UIDatePicker
        {
            Mode = UIDatePickerMode.DateAndTime,
            PreferredDatePickerStyle = UIDatePickerStyle.Compact
        };
    }

    protected override void OnValueChanged(object? sender, EventArgs e)
    {
        if(VirtualView is not DateAndTimePicker dateAndTimePicker)
            return;
        
        dateAndTimePicker.SelectedDateTime = PlatformView.Date.ConvertDate(dateAndTimePicker.IgnoreLocalTime, dateAndTimePicker.SelectedDateTime.Kind);
        dateAndTimePicker.SelectedDateTimeCommand?.Execute(null);
    }

    private static partial void MapMaximumDate(DateAndTimePickerHandler handler, DateAndTimePicker dateAndTimePicker)
    {
        if(dateAndTimePicker.MaximumDate is null)
            return;

        handler.PlatformView.MaximumDate = ((DateTime)dateAndTimePicker.MaximumDate).ConvertAndCastToNsDate(dateAndTimePicker.IgnoreLocalTime);
    }

    private static partial void MapMinimumDate(DateAndTimePickerHandler handler, DateAndTimePicker dateAndTimePicker)
    {
        if(dateAndTimePicker.MinimumDate is null)
            return;
        
        handler.PlatformView.MinimumDate = ((DateTime)dateAndTimePicker.MinimumDate).ConvertAndCastToNsDate(dateAndTimePicker.IgnoreLocalTime);
    }

    private static partial void MapIgnoreLocalTime(DateAndTimePickerHandler handler, DateAndTimePicker dateAndTimePicker)
    {
        if(dateAndTimePicker.SelectedDateTime.Kind == DateTimeKind.Unspecified)
        {
            handler.PlatformView.TimeZone = new NSTimeZone("UTC");
            return;
        }
        
        handler.PlatformView.TimeZone = dateAndTimePicker.IgnoreLocalTime ? new NSTimeZone("UTC") : NSTimeZone.LocalTimeZone;
    }

    private static partial void MapSelectedDate(DateAndTimePickerHandler handler, DateAndTimePicker dateAndTimePicker)
    {
        handler.PlatformView.SetDate(dateAndTimePicker.SelectedDateTime.ConvertAndCastToNsDate(dateAndTimePicker.IgnoreLocalTime), true);
    }
}