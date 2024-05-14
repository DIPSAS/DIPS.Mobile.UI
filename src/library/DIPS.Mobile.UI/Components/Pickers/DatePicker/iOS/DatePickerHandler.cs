using DIPS.Mobile.UI.Components.Pickers.DatePickerShared.iOS;
using Foundation;
using UIKit;

namespace DIPS.Mobile.UI.Components.Pickers.DatePicker;

public partial class DatePickerHandler : BaseDatePickerHandler
{
    protected override DUIDatePicker CreatePlatformView()
    {
        return new DUIDatePicker
        {
            Mode = UIDatePickerMode.Date, 
            PreferredDatePickerStyle = UIDatePickerStyle.Compact
        };
    }

    protected override void OnValueChanged(object? sender, EventArgs e)
    {
        if (VirtualView is not DatePicker datePicker)
            return;
        
        var timeZone = PlatformView.TimeZone ?? NSTimeZone.LocalTimeZone;
        if (DateTime.TryParse(
                new NSDateFormatter {DateFormat = "yyyy-MM-dd", TimeZone = timeZone}.StringFor(
                    PlatformView.Date),
                out var selectedDate))
        {
            datePicker.SelectedDate = selectedDate;
        }
        datePicker.SelectedDateCommand?.Execute(null);
    }

    private static partial void MapSelectedDate(DatePickerHandler handler, DatePicker datePicker)
    {
        handler.PlatformView.SetDate(datePicker.SelectedDate.ConvertDate(datePicker.IgnoreLocalTime), true);
    }
    
    private static partial void MapIgnoreLocalTime(DatePickerHandler handler, DatePicker datePicker)
    {
        handler.PlatformView.TimeZone = datePicker.IgnoreLocalTime ? new NSTimeZone("UTC") : NSTimeZone.LocalTimeZone;
    }
    
    private static partial void MapMaximumDate(DatePickerHandler handler, DatePicker datePicker)
    {
        if (datePicker.MaximumDate is null or null)
            return;

        handler.PlatformView.MaximumDate = ((DateTime)datePicker.MaximumDate).ConvertDate(datePicker.IgnoreLocalTime);
    }

    private static partial void MapMinimumDate(DatePickerHandler handler, DatePicker datePicker)
    {
        if (datePicker.MinimumDate is null or null)
            return;

        handler.PlatformView.MinimumDate = ((DateTime)datePicker.MinimumDate).ConvertDate(datePicker.IgnoreLocalTime);
    }
}