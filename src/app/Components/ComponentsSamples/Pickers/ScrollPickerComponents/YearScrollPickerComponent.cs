using DIPS.Mobile.UI.Components.Pickers.ScrollPicker;

namespace Components.ComponentsSamples.Pickers.ScrollPickerComponents;

public class YearScrollPickerComponent : IScrollPickerComponent
{
    private const int StartYear = 0;
    private const int EndYear = 9999;

    public void SetSelectedItem(int index)
    {
        SelectedItem = index;
    }

    public int GetItemsCount()
    {
        return EndYear - StartYear + 1;
    }

    public int GetSelectedItemIndex()
    {
        return SelectedItem - StartYear;
    }

    public string GetItemText(int index)
    {
        return (StartYear + index).ToString();
    }

    public int SelectedItem { get; private set; } = DateTime.Now.Year;
}