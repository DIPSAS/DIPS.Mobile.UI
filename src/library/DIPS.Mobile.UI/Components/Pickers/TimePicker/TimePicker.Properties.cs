using System.Windows.Input;

namespace DIPS.Mobile.UI.Components.Pickers.TimePicker;

public partial class TimePicker
{
    public static readonly BindableProperty SelectedTimeProperty = BindableProperty.Create(
        nameof(SelectedTime),
        typeof(TimeSpan),
        typeof(TimePicker), defaultBindingMode:BindingMode.TwoWay,
        propertyChanged: (bindable, _, _) => ((TimePicker)bindable).OnTimeChanged());

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
    /// The command to be executed when people selected a time from the date picker.
    /// </summary>
    public ICommand? SelectedTimeCommand
    {
        get => (ICommand)GetValue(SelectedTimeCommandProperty);
        set => SetValue(SelectedTimeCommandProperty, value);
    }
    
    /// <summary>
    /// The view that the time picker's popover should pass through (Most likely only a DatePicker)
    /// <remarks>For iOS</remarks>
    /// </summary>
    internal View? PassThroughView { get; set; }
}