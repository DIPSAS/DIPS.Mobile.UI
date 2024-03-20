namespace DIPS.Mobile.UI.Components.Pickers.ScrollPicker;

public partial class ScrollPickerHandler
{
    public ScrollPickerHandler() : base(ScrollPickerPropertyMapper)
    {
    }
    
    public static readonly IPropertyMapper<ScrollPicker, ScrollPickerHandler> ScrollPickerPropertyMapper = new PropertyMapper<ScrollPicker, ScrollPickerHandler>(ViewMapper)
    {
        [nameof(ScrollPicker.SelectedIndex)] = MapSelectedIndex
    };

    private static partial void MapSelectedIndex(ScrollPickerHandler handler, ScrollPicker scrollPicker);
}