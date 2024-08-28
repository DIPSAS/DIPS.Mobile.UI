using DIPS.Mobile.UI.Components.Pickers.DateAndTimePicker.iOS;
using DIPS.Mobile.UI.Resources.LocalizedStrings.LocalizedStrings;
using DIPS.Mobile.UI.Resources.Styles;
using DIPS.Mobile.UI.Resources.Styles.Chip;

namespace DIPS.Mobile.UI.Components.Pickers.NullableDateAndTimePicker;

public partial class NullableDateAndTimePicker
{
    private bool m_firstTimeSelectedDateTimeChanged = true;
    
    private partial void InternalOnSelectedDateTimeChanged()
    {
        if (m_firstTimeSelectedDateTimeChanged)
        {
            DateChip.CloseCommand = new Command(OnCloseTapped);
            TimeChip.CloseCommand = new Command(OnCloseTapped);
            
            m_firstTimeSelectedDateTimeChanged = false;
        }
        
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
            
            InternalSelectedDateTimeChanged(SelectedDateTime.Value);
        }
    }

    private void OnCloseTapped()
    {
        SelectedDateTime = null;

        DateAndTimePickerService.Close();
    }
}