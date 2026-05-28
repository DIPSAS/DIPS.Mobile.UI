using DIPS.Mobile.UI.Components.Pickers.DatePickerShared.iOS;
using Foundation;
using UIKit;

// ReSharper disable once CheckNamespace
namespace DIPS.Mobile.UI.Components.Pickers.TimePicker;

public partial class TimePickerHandler : BaseDatePickerHandler
{
    private bool m_isClampingTime;
    
    protected override UIDatePicker CreatePlatformView()
    {
        return new UIDatePicker
        {
            Mode = UIDatePickerMode.Time,
            PreferredDatePickerStyle = UIDatePickerStyle.Compact
        };
    }

    protected override void OnValueChanged(object? sender, EventArgs e)
    {
        if (m_isClampingTime)
            return;
        
        if (VirtualView is not TimePicker timePicker)
            return;

        var components = NSCalendar.CurrentCalendar.Components(NSCalendarUnit.Hour | NSCalendarUnit.Minute, PlatformView.Date);
        var selectedTime = new TimeSpan((int)components.Hour, (int)components.Minute, 0);

        if (timePicker.MinimumTime is { } min && selectedTime < min)
        {
            ClampPickerTime(min);
            return;
        }

        if (timePicker.MaximumTime is { } max && selectedTime > max)
        {
            ClampPickerTime(max);
            return;
        }

        VirtualView.SetSelectedDateTime(new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, (int)components.Hour, (int)components.Minute, 0));
    }

    private void ClampPickerTime(TimeSpan time)
    {
        m_isClampingTime = true;
        
        var calendar = NSCalendar.CurrentCalendar;
        var components = calendar.Components(NSCalendarUnit.Year | NSCalendarUnit.Month | NSCalendarUnit.Day | NSCalendarUnit.Hour | NSCalendarUnit.Minute, PlatformView.Date);
        components.Hour = time.Hours;
        components.Minute = time.Minutes;
        PlatformView.SetDate(calendar.DateFromComponents(components), true);
        
        m_isClampingTime = false;
        
        VirtualView.SetSelectedDateTime(new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, time.Hours, time.Minutes, 0));
    }

    private static partial void MapSelectedTime(TimePickerHandler handler, TimePicker timePicker)
    {
        var calendar = NSCalendar.CurrentCalendar;
        var components = NSCalendar.CurrentCalendar.Components(NSCalendarUnit.Hour | NSCalendarUnit.Minute, new NSDate());
        components.Hour = timePicker.SelectedTime.Hours;
        components.Minute = timePicker.SelectedTime.Minutes;
        handler.PlatformView.SetDate(calendar.DateFromComponents(components), true);
    }

    private static partial void MapMinimumTime(TimePickerHandler handler, TimePicker timePicker)
    {
        if (timePicker.MinimumTime is not { } minimumTime)
        {
            handler.PlatformView.MinimumDate = null;
            return;
        }

        var calendar = NSCalendar.CurrentCalendar;
        var components = calendar.Components(
            NSCalendarUnit.Year | NSCalendarUnit.Month | NSCalendarUnit.Day | NSCalendarUnit.Hour | NSCalendarUnit.Minute,
            handler.PlatformView.Date);
        components.Hour = minimumTime.Hours;
        components.Minute = minimumTime.Minutes;
        handler.PlatformView.MinimumDate = calendar.DateFromComponents(components);
    }

    private static partial void MapMaximumTime(TimePickerHandler handler, TimePicker timePicker)
    {
        if (timePicker.MaximumTime is not { } maximumTime)
        {
            handler.PlatformView.MaximumDate = null;
            return;
        }

        var calendar = NSCalendar.CurrentCalendar;
        var components = calendar.Components(
            NSCalendarUnit.Year | NSCalendarUnit.Month | NSCalendarUnit.Day | NSCalendarUnit.Hour | NSCalendarUnit.Minute,
            handler.PlatformView.Date);
        components.Hour = maximumTime.Hours;
        components.Minute = maximumTime.Minutes;
        handler.PlatformView.MaximumDate = calendar.DateFromComponents(components);
    }


}
