using System.Windows.Input;
using DIPS.Mobile.UI.Converters.ValueConverters;

namespace DIPS.Mobile.UI.Components.Pickers.DateTimePickers;

public partial class DateAndTimePicker
{
    public static readonly BindableProperty DescriptionProperty = BindableProperty.Create(
        nameof(Description),
        typeof(string),
        typeof(DateAndTimePicker));

    /// <summary>
    /// The description for people to read when using the date picker.
    /// </summary>
    /// <remarks>This should provide a well suited description on what people are selecting a date for.</remarks>
    public string Description
    {
        get => (string)GetValue(DescriptionProperty);
        set => SetValue(DescriptionProperty, value);
    }

    public static readonly BindableProperty SelectedDateTimeProperty = BindableProperty.Create(
        nameof(SelectedDateTime),
        typeof(System.DateTime),
        typeof(DateAndTimePicker), propertyChanged: OnSelectedDateChanged, defaultBindingMode:BindingMode.TwoWay);

    /// <summary>
    /// The date people selected from the date picker.
    /// </summary>
    public System.DateTime SelectedDateTime
    {
        get => (System.DateTime)GetValue(SelectedDateTimeProperty);
        set => SetValue(SelectedDateTimeProperty, value);
    }

    public static readonly BindableProperty SelectedDateTimeCommandProperty = BindableProperty.Create(
        nameof(SelectedDateTimeCommand),
        typeof(ICommand),
        typeof(DateAndTimePicker));

    /// <summary>
    /// The event to be raised when people selected a date from the picker.
    /// </summary>
    public event EventHandler<object>? DidSelectDateTime;

    /// <summary>
    /// The command to be executed when people selected a date from the date picker.
    /// </summary>
    public ICommand? SelectedDateTimeCommand
    {
        get => (ICommand)GetValue(SelectedDateTimeCommandProperty);
        set => SetValue(SelectedDateTimeCommandProperty, value);
    }
    
    public static readonly BindableProperty SelectedDateTimeDisplayFormat = BindableProperty.Create(
        nameof(DateFormat),
        typeof(DateAndTimeConverter.DateAndTimeConverterFormat),
        typeof(DateAndTimePicker));

    /// <summary>
    /// The format of the <see cref="SelectedDateTime"/> when displaying it to people.
    /// </summary>
    public DateAndTimeConverter.DateAndTimeConverterFormat DateFormat
    {
        get => (DateAndTimeConverter.DateAndTimeConverterFormat)GetValue(SelectedDateTimeDisplayFormat);
        set => SetValue(SelectedDateTimeDisplayFormat, value);
    }

    public static readonly BindableProperty IgnoreLocalTimeProperty = BindableProperty.Create(
        nameof(IgnoreLocalTime),
        typeof(bool),
        typeof(DateAndTimePicker));

    public bool IgnoreLocalTime
    {
        get => (bool)GetValue(IgnoreLocalTimeProperty);
        set => SetValue(IgnoreLocalTimeProperty, value);
    }
    
}