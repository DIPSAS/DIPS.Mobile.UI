using DIPS.Mobile.UI.Components.Pickers.Platforms;

namespace DIPS.Mobile.UI.Components.Pickers.NullableDateTimePicker;

public partial class NullableDateTimePicker : HorizontalStackLayout
{
    private IDateTimePicker m_dateTimePicker;
    private readonly Images.ImageButton.ImageButton m_clearButton;

    public NullableDateTimePicker()
    {
        m_clearButton = new Images.ImageButton.ImageButton 
        { 
            Source = Icons.GetIcon(IconName.close_circle_line), 
            IsVisible = false,
            Command = new Command(() =>
            {
                SetClearButtonVisibility(false);
                SelectedDate = new DateTime();
                SelectedTime = TimeSpan.Zero;
            })
        };
        
        Add(m_clearButton);
    }

    protected override void OnHandlerChanged()
    {
        base.OnHandlerChanged();

        View picker;
        
        if (Type == DatePickerType.Date)
        {
            var datePicker = new DatePicker.DatePicker { IsNullable = true, SelectedDateCommand = new Command(() => SetClearButtonVisibility(true)) };
            datePicker.SetBinding(DatePicker.DatePicker.SelectedDateProperty, new Binding(nameof(SelectedDate), mode: BindingMode.TwoWay, source: this));
            datePicker.SetBinding(DatePicker.DatePicker.MaximumDateProperty, new Binding(nameof(MaximumDate), source: this));
            datePicker.SetBinding(DatePicker.DatePicker.MinimumDateProperty, new Binding(nameof(MinimumDate), source: this));
            m_dateTimePicker = datePicker;

            picker = datePicker;
        }
        else if(Type == DatePickerType.DateAndTime)
        {
            var dateAndTimePicker = new DateAndTimePicker.DateAndTimePicker { IsNullable = true, SelectedDateTimeCommand = new Command(() => SetClearButtonVisibility(true)) };
            dateAndTimePicker.SetBinding(DateAndTimePicker.DateAndTimePicker.SelectedDateTimeProperty, new Binding(nameof(SelectedDate), mode: BindingMode.TwoWay, source: this));
            dateAndTimePicker.SetBinding(DateAndTimePicker.DateAndTimePicker.MaximumDateProperty, new Binding(nameof(MaximumDate), source: this));
            dateAndTimePicker.SetBinding(DateAndTimePicker.DateAndTimePicker.MinimumDateProperty, new Binding(nameof(MinimumDate), source: this));
            m_dateTimePicker = dateAndTimePicker;

            picker = dateAndTimePicker;
        }
        else
        {
            var timePicker = new TimePicker.TimePicker { IsNullable = true, SelectedTimeCommand = new Command(() => SetClearButtonVisibility(true)) };
            timePicker.SetBinding(TimePicker.TimePicker.SelectedTimeProperty, new Binding(nameof(SelectedTime), mode: BindingMode.TwoWay, source: this));
            
            m_dateTimePicker = timePicker;

            picker = timePicker;
        }
        
        picker.SetBinding(HorizontalOptionsProperty, new Binding(nameof(picker.HorizontalOptions), source: this));
        
        
        Insert(0, m_dateTimePicker);
        
        if (SelectedDate != default)
        {
            SetClearButtonVisibility(true);
        }
    }

    private void SetClearButtonVisibility(bool state)
    {
        m_clearButton.IsVisible = state;
    }
}