using DIPS.Mobile.UI.Components.Pickers.ScrollPicker;
using DIPS.Mobile.UI.Components.Pickers.ScrollPicker.Component;

namespace Components.ComponentsSamples.Pickers.ScrollPickerComponents;

public class DayScrollPickerComponent : BaseScrollPickerComponent<int>
{
    protected override int GetDefaultSelectedItem()
    {
        return DateTime.Now.Day;
    }

    protected override int GetItem(int index)
    {
        return index + 1;
    }

    public override int GetItemsCount()
    {
        return 31;
    }

    protected override int IndexOfSelectedItem(int selectedItem)
    {
        return selectedItem - 1;
    }

    public override string GetItemText(int index)
    {
        return (index + 1).ToString();
    }
}