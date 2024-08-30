using DIPS.Mobile.UI.Components.Pickers.DateAndTimePicker;
using DIPS.Mobile.UI.Components.Pickers.NullableDatePickerShared;
using DIPS.Mobile.UI.Resources.LocalizedStrings.LocalizedStrings;
using DIPS.Mobile.UI.Resources.Styles;
using DIPS.Mobile.UI.Resources.Styles.Chip;

namespace DIPS.Mobile.UI.Components.Pickers.NullableDateAndTimePicker;

public partial class NullableDateAndTimePicker : DateAndTimePicker.DateAndTimePicker, INullableDatePicker
{
    public NullableDateAndTimePicker()
    {
        DateChip.CloseCommand = new Command(OnCloseTapped);
        TimeChip.CloseCommand = new Command(OnCloseTapped);
    }
    
    protected override void OnHandlerChanged()
    {
        base.OnHandlerChanged();
        
        OnSelectedDateTimeChanged();
    }
    
    private void OnSelectedDateTimeChanged()
    {
        if (SelectedDateTime is null)
        {
            DateChip.IsCloseable = false;
            TimeChip.IsCloseable = false;
            
            DateChip.Style = Styles.GetChipStyle(ChipStyle.EmptyInput);
            TimeChip.Style = Styles.GetChipStyle(ChipStyle.EmptyInput);

            DateChip.Title = DUILocalizedStrings.ChooseDate;
            TimeChip.Title = DUILocalizedStrings.ChooseTime;
        }
        else
        {
            DateChip.IsCloseable = true;
            TimeChip.IsCloseable = true;
            
            DateChip.Style = Styles.GetChipStyle(ChipStyle.Input);
            TimeChip.Style = Styles.GetChipStyle(ChipStyle.Input);
            
            OnSelectedDateTimeChanged(SelectedDateTime.Value);
        }
    }

    public override void SetSelectedDateTime(DateTime? selectedDate)
    {
        base.SetSelectedDateTime(selectedDate);

        SelectedDateTime = selectedDate is null ? null : ValidateDateTime(selectedDate.Value);
    }

    public override DateTime GetDateOnOpen()
    {
        return ValidateDateTime(SelectedDateTime ?? DateTime.Now);
    }
    
    private void OnCloseTapped()
    {
        SelectedDateTime = null;

        DateAndTimePickerService.Close();
    }

}