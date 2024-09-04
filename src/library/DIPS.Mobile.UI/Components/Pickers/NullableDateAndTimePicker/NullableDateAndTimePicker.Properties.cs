using System.Windows.Input;

namespace DIPS.Mobile.UI.Components.Pickers.NullableDateAndTimePicker;

public partial class NullableDateAndTimePicker
{
    public static new readonly BindableProperty SelectedDateTimeProperty = BindableProperty.Create(
        nameof(SelectedDateTime),
        typeof(DateTime?),
        typeof(NullableDateAndTimePicker), defaultBindingMode: BindingMode.TwoWay,
        propertyChanged: (bindable, _, _) => ((NullableDateAndTimePicker)bindable).OnSelectedDateTimeChanged());

    /// <summary>
    /// The date people selected from the date picker.
    /// </summary>
    public new DateTime? SelectedDateTime
    {
        get => (DateTime?)GetValue(SelectedDateTimeProperty);
        set => SetValue(SelectedDateTimeProperty, value);
    }
}