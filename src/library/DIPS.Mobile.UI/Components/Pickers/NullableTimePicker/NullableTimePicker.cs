using DIPS.Mobile.UI.Components.Pickers.NullableDatePickerShared;
using DIPS.Mobile.UI.Resources.LocalizedStrings.LocalizedStrings;
using DIPS.Mobile.UI.Resources.Styles;
using DIPS.Mobile.UI.Resources.Styles.Chip;

namespace DIPS.Mobile.UI.Components.Pickers.NullableTimePicker;

public partial class NullableTimePicker : TimePicker.TimePicker, INullableDatePicker
{
    public NullableTimePicker()
    {
        CloseCommand = new Command(OnCloseTapped);
    }

    private void OnCloseTapped()
    {
        SelectedTime = null;
    }

    protected override void OnHandlerChanging(HandlerChangingEventArgs args)
    {
        base.OnHandlerChanging(args);
        
        if(args.NewHandler is null)
            return;
        
        OnSelectedTimeChanged();
    }

    private void OnSelectedTimeChanged()
    {
        if (SelectedTime is null)
        {
            IsCloseable = false;
            Style = Styles.GetChipStyle(ChipStyle.EmptyInput);
            Title = DUILocalizedStrings.ChooseTime;
        }
        else
        {
            IsCloseable = true;
            Style = Styles.GetChipStyle(ChipStyle.Input);
            SetTitle(SelectedTime.Value);
        }
    }

    public override void SetSelectedDateTime(DateTime? selectedDate)
    {
        base.SetSelectedDateTime(selectedDate);

        SelectedTime = selectedDate?.TimeOfDay;
    }

    public override TimeSpan GetTimeOnOpen()
    {
        return SelectedTime ?? DateTime.Now.TimeOfDay;
    }
}