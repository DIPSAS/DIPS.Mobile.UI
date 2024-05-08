using DIPS.Mobile.UI.Components.Pickers.DatePickerShared.iOS;
using DIPS.Mobile.UI.Platforms.iOS;
using Foundation;
using UIKit;

// ReSharper disable once CheckNamespace
namespace DIPS.Mobile.UI.Components.Pickers.DateAndTimePicker;

public partial class DateAndTimePickerHandler : BaseDatePickerHandler
{
    protected override DUIDatePicker CreatePlatformView()
    {
        return new DUIDatePicker
        {
            Mode = UIDatePickerMode.DateAndTime,
            PreferredDatePickerStyle = UIDatePickerStyle.Compact
        };
    }

    protected override void OnValueChanged(object? sender, EventArgs e)
    {
        if(VirtualView is not DateAndTimePicker dateAndTimePicker)
            return;
        
        dateAndTimePicker.SelectedDateTime = (DateTime)PlatformView.Date;
        dateAndTimePicker.SelectedDateTimeCommand?.Execute(null);
    }

    private static partial void MapMaximumDate(DateAndTimePickerHandler handler, DateAndTimePicker dateAndTimePicker)
    {
        if(dateAndTimePicker.MaximumDate is null or null)
            return;

        handler.PlatformView.MaximumDate = ((DateTime)dateAndTimePicker.MaximumDate).ConvertDate();
    }

    private static partial void MapMinimumDate(DateAndTimePickerHandler handler, DateAndTimePicker dateAndTimePicker)
    {
        if(dateAndTimePicker.MinimumDate is null or null)
            return;
        
        handler.PlatformView.MinimumDate = ((DateTime)dateAndTimePicker.MinimumDate).ConvertDate();
    }

    private static partial void MapIgnoreLocalTime(DateAndTimePickerHandler handler, DateAndTimePicker dateAndTimePicker)
    {
        handler.PlatformView.TimeZone = dateAndTimePicker.IgnoreLocalTime ? new NSTimeZone("UTC") : NSTimeZone.LocalTimeZone;
    }

    private static partial void MapSelectedDate(DateAndTimePickerHandler handler, DateAndTimePicker dateAndTimePicker)
    {
        handler.PlatformView.SetDate(dateAndTimePicker.SelectedDateTime.ConvertDate(), true);
    }
}