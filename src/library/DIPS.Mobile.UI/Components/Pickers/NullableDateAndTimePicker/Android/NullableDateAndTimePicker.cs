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
            DatePicker.Style = Styles.GetChipStyle(ChipStyle.EmptyInput);
            TimePicker.Style = Styles.GetChipStyle(ChipStyle.EmptyInput);

            DatePicker.Title = DUILocalizedStrings.ChooseDate;
            TimePicker.Title = DUILocalizedStrings.ChooseTime;
        }
        else
        {
            DatePicker.Style = Styles.GetChipStyle(ChipStyle.Input);
            TimePicker.Style = Styles.GetChipStyle(ChipStyle.Input);
            
            SetTitle(SelectedDateTime.Value);
        }
    }
}