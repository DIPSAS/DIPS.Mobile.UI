using System.Windows.Input;

namespace DIPS.Mobile.UI.Components.Pickers.NullableTimePicker;

public partial class NullableTimePicker
{
    public static readonly BindableProperty SelectedTimeProperty = BindableProperty.Create(
        nameof(SelectedTime),
        typeof(TimeSpan?),
        typeof(NullableTimePicker), defaultBindingMode:BindingMode.TwoWay,
        propertyChanged: (bindable, _, _) => ((NullableTimePicker)bindable).OnSelectedTimeChanged());

    /// <summary>
    /// The time people selected from the time picker.
    /// </summary>
    public TimeSpan? SelectedTime
    {
        get => (TimeSpan?)GetValue(SelectedTimeProperty);
        set => SetValue(SelectedTimeProperty, value);
    }

    public static readonly BindableProperty SelectedTimeCommandProperty = BindableProperty.Create(
        nameof(SelectedTimeCommand),
        typeof(ICommand),
        typeof(NullableTimePicker));

    /// <summary>
    /// The command to be executed when people selected a time from the date picker.
    /// </summary>
    public ICommand? SelectedTimeCommand
    {
        get => (ICommand)GetValue(SelectedTimeCommandProperty);
        set => SetValue(SelectedTimeCommandProperty, value);
    }
}