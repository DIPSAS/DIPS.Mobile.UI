namespace DIPS.Mobile.UI.Components.Pickers.NullableDateTimePicker;

public partial class NullableDateTimePicker
{
    public static readonly BindableProperty SelectedDateProperty = BindableProperty.Create(
        nameof(SelectedDate),
        typeof(DateTime),
        typeof(NullableDateTimePicker),
        defaultBindingMode:BindingMode.TwoWay);
    
    public static readonly BindableProperty SelectedTimeProperty = BindableProperty.Create(
        nameof(SelectedTime),
        typeof(TimeSpan),
        typeof(NullableDateTimePicker));
    
    public static readonly BindableProperty IgnoreLocalTimeProperty = BindableProperty.Create(
        nameof(IgnoreLocalTime),
        typeof(bool),
        typeof(NullableDateTimePicker));
    
    public static readonly BindableProperty MaximumDateProperty = BindableProperty.Create(
        nameof(MaximumDate),
        typeof(DateTime?),
        typeof(NullableDateTimePicker));
    
    public static readonly BindableProperty MinimumDateProperty = BindableProperty.Create(
        nameof(MinimumDate),
        typeof(DateTime?),
        typeof(NullableDateTimePicker));

    /// <summary>
    /// The date people selected from the date picker.
    /// </summary>
    /// <remarks>Only valid when <see cref="Type"/> is of <see cref="DatePickerType.Date"/> or <see cref="DatePickerType.DateAndTime"/></remarks>
    public DateTime SelectedDate
    {
        get => (DateTime)GetValue(SelectedDateProperty);
        set => SetValue(SelectedDateProperty, value);
    }

    /// <summary>
    /// The time people selected from the time picker.
    /// </summary>
    /// <remarks>Only valid when <see cref="Type"/> is of <see cref="DatePickerType.Time"/></remarks>
    public TimeSpan SelectedTime
    {
        get => (TimeSpan)GetValue(SelectedTimeProperty);
        set => SetValue(SelectedTimeProperty, value);
    }
        
    /// <summary>
    /// If this is false, the <see cref="DatePicker"/> will use local time zone.
    /// If this is true, the <see cref="DatePicker"/> will use UTC time zone.
    /// </summary>
    /// <remarks>Only valid when <see cref="Type"/> is of <see cref="DatePickerType.Date"/> or <see cref="DatePickerType.DateAndTime"/></remarks>
    public bool IgnoreLocalTime
    {
        get => (bool)GetValue(IgnoreLocalTimeProperty);
        set => SetValue(IgnoreLocalTimeProperty, value);
    }
        
    /// <summary>
    /// Sets the minimum date that people can choose
    /// </summary>
    /// <remarks>Only valid when <see cref="Type"/> is of <see cref="DatePickerType.Date"/> or <see cref="DatePickerType.DateAndTime"/></remarks>
    public DateTime? MinimumDate
    {
        get => (DateTime?)GetValue(MinimumDateProperty);
        set => SetValue(MinimumDateProperty, value);
    }

    /// <summary>
    /// Sets the maximum date that people can choose
    /// </summary>
    /// <remarks>Only valid when <see cref="Type"/> is of <see cref="DatePickerType.Date"/> or <see cref="DatePickerType.DateAndTime"/></remarks>
    public DateTime? MaximumDate
    {
        get => (DateTime?)GetValue(MaximumDateProperty);
        set => SetValue(MaximumDateProperty, value);
    }
    
    public DatePickerType Type { get; set; }
}


public enum DatePickerType
{
    Date,
    Time,
    DateAndTime
}
