using DIPS.Mobile.UI.Components.Pickers.DatePickerShared.iOS;
using Foundation;
using UIKit;

namespace DIPS.Mobile.UI.Components.Pickers.DatePicker.Inline.iOS;

internal class InlineDatePickerHandler : BaseDatePickerHandler
{
    public InlineDatePickerHandler() : base(PropertyMapper)
    {
    }

    public InlineDatePickerHandler(IPropertyMapper mapper, CommandMapper? commandMapper = null) : base(mapper,
        commandMapper)
    {
    }

    public static readonly IPropertyMapper<DatePicker, InlineDatePickerHandler> PropertyMapper =
        new PropertyMapper<DatePicker, InlineDatePickerHandler>(BasePropertyMapper)
        {
            [nameof(DatePicker.SelectedDate)] = MapSelectedDate,
            [nameof(DatePicker.IgnoreLocalTime)] = MapIgnoreLocalTime,
            [nameof(DatePicker.MaximumDate)] = MapMaximumDate,
            [nameof(DatePicker.MinimumDate)] = MapMinimumDate
        };
    
    protected override UIDatePicker CreatePlatformView()
    {
        return new UIDatePicker { PreferredDatePickerStyle = UIDatePickerStyle.Inline, Mode = UIDatePickerMode.Date };
    }

    private static void MapSelectedDate(InlineDatePickerHandler handler, DatePicker datePicker)
    {
        handler.PlatformView.SetDate(datePicker.SelectedDate.ConvertAndCastToNsDate(datePicker.IgnoreLocalTime), true);
    }

    private static void MapIgnoreLocalTime(InlineDatePickerHandler handler, DatePicker datePicker)
    {
        handler.PlatformView.TimeZone = datePicker.IgnoreLocalTime ? new NSTimeZone("UTC") : NSTimeZone.LocalTimeZone;
    }

    private static void MapMaximumDate(InlineDatePickerHandler handler, DatePicker datePicker)
    {
        if (datePicker.MaximumDate is null)
            return;

        handler.PlatformView.MaximumDate =
            ((DateTime)datePicker.MaximumDate).ConvertAndCastToNsDate(datePicker.IgnoreLocalTime);
    }

    private static void MapMinimumDate(InlineDatePickerHandler handler, DatePicker datePicker)
    {
        if (datePicker.MinimumDate is null)
            return;

        handler.PlatformView.MinimumDate =
            ((DateTime)datePicker.MinimumDate).ConvertAndCastToNsDate(datePicker.IgnoreLocalTime);
    }
}