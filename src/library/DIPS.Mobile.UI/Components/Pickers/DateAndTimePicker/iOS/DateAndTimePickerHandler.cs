using DIPS.Mobile.UI.Components.Pickers.DatePickerShared.iOS;
using Foundation;
using UIKit;

// ReSharper disable once CheckNamespace
namespace DIPS.Mobile.UI.Components.Pickers.DateAndTimePicker;

public partial class DateAndTimePickerHandler : BaseDatePickerHandler
{
    private static partial void MapMaximumDate(DateAndTimePickerHandler handler, DateAndTimePicker dateAndTimePicker)
    {
        if(dateAndTimePicker.MaximumDate is null or null)
            return;

        handler.m_nativeDatePicker.MaximumDate = ((DateTime)dateAndTimePicker.MaximumDate).ConvertDate();
    }

    private static partial void MapMinimumDate(DateAndTimePickerHandler handler, DateAndTimePicker dateAndTimePicker)
    {
        if(dateAndTimePicker.MinimumDate is null or null)
            return;
        
        handler.m_nativeDatePicker.MinimumDate = ((DateTime)dateAndTimePicker.MinimumDate).ConvertDate();
    }

    private static partial void MapIgnoreLocalTime(DateAndTimePickerHandler handler, DateAndTimePicker dateAndTimePicker)
    {
        handler.m_nativeDatePicker.TimeZone = dateAndTimePicker.IgnoreLocalTime ? new NSTimeZone("UTC") : NSTimeZone.LocalTimeZone;
    }

    private static partial void MapSelectedDate(DateAndTimePickerHandler handler, DateAndTimePicker dateAndTimePicker)
    {
        handler.OnDateOrTimeChanged();
        
        if(!dateAndTimePicker.SelectedDateTime.HasValue)
            return;
        
        handler.m_nativeDatePicker.SetDate(dateAndTimePicker.SelectedDateTime.Value.ConvertDate(), true);
    }
    
    protected override UIDatePickerMode GetMode() => UIDatePickerMode.DateAndTime;
    protected override void OnDateSelected()
    {
        if (VirtualView is not DateAndTimePicker dateAndTimePicker)
            return;
        
        dateAndTimePicker.SelectedDateTime = (DateTime)m_nativeDatePicker.Date;
        dateAndTimePicker.SelectedDateTimeCommand?.Execute(null);
    }
}