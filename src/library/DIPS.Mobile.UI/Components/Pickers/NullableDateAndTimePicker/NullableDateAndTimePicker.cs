using DIPS.Mobile.UI.Components.Pickers.NullableDatePickerShared;

namespace DIPS.Mobile.UI.Components.Pickers.NullableDateAndTimePicker;

public partial class NullableDateAndTimePicker : BaseNullableDatePicker
{
    private DateAndTimePicker.DateAndTimePicker? m_dateAndTimePicker;
   
    protected override View CreateDateOrTimePicker()
    {
        m_dateAndTimePicker = new DateAndTimePicker.DateAndTimePicker
        {
            SelectedDateTimeCommand = new Command(OnInternalDateChanged),
            SelectedDateTime = SelectedDateTime ?? DateTime.Now
        };

        m_dateAndTimePicker.SetBinding(DatePicker.DatePicker.MinimumDateProperty, new Binding(nameof(MinimumDate), BindingMode.OneWay, source: this));
        m_dateAndTimePicker.SetBinding(DatePicker.DatePicker.MaximumDateProperty, new Binding(nameof(MaximumDate), BindingMode.OneWay, source: this));
        m_dateAndTimePicker.SetBinding(DatePicker.DatePicker.IgnoreLocalTimeProperty, new Binding(nameof(IgnoreLocalTime), BindingMode.OneWay, source: this));
        return m_dateAndTimePicker;
    }

    protected override bool IsDateOrTimeNull()
    {
        return SelectedDateTime is null;
    }
    
    private void OnInternalDateChanged()
    {
        SelectedDateTime = m_dateAndTimePicker?.SelectedDateTime;
    }
    
    protected override void OnSwitchToggled(object? sender, ToggledEventArgs e)
    {
        base.OnSwitchToggled(sender, e);
        
        if (e.Value)
        {
            OnInternalDateChanged();
        }
        else
        {
            SelectedDateTime = null;
        }
    }

    private void OnSelectedDateTimeChanged()
    {
        DateEnabledSwitch.IsToggled = SelectedDateTime.HasValue;
        SelectedDateTimeCommand?.Execute(null);
    }
}