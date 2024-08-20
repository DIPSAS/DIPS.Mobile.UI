namespace DIPS.Mobile.UI.Components.Pickers.DateAndTimePicker;

public partial class DateAndTimePicker
{
    private DateTime m_dateSetFromPickers;
    
    protected readonly DatePicker.DatePicker DatePicker;
    protected readonly TimePicker.TimePicker TimePicker;
    
    public DateAndTimePicker()
    {
        DatePicker = new DatePicker.DatePicker();
        TimePicker = new TimePicker.TimePicker();

        DatePicker.SelectedDateCommand = new Command(() =>
        {
            SetSelectedDateTime();
            SelectedDateTimeCommand?.Execute(null);
        });
        TimePicker.SelectedTimeCommand = new Command(() =>
        {
            SetSelectedDateTime();
            SelectedDateTimeCommand?.Execute(null);
        });

        Spacing = Sizes.GetSize(SizeName.size_1);
        
        Add(DatePicker);
        Add(TimePicker);
    }

    private void SetSelectedDateTime()
    {
        var dateTime = new DateTime(DatePicker.SelectedDate.Year, 
            DatePicker.SelectedDate.Month,
            DatePicker.SelectedDate.Day, 
            TimePicker.SelectedTime.Hours, 
            TimePicker.SelectedTime.Minutes, 
            TimePicker.SelectedTime.Seconds, 
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
        
        DatePicker.SelectedDate = dateTime;
        TimePicker.SelectedTime = timeSpan;
    }

}