using DIPS.Mobile.UI.Components.Pickers.ScrollPicker;

namespace Components.ComponentsSamples.Pickers.ScrollPickerComponents;

public class YearScrollPickerComponent : BaseScrollPickerComponent
{
    private const int StartYear = 0;
    private const int EndYear = 9999;

    public override void SetSelectedItem(int index)
    {
        SelectedItem = index;
    }

    public override int GetItemsCount()
    {
        return EndYear - StartYear + 1;
    }

    public override int GetSelectedItemIndex()
    {
        return SelectedItem - StartYear;
    }

    public override string GetItemText(int index)
    {
        return (StartYear + index).ToString();
    }

    public int SelectedItem { get; private set; } = DateTime.Now.Year;
}