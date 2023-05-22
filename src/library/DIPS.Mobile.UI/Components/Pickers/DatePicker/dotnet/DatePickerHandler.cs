using DIPS.Mobile.UI.Exceptions;
using Microsoft.Maui.Handlers;

namespace DIPS.Mobile.UI.Components.Pickers.DatePicker;

public partial class DatePickerHandler : ViewHandler<DatePicker, Only_Here_For_UnitTests>
{
    protected override Only_Here_For_UnitTests CreatePlatformView()
    {
        throw new NotImplementedException();
    }
    
    private partial void AppendPropertyMapper()
    {
        
    }

    private static partial void MapSelectedDate(DatePickerHandler handler, DatePicker datePicker)
    {
        
    }
    
}