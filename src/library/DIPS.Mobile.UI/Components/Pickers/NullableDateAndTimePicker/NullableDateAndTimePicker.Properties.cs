using System.Windows.Input;

namespace DIPS.Mobile.UI.Components.Pickers.NullableDateAndTimePicker;

public partial class NullableDateAndTimePicker
{
    public static readonly BindableProperty SelectedDateTimeProperty = BindableProperty.Create(
        nameof(SelectedDateTime),
        typeof(DateTime?),
        typeof(NullableDateAndTimePicker), defaultBindingMode: BindingMode.TwoWay);

    /// <summary>
    /// The date people selected from the date picker.
    /// </summary>
    public DateTime? SelectedDateTime
    {
        get => (DateTime?)GetValue(SelectedDateTimeProperty);
        set => SetValue(SelectedDateTimeProperty, value);
    }

    public static readonly BindableProperty SelectedDateTimeCommandProperty = BindableProperty.Create(
        nameof(SelectedDateTimeCommand),
        typeof(ICommand),
        typeof(NullableDateAndTimePicker));

    /// <summary>
    /// The command to be executed when people selected a date from the date picker.
    /// </summary>
    public ICommand? SelectedDateTimeCommand
    {
        get => (ICommand)GetValue(SelectedDateTimeCommandProperty);
        set => SetValue(SelectedDateTimeCommandProperty, value);
    }

    public static readonly BindableProperty IgnoreLocalTimeProperty = BindableProperty.Create(
        nameof(IgnoreLocalTime),
        typeof(bool),
        typeof(NullableDateAndTimePicker));

    /// <summary>
    /// If this is false, the <see cref="DateAndTimePicker"/> will use local time zone.
    /// If this is true, the <see cref="DateAndTimePicker"/> will use UTC time zone.
    /// </summary>
    public bool IgnoreLocalTime
    {
        get => (bool)GetValue(IgnoreLocalTimeProperty);
        set => SetValue(IgnoreLocalTimeProperty, value);
    }

    public static readonly BindableProperty MinimumDateProperty = BindableProperty.Create(
        nameof(MinimumDate),
        typeof(DateTime?),
        typeof(NullableDateAndTimePicker));

    /// <summary>
    /// Sets the minimum date that people can choose
    /// </summary>
    public DateTime? MinimumDate
    {
        get => (DateTime?)GetValue(MinimumDateProperty);
        set => SetValue(MinimumDateProperty, value);
    }

    public static readonly BindableProperty MaximumDateProperty = BindableProperty.Create(
        nameof(MaximumDate),
        typeof(DateTime?),
        typeof(NullableDateAndTimePicker));

    /// <summary>
    /// Sets the maximum date that people can choose
    /// </summary>
    public DateTime? MaximumDate
    {
        get => (DateTime?)GetValue(MaximumDateProperty);
        set => SetValue(MaximumDateProperty, value);
    }
}