using DIPS.Mobile.UI.Components.Pickers.DatePickerShared.iOS;
using Foundation;
using Microsoft.Maui.Platform;
using UIKit;

namespace DIPS.Mobile.UI.Components.Pickers.DatePicker;

public partial class DatePickerHandler : BaseDatePickerHandler
{
    protected override UIDatePicker CreatePlatformView()
    {
        return new UIDatePicker
        {
            Mode = UIDatePickerMode.Date, 
            PreferredDatePickerStyle = UIDatePickerStyle.Compact
        };
    }

    private static partial void MapSelectedDate(DatePickerHandler handler, DatePicker datePicker)
    {
        handler.PlatformView.SetDate(datePicker.SelectedDate.ConvertAndCastToNsDate(datePicker.IgnoreLocalTime), true);
    }
    
    private static partial void MapIgnoreLocalTime(DatePickerHandler handler, DatePicker datePicker)
    {
        handler.PlatformView.TimeZone = datePicker.IgnoreLocalTime ? new NSTimeZone("UTC") : NSTimeZone.LocalTimeZone;
    }
    
    private static partial void MapMaximumDate(DatePickerHandler handler, DatePicker datePicker)
    {
        if (datePicker.MaximumDate is null)
            return;

        handler.PlatformView.MaximumDate = ((DateTime)datePicker.MaximumDate).ConvertAndCastToNsDate(datePicker.IgnoreLocalTime);
    }

    private static partial void MapMinimumDate(DatePickerHandler handler, DatePicker datePicker)
    {
        if (datePicker.MinimumDate is null)
            return;

        handler.PlatformView.MinimumDate = ((DateTime)datePicker.MinimumDate).ConvertAndCastToNsDate(datePicker.IgnoreLocalTime);
    }
}