using DIPS.Mobile.UI.Components.Pickers.NullableDatePickerShared;
using DIPS.Mobile.UI.Resources.LocalizedStrings.LocalizedStrings;
using DIPS.Mobile.UI.Resources.Styles;
using DIPS.Mobile.UI.Resources.Styles.Chip;

namespace DIPS.Mobile.UI.Components.Pickers.NullableDatePicker;

public partial class NullableDatePicker : DatePicker.DatePicker, INullableDatePicker
{
    public NullableDatePicker()
    {
        CloseCommand = new Command(OnCloseTapped);
    }

    public override DateTimeKind GetDateTimeKind()
    {
        return SelectedDate?.Kind ?? DateTimeKind.Unspecified;
    }

    private void OnCloseTapped()
    {
        SelectedDate = null;
    }

    protected override void OnHandlerChanged()
    {
        base.OnHandlerChanged();
        
        OnSelectedDateChanged();
    }

    private void OnSelectedDateChanged()
    {
        if (SelectedDate is null)
        {
            IsCloseable = false;
            Style = Styles.GetChipStyle(ChipStyle.EmptyInput);
            Title = DUILocalizedStrings.ChooseDate;
        }
        else
        {
            IsCloseable = true;
            Style = Styles.GetChipStyle(ChipStyle.Input);
            SetTitle(SelectedDate.Value);
        }
    }

    public override void SetSelectedDateTime(DateTime? selectedDate)
    {
        base.SetSelectedDateTime(selectedDate);

        SelectedDate = selectedDate is null ? null : ValidateDateTime(selectedDate.Value);
    }

    internal override DateTime GetDateOnOpen()
    {
        return ValidateDateTime(SelectedDate ?? DateTime.Now);
    }
}