using Microsoft.Maui.Handlers;

namespace DIPS.Mobile.UI.Components.Pickers.DatePicker;

public abstract partial class BaseDatePickerHandler
{
    public BaseDatePickerHandler() : base(DatePickerPropertyMapper)
    {
        AppendPropertyMapper();
    }
    
    public static readonly IPropertyMapper<DatePicker, BaseDatePickerHandler> DatePickerPropertyMapper = new PropertyMapper<DatePicker, BaseDatePickerHandler>(ViewHandler.ViewMapper)
    {
        [nameof(DatePicker.SelectedDate)] = MapSelectedDate
    };

    private partial void AppendPropertyMapper();
    public static partial void MapSelectedDate(BaseDatePickerHandler handler, DatePicker datePicker);
}