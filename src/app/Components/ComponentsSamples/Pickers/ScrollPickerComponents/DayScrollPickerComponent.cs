using DIPS.Mobile.UI.Components.Pickers.ScrollPicker;

namespace Components.ComponentsSamples.Pickers.ScrollPickerComponents;

public class DayScrollPickerComponent : IScrollPickerComponent
{
    public void SetSelectedItem(int index)
    {
        SelectedItem = index + 1;
    }

    public int GetItemsCount()
    {
        return 31;
    }

    public int GetSelectedItemIndex()
    {
        return SelectedItem - 1;
    }

    public string GetItemText(int index)
    {
        return (index + 1).ToString();
    }

    public int SelectedItem { get; private set; } = DateTime.Now.Day;
}