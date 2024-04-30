using DIPS.Mobile.UI.Components.Pickers.DatePickerShared.iOS;
using Foundation;
using UIKit;

// ReSharper disable once CheckNamespace
namespace DIPS.Mobile.UI.Components.Pickers.TimePicker;

public partial class TimePickerHandler : BaseDatePickerHandler
{
    private static partial void MapSelectedTime(TimePickerHandler handler, TimePicker timePicker)
    {
        handler.OnDateOrTimeChanged();
        
        if(!timePicker.SelectedTime.HasValue)
            return;
        
        var calendar = NSCalendar.CurrentCalendar;
        var components = NSCalendar.CurrentCalendar.Components(NSCalendarUnit.Hour | NSCalendarUnit.Minute, new NSDate());
        components.Hour = timePicker.SelectedTime.Value.Hours;
        components.Minute = timePicker.SelectedTime.Value.Minutes;
        handler.m_nativeDatePicker.SetDate(calendar.DateFromComponents(components), true);
    }

    protected override UIDatePickerMode GetMode() => UIDatePickerMode.Time;
    protected override void OnDateSelected()
    {
        if(VirtualView is not TimePicker timePicker)
            return;
        
        var components = NSCalendar.CurrentCalendar.Components(NSCalendarUnit.Hour | NSCalendarUnit.Minute, m_nativeDatePicker.Date);
        var timeSpan = new TimeSpan((int)components.Hour, (int)components.Minute, 0);
        timePicker.SelectedTime = timeSpan;
        timePicker.SelectedTimeCommand?.Execute(null);
    }
}