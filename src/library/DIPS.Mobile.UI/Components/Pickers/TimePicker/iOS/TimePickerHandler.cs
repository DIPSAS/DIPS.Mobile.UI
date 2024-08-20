using DIPS.Mobile.UI.Components.Pickers.DatePickerShared.iOS;
using Foundation;
using UIKit;

// ReSharper disable once CheckNamespace
namespace DIPS.Mobile.UI.Components.Pickers.TimePicker;

public partial class TimePickerHandler : BaseDatePickerHandler
{
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
        if(VirtualView is not TimePicker)
            return;
        
        var components = NSCalendar.CurrentCalendar.Components(NSCalendarUnit.Hour | NSCalendarUnit.Minute, PlatformView.Date);
        VirtualView.SetSelectedDateTime(new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, (int)components.Hour, (int)components.Minute, 0));
    }

    private static partial void MapSelectedTime(TimePickerHandler handler, TimePicker timePicker)
    {
        var calendar = NSCalendar.CurrentCalendar;
        var components = NSCalendar.CurrentCalendar.Components(NSCalendarUnit.Hour | NSCalendarUnit.Minute, new NSDate());
        components.Hour = timePicker.SelectedTime.Hours;
        components.Minute = timePicker.SelectedTime.Minutes;
        handler.PlatformView.SetDate(calendar.DateFromComponents(components), true);
    }

    
}