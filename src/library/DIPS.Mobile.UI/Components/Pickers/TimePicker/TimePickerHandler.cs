namespace DIPS.Mobile.UI.Components.Pickers.TimePicker;

public partial class TimePickerHandler
{
    public TimePickerHandler() : base(TimePickerPropertyMapper)
    {
    }

#if __IOS__
    
    public static readonly IPropertyMapper<TimePicker, TimePickerHandler> TimePickerPropertyMapper = new PropertyMapper<TimePicker, TimePickerHandler>(BasePropertyMapper)
    {
        [nameof(TimePicker.SelectedTime)] = MapSelectedTime
    };
#else
     public static readonly IPropertyMapper<TimePicker, TimePickerHandler> TimePickerPropertyMapper = new PropertyMapper<TimePicker, TimePickerHandler>(ViewMapper)
    {
        [nameof(TimePicker.SelectedTime)] = MapSelectedTime
    };
#endif

    private static partial void MapSelectedTime(TimePickerHandler handler, TimePicker timePicker);
}