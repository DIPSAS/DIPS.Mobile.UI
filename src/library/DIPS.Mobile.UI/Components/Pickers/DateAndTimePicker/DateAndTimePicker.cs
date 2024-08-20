using HorizontalStackLayout = DIPS.Mobile.UI.Components.Lists.HorizontalStackLayout;
using IDatePicker = DIPS.Mobile.UI.Components.Pickers.DatePickerShared.IDatePicker;

namespace DIPS.Mobile.UI.Components.Pickers.DateAndTimePicker;

public partial class DateAndTimePicker : HorizontalStackLayout, IDatePicker
{
    private void OnSelectedDateTimeChanged()
    {    
        InternalSelectedDateTimeChanged(SelectedDateTime);
    }

    protected partial void InternalSelectedDateTimeChanged(DateTime selectedDateTime);
    
}