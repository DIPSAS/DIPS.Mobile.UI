using System.Windows.Input;

namespace DIPS.Mobile.UI.Components.Pickers.NullableDatePicker;

public partial class NullableDatePicker
{
    public static readonly BindableProperty SelectedDateProperty = BindableProperty.Create(
        nameof(SelectedDate),
        typeof(DateTime?),
        typeof(NullableDatePicker), defaultBindingMode: BindingMode.TwoWay,
        propertyChanged: (bindable, _, _) => ((NullableDatePicker)bindable).OnSelectedDateChanged());

    public new DateTime? SelectedDate
    {
        get => (DateTime?)GetValue(SelectedDateProperty);
        set => SetValue(SelectedDateProperty, value);
    }

}