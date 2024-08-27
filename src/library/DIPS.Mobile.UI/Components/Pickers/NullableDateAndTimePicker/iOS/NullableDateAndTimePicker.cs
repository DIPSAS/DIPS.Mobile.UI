using DIPS.Mobile.UI.Resources.LocalizedStrings.LocalizedStrings;
using DIPS.Mobile.UI.Resources.Styles;
using DIPS.Mobile.UI.Resources.Styles.Chip;

namespace DIPS.Mobile.UI.Components.Pickers.NullableDateAndTimePicker;

public partial class NullableDateAndTimePicker
{
    private partial void InternalOnSelectedDateTimeChanged()
    {
        if (SelectedDateTime is null)
        {
            DateChip.Style = Styles.GetChipStyle(ChipStyle.EmptyInput);
            TimeChip.Style = Styles.GetChipStyle(ChipStyle.EmptyInput);

            DateChip.Title = DUILocalizedStrings.ChooseDate;
            TimeChip.Title = DUILocalizedStrings.ChooseTime;
        }
        else
        {
            DateChip.Style = Styles.GetChipStyle(ChipStyle.Input);
            TimeChip.Style = Styles.GetChipStyle(ChipStyle.Input);
            
            InternalSelectedDateTimeChanged(SelectedDateTime.Value);
        }
    }
}