using DIPS.Mobile.UI.Resources.LocalizedStrings.LocalizedStrings;
using DIPS.Mobile.UI.Resources.Styles;
using DIPS.Mobile.UI.Resources.Styles.Chip;

namespace DIPS.Mobile.UI.Components.Pickers.NullableDateAndTimePicker;

public partial class NullableDateAndTimePicker
{
    protected override void OnHandlerChanged()
    {
        base.OnHandlerChanged();
        
        OnSelectedDateTimeChanged();
    }

    private void OnSelectedDateTimeChanged()
    {
        if (SelectedDateTime is null)
        {
            m_dateChip.Style = Styles.GetChipStyle(ChipStyle.EmptyInput);
            m_timeChip.Style = Styles.GetChipStyle(ChipStyle.EmptyInput);

            m_dateChip.Title = DUILocalizedStrings.ChooseDate;
            m_timeChip.Title = DUILocalizedStrings.ChooseTime;
        }
        else
        {
            m_dateChip.Style = Styles.GetChipStyle(ChipStyle.Input);
            m_timeChip.Style = Styles.GetChipStyle(ChipStyle.Input);
            
            InternalSelectedDateTimeChanged(SelectedDateTime.Value);
        }
    }
}