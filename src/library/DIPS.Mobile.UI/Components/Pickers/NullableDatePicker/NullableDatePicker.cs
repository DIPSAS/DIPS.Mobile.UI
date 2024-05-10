using DIPS.Mobile.UI.Components.Pickers.NullableDatePickerShared;

namespace DIPS.Mobile.UI.Components.Pickers.NullableDatePicker;

public partial class NullableDatePicker : BaseNullableDatePicker
{
    private DatePicker.DatePicker? m_datePicker;
    
    protected override View CreateDateOrTimePicker()
    {
        m_datePicker = new DatePicker.DatePicker
        {
            SelectedDateCommand = new Command(OnInternalDateChanged),
            SelectedDate = SelectedDate ?? DateTime.Now.Date
        };

        m_datePicker.SetBinding(DatePicker.DatePicker.MinimumDateProperty, new Binding(nameof(MinimumDate), BindingMode.OneWay, source: this));
        m_datePicker.SetBinding(DatePicker.DatePicker.MaximumDateProperty, new Binding(nameof(MaximumDate), BindingMode.OneWay, source: this));
        m_datePicker.SetBinding(DatePicker.DatePicker.IgnoreLocalTimeProperty, new Binding(nameof(IgnoreLocalTime), BindingMode.OneWay, source: this));
        m_datePicker.SetBinding(HorizontalOptionsProperty, new Binding(nameof(HorizontalOptions), BindingMode.OneWay, source: this));
        return m_datePicker;
    }

    protected override bool IsDateOrTimeNull()
    {
        return SelectedDate is null;
    }
    
    private void OnInternalDateChanged()
    {
        SelectedDate = m_datePicker?.SelectedDate;
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
            SelectedDate = null;
        }
        
        SelectedDateCommand?.Execute(null);
    }

    private void OnSelectedDateChanged()
    {
        if(m_datePicker is not null)
            m_datePicker.SelectedDate = SelectedDate ?? m_datePicker.SelectedDate;
        
        DateEnabledSwitch.IsToggled = SelectedDate.HasValue;
        SelectedDateCommand?.Execute(null);
    }
}