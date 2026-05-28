namespace DIPS.Mobile.UI.Components.Pickers.TimePicker;

public partial class TimePickerHandler
{
    public TimePickerHandler() : base(TimePickerPropertyMapper)
    {
    }

#if __IOS__
    
    public static readonly IPropertyMapper<TimePicker, TimePickerHandler> TimePickerPropertyMapper = new PropertyMapper<TimePicker, TimePickerHandler>(BasePropertyMapper)
    {
        [nameof(TimePicker.SelectedTime)] = MapSelectedTime,
        [nameof(TimePicker.MinimumTime)] = MapMinimumTime,
        [nameof(TimePicker.MaximumTime)] = MapMaximumTime
    };
#else
     public static readonly IPropertyMapper<TimePicker, TimePickerHandler> TimePickerPropertyMapper = new PropertyMapper<TimePicker, TimePickerHandler>(ViewMapper)
    {
        [nameof(TimePicker.SelectedTime)] = MapSelectedTime,
        [nameof(TimePicker.MinimumTime)] = MapMinimumTime,
        [nameof(TimePicker.MaximumTime)] = MapMaximumTime
    };
#endif

    private static partial void MapSelectedTime(TimePickerHandler handler, TimePicker timePicker);
    private static partial void MapMinimumTime(TimePickerHandler handler, TimePicker timePicker);
    private static partial void MapMaximumTime(TimePickerHandler handler, TimePicker timePicker);
}