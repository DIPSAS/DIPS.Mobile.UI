namespace DIPS.Mobile.UI.Components.Pickers.DatePicker.HorizontalInLine;

public partial class HorizontalInlineDatePicker
{
    public static readonly BindableProperty SelectedDateProperty = BindableProperty.Create(
        nameof(SelectedDate),
        typeof(DateTime),
        typeof(HorizontalInlineDatePicker), defaultValue: DateTime.Now);

    /// <summary>
    /// The date that people selected from the horizontal in line date picker.
    /// </summary>
    public DateTime SelectedDate
    {
        get => (DateTime)GetValue(SelectedDateProperty);
        set => SetValue(SelectedDateProperty, value);
    }

    public static readonly BindableProperty MaxSelectableDaysFromTodayProperty = BindableProperty.Create(
        nameof(MaxSelectableDaysFromToday),
        typeof(int),
        typeof(HorizontalInlineDatePicker), defaultValue: 1825);

    /// <summary>
    /// The max number of days that people can select from. This is used to set both a minimum days and a max days to limit the number of drawings the control has to do.
    /// </summary>
    /// <remarks>Default value is 1825, which is approximately 5 years</remarks>
    public int MaxSelectableDaysFromToday
    {
        get => (int)GetValue(MaxSelectableDaysFromTodayProperty);
        set => SetValue(MaxSelectableDaysFromTodayProperty, value);
    }
}