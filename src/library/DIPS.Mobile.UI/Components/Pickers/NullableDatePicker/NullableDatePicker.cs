using DIPS.Mobile.UI.Components.Pickers.NullableDatePickerShared;
using DIPS.Mobile.UI.Resources.LocalizedStrings.LocalizedStrings;
using DIPS.Mobile.UI.Resources.Styles;
using DIPS.Mobile.UI.Resources.Styles.Chip;

namespace DIPS.Mobile.UI.Components.Pickers.NullableDatePicker;

public partial class NullableDatePicker : DatePicker.DatePicker, INullableDatePicker
{
    protected override void OnHandlerChanged()
    {
        base.OnHandlerChanged();
        
        OnSelectedDateChanged();
    }

    private void OnSelectedDateChanged()
    {
        if (SelectedDate is null)
        {
            Style = Styles.GetChipStyle(ChipStyle.EmptyInput);
            Title = DUILocalizedStrings.ChooseDate;
        }
        else
        {
            Style = Styles.GetChipStyle(ChipStyle.Input);
            SetTitle(SelectedDate.Value);
        }
    }
}