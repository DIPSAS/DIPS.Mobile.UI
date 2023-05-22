using Microsoft.Maui.Handlers;

namespace DIPS.Mobile.UI.Components.Pickers.DateAndTimePicker;

public partial class DateAndTimePickerHandler
{
    public DateAndTimePickerHandler() : base(DateAndTimePickerPropertyMapper)
    {
        AppendPropertyMapper();
    }
    
    public static readonly IPropertyMapper<DateAndTimePicker, DateAndTimePickerHandler> DateAndTimePickerPropertyMapper = new PropertyMapper<DateAndTimePicker, DateAndTimePickerHandler>(ViewHandler.ViewMapper)
    {
        [nameof(DateAndTimePicker.SelectedDateTime)] = MapSelectedDate,
        [nameof(DateAndTimePicker.IgnoreLocalTime)] = MapIgnoreLocalTime,
        [nameof(DateAndTimePicker.MaximumDate)] = MapMaximumDate,
        [nameof(DateAndTimePicker.MinimumDate)] = MapMinimumDate
    };

    private partial void AppendPropertyMapper();

    private static partial void MapMinimumDate(DateAndTimePickerHandler handler, DateAndTimePicker dateAndTimePicker);
    private static partial void MapMaximumDate(DateAndTimePickerHandler handler, DateAndTimePicker dateAndTimePicker);
    private static partial void MapIgnoreLocalTime(DateAndTimePickerHandler handler, DateAndTimePicker dateAndTimePicker);
    private static partial void MapSelectedDate(DateAndTimePickerHandler handler, DateAndTimePicker dateAndTimePicker);
}