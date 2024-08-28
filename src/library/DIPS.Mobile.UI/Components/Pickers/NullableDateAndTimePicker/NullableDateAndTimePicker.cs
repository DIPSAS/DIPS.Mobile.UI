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

    public override void SetSelectedDateTime(DateTime? selectedDate)
    {
        base.SetSelectedDateTime(selectedDate);

        SelectedDateTime = selectedDate;
    }

    private partial void InternalOnSelectedDateTimeChanged();

    public override DateTimeKind GetKind()
    {
        return SelectedDateTime?.Kind ?? DateTimeKind.Unspecified;
    }

#if __IOS__
    public override DateTime SetSelectedDateTimeOnPopoverOpen()
    {
        SelectedDateTime ??= DateTime.Now;

        return SelectedDateTime.Value;
    }
#endif
}