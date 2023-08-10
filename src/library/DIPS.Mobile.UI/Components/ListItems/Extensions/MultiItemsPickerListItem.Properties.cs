using DIPS.Mobile.UI.Components.Pickers.MultiItemsPicker;

namespace DIPS.Mobile.UI.Components.ListItems.Extensions;

public partial class MultiItemsPickerListItem
{
    public static readonly BindableProperty MultiItemPickerProperty = BindableProperty.Create(
        nameof(MultiItemPicker),
        typeof(MultiItemsPicker),
        typeof(MultiItemsPicker), propertyChanged: (bindable, value, newValue) => ((MultiItemsPickerListItem)bindable).MultiItemPickerPropertyChanged());

    public MultiItemsPicker? MultiItemPicker
    {
        get => (MultiItemsPicker)GetValue(MultiItemPickerProperty);
        set => SetValue(MultiItemPickerProperty, value);
    }
}