using DIPS.Mobile.UI.Components.Pickers.MultiItemsPicker;

namespace DIPS.Mobile.UI.Components.ListItems.Extensions;

public partial class MultiItemsPickerListItem
{
    public static readonly BindableProperty MultiItemsPickerProperty = BindableProperty.Create(
        nameof(MultiItemsPicker),
        typeof(MultiItemsPicker),
        typeof(MultiItemsPicker), propertyChanged: (bindable, value, newValue) => ((MultiItemsPickerListItem)bindable).MultiItemPickerPropertyChanged());

    public MultiItemsPicker? MultiItemsPicker
    {
        get => (MultiItemsPicker)GetValue(MultiItemsPickerProperty);
        set => SetValue(MultiItemsPickerProperty, value);
    }
}