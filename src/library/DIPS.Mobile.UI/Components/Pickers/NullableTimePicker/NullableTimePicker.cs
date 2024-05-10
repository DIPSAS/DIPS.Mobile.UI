using DIPS.Mobile.UI.Components.Pickers.NullableDatePickerShared;

namespace DIPS.Mobile.UI.Components.Pickers.NullableTimePicker;

public partial class NullableTimePicker : BaseNullableDatePicker
{
    private TimePicker.TimePicker? m_timePicker;
    
    protected override bool IsDateOrTimeNull()
    {
        return SelectedTime is null;
    }

    protected override View CreateDateOrTimePicker()
    {
        m_timePicker = new TimePicker.TimePicker
        {
            SelectedTimeCommand = new Command(OnInternalTimeChanged),
            SelectedTime = SelectedTime ?? DateTime.Now.TimeOfDay
        };

        return m_timePicker;
    }

    private void OnInternalTimeChanged()
    {
        SelectedTime = m_timePicker?.SelectedTime;
    }

    protected override void OnSwitchToggled(object? sender, ToggledEventArgs e)
    {
        base.OnSwitchToggled(sender, e);
        
        if (e.Value)
        {
            OnInternalTimeChanged();
        }
        else
        {
            SelectedTime = null;
        }
    }

    private void OnSelectedTimeChanged()
    {
        DateEnabledSwitch.IsToggled = SelectedTime.HasValue;
        SelectedTimeCommand?.Execute(null);
    }
}