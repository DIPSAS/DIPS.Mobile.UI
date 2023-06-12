using Microsoft.Maui.Handlers;

namespace DIPS.Mobile.UI.Components.Pickers.DatePicker;

public partial class DatePickerHandler
{
    public DatePickerHandler() : base(DatePickerPropertyMapper)
    {
        AppendPropertyMapper();
    }
    
    public static readonly IPropertyMapper<DatePicker, DatePickerHandler> DatePickerPropertyMapper = new PropertyMapper<DatePicker, DatePickerHandler>(ViewHandler.ViewMapper)
    {
        [nameof(DatePicker.SelectedDate)] = MapSelectedDate
    };

    private partial void AppendPropertyMapper();
    public static partial void MapSelectedDate(DatePickerHandler handler, DatePicker datePicker);
}