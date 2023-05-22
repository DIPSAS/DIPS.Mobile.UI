using DIPS.Mobile.UI.Exceptions;
using Microsoft.Maui.Handlers;

namespace DIPS.Mobile.UI.Components.Pickers.TimePicker;
public partial class TimePickerHandler : ViewHandler<TimePicker, Only_Here_For_UnitTests>
{
    
    protected override Only_Here_For_UnitTests CreatePlatformView()
    {
        throw new NotImplementedException();
    }
    
    private partial void AppendPropertyMapper()
    {
        
    }

    private static partial void MapSelectedTime(TimePickerHandler handler, TimePicker datePicker)
    {
        
    }
    
}