using DIPS.Mobile.UI.Components.Pickers.DatePickerShared.iOS;
using Foundation;
using UIKit;

namespace DIPS.Mobile.UI.Components.Pickers.DatePicker;

public partial class DatePickerHandler : BaseDatePickerHandler
{
    private static partial void MapSelectedDate(DatePickerHandler handler, DatePicker datePicker)
    {
        handler.OnDateOrTimeChanged();
        
        if (!datePicker.SelectedDate.HasValue)
            return;
        
        handler.m_nativeDatePicker.SetDate(datePicker.SelectedDate.Value.ConvertDate(), true);
    }
    
    private static partial void MapIgnoreLocalTime(DatePickerHandler handler, DatePicker datePicker)
    {
        handler.m_nativeDatePicker.TimeZone = datePicker.IgnoreLocalTime ? new NSTimeZone("UTC") : NSTimeZone.LocalTimeZone;
    }
    
    private static partial void MapMaximumDate(DatePickerHandler handler, DatePicker datePicker)
    {
        if (datePicker.MaximumDate is null or null)
            return;

        handler.m_nativeDatePicker.MaximumDate = ((DateTime)datePicker.MaximumDate).ConvertDate();
    }

    private static partial void MapMinimumDate(DatePickerHandler handler, DatePicker datePicker)
    {
        if (datePicker.MinimumDate is null or null)
            return;

        handler.m_nativeDatePicker.MinimumDate = ((DateTime)datePicker.MinimumDate).ConvertDate();
    }

    protected override UIDatePickerMode GetMode() => UIDatePickerMode.Date;
    protected override void OnDateSelected()
    {
        if(VirtualView is not DatePicker datePicker)
            return;
        
        var timeZone = m_nativeDatePicker.TimeZone ?? NSTimeZone.LocalTimeZone;
        if (DateTime.TryParse(
                new NSDateFormatter { DateFormat = "yyyy-MM-dd", TimeZone = timeZone }.StringFor(
                    m_nativeDatePicker.Date),
                out var selectedDate))
        {
            datePicker.SelectedDate = selectedDate;
        }
        datePicker.SelectedDateCommand?.Execute(null);
    }
}