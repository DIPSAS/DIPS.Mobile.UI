using DIPS.Mobile.UI.Components.Pickers.NullableDatePickerShared;

namespace DIPS.Mobile.UI.Components.Pickers.NullableDateAndTimePicker;

public partial class NullableDateAndTimePicker : DateAndTimePicker.DateAndTimePicker, INullableDatePicker
{
    protected override void OnHandlerChanged()
    {
        base.OnHandlerChanged();
        
        InternalOnSelectedDateTimeChanged();
    }
    
    private void OnSelectedDateTimeChanged()
    {
        InternalOnSelectedDateTimeChanged();
    }
    
    private partial void InternalOnSelectedDateTimeChanged();
}