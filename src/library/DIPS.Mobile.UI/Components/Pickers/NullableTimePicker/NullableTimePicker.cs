using DIPS.Mobile.UI.Components.Pickers.NullableDatePickerShared;

namespace DIPS.Mobile.UI.Components.Pickers.NullableTimePicker;

public partial class NullableTimePicker : BaseNullableDatePicker
{
    private TimePicker.TimePicker m_timePicker;

    protected override bool IsDateOrTimeNull()
    {
        return SelectedTime is null;
    }

    protected override View CreateDateOrTimePicker()
    {
        m_timePicker = new TimePicker.TimePicker
        {
            SelectedTimeCommand = new Command(OnTimeChanged),
            Opacity = 0.25,
            SelectedTime = SelectedTime ?? DateTime.Now.TimeOfDay,
            IsEnabled = false
        };

        return m_timePicker;
    }

    private void OnTimeChanged()
    {
        SelectedTime = m_timePicker.SelectedTime;
        SelectedTimeCommand?.Execute(null);
    }

    protected override void OnSwitchToggled(object? sender, ToggledEventArgs e)
    {
        base.OnSwitchToggled(sender, e);
        
        if (e.Value)
        {
            OnTimeChanged();
        }
        else
        {
            SelectedTime = null;
            SelectedTimeCommand?.Execute(null);
        }
    }
}