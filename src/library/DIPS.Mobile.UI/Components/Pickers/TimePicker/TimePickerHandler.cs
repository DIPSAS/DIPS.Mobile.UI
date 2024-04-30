using Microsoft.Maui.Handlers;

namespace DIPS.Mobile.UI.Components.Pickers.TimePicker;

public partial class TimePickerHandler
{
    public TimePickerHandler() : base(TimePickerPropertyMapper)
    {
    }
    
    public static readonly IPropertyMapper<TimePicker, TimePickerHandler> TimePickerPropertyMapper = new PropertyMapper<TimePicker, TimePickerHandler>(ViewMapper)
    {
        [nameof(TimePicker.SelectedTime)] = MapSelectedTime
    };

    private static partial void MapSelectedTime(TimePickerHandler handler, TimePicker timePicker);
}