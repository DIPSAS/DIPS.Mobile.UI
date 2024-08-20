using DIPS.Mobile.UI.Components.Pickers.NullableDatePickerShared;
using DIPS.Mobile.UI.Resources.LocalizedStrings.LocalizedStrings;
using DIPS.Mobile.UI.Resources.Styles;
using DIPS.Mobile.UI.Resources.Styles.Chip;

namespace DIPS.Mobile.UI.Components.Pickers.NullableTimePicker;

public partial class NullableTimePicker : TimePicker.TimePicker, INullableDatePicker
{
    protected override void OnHandlerChanged()
    {
        base.OnHandlerChanged();
        
        OnSelectedTimeChanged();
    }

    private void OnSelectedTimeChanged()
    {
        if (SelectedTime is null)
        {
            Style = Styles.GetChipStyle(ChipStyle.EmptyInput);
            Title = DUILocalizedStrings.ChooseTime;
        }
        else
        {
            Style = Styles.GetChipStyle(ChipStyle.Input);
            SetTitle(SelectedTime.Value);
        }
    }
}