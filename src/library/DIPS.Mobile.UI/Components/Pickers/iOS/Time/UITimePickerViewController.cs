using DIPS.Mobile.UI.Components.Pickers.DateTimePickers;
using Foundation;
using UIKit;
using TimePicker = DIPS.Mobile.UI.Components.Pickers.DateTimePickers.TimePicker;

namespace DIPS.Mobile.UI.Components.Pickers.iOS.Time;

public class UITimePickerViewController : UIDateTimePickerViewController
{
    private readonly TimePicker m_timePicker;

   
    public UITimePickerViewController(IDateTimePicker dateTimePicker) : base(dateTimePicker)
    {
        m_timePicker = (dateTimePicker as TimePicker)!;
    }

    protected override void SetDateTimePickerMode(UIDatePicker uiDatePicker)
    {
        uiDatePicker.Mode = UIDatePickerMode.Time;
        uiDatePicker.PreferredDatePickerStyle = UIDatePickerStyle.Wheels;
    }

    protected override void SetDateTime(UIDatePicker uiDatePicker)
    {
        var calendar = NSCalendar.CurrentCalendar;
        var components = NSCalendar.CurrentCalendar.Components(NSCalendarUnit.Hour | NSCalendarUnit.Minute, new NSDate());
        components.Hour = m_timePicker.SelectedTime.Hours;
        components.Minute = m_timePicker.SelectedTime.Minutes;
        
        uiDatePicker.SetDate(calendar.DateFromComponents(components), true);
    }

    protected override void OnFinished(UIDatePicker uiDatePicker)
    {
        var components = NSCalendar.CurrentCalendar.Components(NSCalendarUnit.Hour | NSCalendarUnit.Minute, uiDatePicker.Date);
        var timeSpan = new TimeSpan((int)components.Hour, (int)components.Minute, 0);
        m_timePicker.SelectedTime = timeSpan;   
    }
}