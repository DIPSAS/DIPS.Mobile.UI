using DIPS.Mobile.UI.Exceptions;
using Microsoft.Maui.Handlers;

// ReSharper disable once CheckNamespace
namespace DIPS.Mobile.UI.Components.Pickers.DateAndTimePicker;

public partial class DateAndTimePickerHandler : ViewHandler<DateAndTimePicker, Only_Here_For_UnitTests>
{
    protected override Only_Here_For_UnitTests CreatePlatformView()
    {
        throw new NotImplementedException();
    }
    
    private static partial void MapIgnoreLocalTime(DateAndTimePickerHandler handler,
        DateAndTimePicker dateAndTimePicker)
    {
    }

    private static partial void MapSelectedDate(DateAndTimePickerHandler handler, DateAndTimePicker dateAndTimePicker)
    {
    }
    
    private static partial void MapMaximumDate(DateAndTimePickerHandler handler, DateAndTimePicker dateAndTimePicker)
    {
    }

    private static partial void MapMinimumDate(DateAndTimePickerHandler handler, DateAndTimePicker dateAndTimePicker)
    {
    }
}