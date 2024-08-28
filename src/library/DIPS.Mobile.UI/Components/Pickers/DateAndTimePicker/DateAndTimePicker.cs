using DIPS.Mobile.UI.Components.Pickers.DatePickerShared;
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

    public event Action<DateTime?>? SelectedDateTimeChanged;
    public DatePickerMode Mode => DatePickerMode.DateAndTime;
    public virtual void SetSelectedDateTime(DateTime? selectedDate)
    {
        if (selectedDate.HasValue)
        {
            SelectedDateTime = selectedDate.Value;
            
            OnSelectedDateTimeChanged();
        }
        
        SelectedDateTimeCommand?.Execute(selectedDate);
        SelectedDateTimeChanged?.Invoke(selectedDate);
    }

    public virtual DateTimeKind GetKind()
    {
        return SelectedDateTime.Kind;        
    }

#if __IOS__
    public virtual DateTime SetSelectedDateTimeOnPopoverOpen()
    {
        return SelectedDateTime;
    }
#endif
}