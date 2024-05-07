using DIPS.Mobile.UI.Components.Pickers.NullableDatePickerShared;

namespace DIPS.Mobile.UI.Components.Pickers.NullableDatePicker;

public partial class NullableDatePicker : BaseNullableDatePicker
{
#nullable disable
    private DatePicker.DatePicker m_datePicker;
#nullable enable
    
    protected override View CreateDateOrTimePicker()
    {
        m_datePicker = new DatePicker.DatePicker
        {
            SelectedDateCommand = new Command(OnDateChanged),
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
    
    private void OnDateChanged()
    {
        SelectedDate = m_datePicker.SelectedDate;
        SelectedDateCommand?.Execute(null);
    }

    protected override void OnSwitchToggled(object? sender, ToggledEventArgs e)
    {
        base.OnSwitchToggled(sender, e);
        
        if (e.Value)
        {
            OnDateChanged();
        }
        else
        {
            SelectedDate = null;
            SelectedDateCommand?.Execute(null);
        }
    }
}