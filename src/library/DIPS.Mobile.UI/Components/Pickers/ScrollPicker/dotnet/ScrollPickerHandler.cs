using DIPS.Mobile.UI.Exceptions;
using Microsoft.Maui.Handlers;

namespace DIPS.Mobile.UI.Components.Pickers.ScrollPicker;

public partial class ScrollPickerHandler : ViewHandler<ScrollPicker, Only_Here_For_UnitTests>
{
    protected override Only_Here_For_UnitTests CreatePlatformView()
    {
        throw new NotImplementedException();
    }
    
    private static partial void MapSelectedIndex(ScrollPickerHandler handler, ScrollPicker scrollPicker)
    {
        
    }
}