namespace DIPS.Mobile.UI.Components.Pickers.DateAndTimePicker;

public partial class DateAndTimePicker
{
    private DateTime m_dateSetFromPickers;
    
    private readonly DatePicker.DatePicker m_datePicker;
    private readonly TimePicker.TimePicker m_timePicker;
    
    public DateAndTimePicker()
    {
        m_datePicker = new DatePicker.DatePicker();
        m_timePicker = new TimePicker.TimePicker();

        m_datePicker.SelectedDateCommand = new Command(() =>
        {
            SetSelectedDateTime();
            SelectedDateTimeCommand?.Execute(null);
        });
        m_timePicker.SelectedTimeCommand = new Command(() =>
        {
            SetSelectedDateTime();
            SelectedDateTimeCommand?.Execute(null);
        });

        Spacing = Sizes.GetSize(SizeName.size_1);
        
        Add(m_datePicker);
        Add(m_timePicker);
    }

    private void SetSelectedDateTime()
    {
        var dateTime = new DateTime(m_datePicker.SelectedDate.Year, 
            m_datePicker.SelectedDate.Month,
            m_datePicker.SelectedDate.Day, 
            m_timePicker.SelectedTime.Hours, 
            m_timePicker.SelectedTime.Minutes, 
            m_timePicker.SelectedTime.Seconds, 
            SelectedDateTime.Kind);
        
        m_dateSetFromPickers = dateTime.ConvertDate(IgnoreLocalTime);

        SelectedDateTime = m_dateSetFromPickers;
    }

    protected override void OnHandlerChanged()
    {
        base.OnHandlerChanged();
        
        // SelectedDate should not be above maximum date
        if (MaximumDate != null && SelectedDateTime > MaximumDate)
        {
            SelectedDateTime = new DateTime(MaximumDate.Value.Year,
                MaximumDate.Value.Month,
                MaximumDate.Value.Day,
                SelectedDateTime.Hour, SelectedDateTime.Minute, SelectedDateTime.Second, SelectedDateTime.Kind);
        }

        // SelectedDate should not be below minimum date
        if (MinimumDate != null && SelectedDateTime < MinimumDate)
        {
            SelectedDateTime = new DateTime(MinimumDate.Value.Year,
                MinimumDate.Value.Month,
                MinimumDate.Value.Day,
                SelectedDateTime.Hour, SelectedDateTime.Minute, SelectedDateTime.Second, SelectedDateTime.Kind);
        }
    }

    protected partial void InternalSelectedDateTimeChanged(DateTime selectedDateTime)
    {
        // If the date is already set from the pickers, we don't want to show the converted date to the consumer.
        if(m_dateSetFromPickers == selectedDateTime)
            return;

        var dateTime = selectedDateTime.ConvertDate(IgnoreLocalTime);
        
        var timeSpan = new TimeSpan(dateTime.Hour,
            dateTime.Minute,
            dateTime.Second);
        
        m_datePicker.SelectedDate = dateTime;
        m_timePicker.SelectedTime = timeSpan;
    }

}