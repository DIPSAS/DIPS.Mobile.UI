using DIPS.Mobile.UI.Components.Pickers.ScrollPicker;

namespace Components.ComponentsSamples.Pickers.ScrollPickerComponents;

public class DayScrollPickerComponent : BaseScrollPickerComponent
{
    public override void SetSelectedItem(int index)
    {
        SelectedItem = index + 1;
    }

    public override int GetItemsCount()
    {
        return 31;
    }

    public override int GetSelectedItemIndex()
    {
        return SelectedItem - 1;
    }

    public override string GetItemText(int index)
    {
        return (index + 1).ToString();
    }

    public int SelectedItem { get; private set; } = DateTime.Now.Day;
}