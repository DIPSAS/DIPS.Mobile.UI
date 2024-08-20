using System.Windows.Input;

namespace DIPS.Mobile.UI.Components.Pickers.NullableTimePicker;

public partial class NullableTimePicker
{
    public static new readonly BindableProperty SelectedTimeProperty = BindableProperty.Create(
        nameof(SelectedTime),
        typeof(TimeSpan?),
        typeof(NullableTimePicker), defaultBindingMode:BindingMode.TwoWay,
        propertyChanged: (bindable, _, _) => ((NullableTimePicker)bindable).OnSelectedTimeChanged());

    /// <summary>
    /// The time people selected from the time picker.
    /// </summary>
    public new TimeSpan? SelectedTime
    {
        get => (TimeSpan?)GetValue(SelectedTimeProperty);
        set => SetValue(SelectedTimeProperty, value);
    }
}