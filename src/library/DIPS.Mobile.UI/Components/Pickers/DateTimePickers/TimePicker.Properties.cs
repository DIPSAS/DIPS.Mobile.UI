using System.Windows.Input;
using DIPS.Mobile.UI.Converters.ValueConverters;

namespace DIPS.Mobile.UI.Components.Pickers.DateTimePickers;

public partial class TimePicker
{
    public static readonly BindableProperty DescriptionProperty = BindableProperty.Create(
        nameof(Description),
        typeof(string),
        typeof(TimePicker));

    /// <summary>
    /// The description for people to read when using the time picker.
    /// </summary>
    /// <remarks>This should provide a well suited description on what people are selecting a time for.</remarks>
    public string Description
    {
        get => (string)GetValue(DescriptionProperty);
        set => SetValue(DescriptionProperty, value);
    }
    
    public static readonly BindableProperty SelectedTimeProperty = BindableProperty.Create(
        nameof(SelectedTime),
        typeof(TimeSpan),
        typeof(TimePicker), propertyChanged: OnSelectedTimeChanged, defaultBindingMode:BindingMode.TwoWay);

    /// <summary>
    /// The time people selected from the time picker.
    /// </summary>
    public TimeSpan SelectedTime
    {
        get => (TimeSpan)GetValue(SelectedTimeProperty);
        set => SetValue(SelectedTimeProperty, value);
    }

    public static readonly BindableProperty SelectedTimeCommandProperty = BindableProperty.Create(
        nameof(SelectedTimeCommand),
        typeof(ICommand),
        typeof(TimePicker));

    /// <summary>
    /// The event to be raised when people selected a time from the picker.
    /// </summary>
    public event EventHandler<object>? DidSelectDate;
    
    /// <summary>
    /// The command to be executed when people selected a time from the date picker.
    /// </summary>
    public ICommand? SelectedTimeCommand
    {
        get => (ICommand)GetValue(SelectedTimeCommandProperty);
        set => SetValue(SelectedTimeCommandProperty, value);
    }
        
    public static readonly BindableProperty SelectedTimeDisplayFormat = BindableProperty.Create(
        nameof(TimeFormat),
        typeof(TimeConverter.TimeConverterFormat),
        typeof(TimePicker));

    /// <summary>
    /// The format of the <see cref="SelectedTime"/> when displaying it to people.
    /// </summary>
    public TimeConverter.TimeConverterFormat TimeFormat
    {
        get => (TimeConverter.TimeConverterFormat)GetValue(SelectedTimeDisplayFormat);
        set => SetValue(SelectedTimeDisplayFormat, value);
    }
}