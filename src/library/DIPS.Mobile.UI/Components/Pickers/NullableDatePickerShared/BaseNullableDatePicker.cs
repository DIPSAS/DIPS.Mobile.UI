namespace DIPS.Mobile.UI.Components.Pickers.NullableDatePickerShared;

public abstract class BaseNullableDatePicker : HorizontalStackLayout
{
    private readonly Switch m_dateEnabledSwitch;
    
#nullable disable
    private View m_dateOrTimePicker;
#nullable enable

    protected BaseNullableDatePicker()
    {
        Spacing = Sizes.GetSize(SizeName.size_3);

        m_dateEnabledSwitch = new Switch { VerticalOptions = LayoutOptions.Center };
        m_dateEnabledSwitch.Toggled += OnSwitchToggled;
        
        Add(m_dateEnabledSwitch);
    }

    protected override void OnHandlerChanged()
    {
        base.OnHandlerChanged();
        
        m_dateOrTimePicker = CreateDateOrTimePicker();
        m_dateOrTimePicker.IsVisible = false;
        m_dateOrTimePicker.VerticalOptions = LayoutOptions.Center;
#if __IOS__
        m_dateOrTimePicker.SetBinding(HorizontalOptionsProperty, new Binding(nameof(HorizontalOptions), BindingMode.OneWay, source: this));
#endif
        
        Insert(0, m_dateOrTimePicker);
        var size = m_dateOrTimePicker.Measure(int.MaxValue, int.MaxValue);
        MinimumHeightRequest = size.Request.Height;
        
        if (!IsDateOrTimeNull())
        {
            m_dateEnabledSwitch.IsToggled = true;
        }
    }

    protected abstract bool IsDateOrTimeNull();
    protected abstract View CreateDateOrTimePicker();
    protected virtual void OnSwitchToggled(object? sender, ToggledEventArgs e)
    {
        m_dateOrTimePicker.IsVisible = e.Value;
    }
}