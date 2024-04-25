using Microsoft.Maui.Handlers;

namespace DIPS.Mobile.UI.Components.Pickers.TimePicker;

public partial class TimePickerHandler
{
    public TimePickerHandler() : base(TimePickerPropertyMapper)
    {
        AppendPropertyMapper();
    }
    
    public static readonly IPropertyMapper<TimePicker, TimePickerHandler> TimePickerPropertyMapper = new PropertyMapper<Pickers.TimePicker.TimePicker, TimePickerHandler>(ViewHandler.ViewMapper)
    {
        [nameof(TimePicker.SelectedTime)] = MapSelectedTime
    };

    private partial void AppendPropertyMapper();
    private static partial void MapSelectedTime(TimePickerHandler handler, TimePicker timePicker);
}