using DIPS.Mobile.UI.Components.Pickers.ScrollPicker;

namespace Components.ComponentsSamples.Pickers.ScrollPickerComponents;

public class YearScrollPickerComponent : BaseScrollPickerComponent<int>
{
    private const int StartYear = 0;
    private const int EndYear = 9999;

    public YearScrollPickerComponent(bool isNullable) : base(null, isNullable)
    {
    }

    protected override int GetDefaultSelectedItem()
    {
        return DateTime.Now.Year;
    }

    protected override int GetItem(int index)
    {
        return index;
    }

    public override int GetItemsCount()
    {
        return EndYear - StartYear + 1;
    }

    protected override int IndexOfSelectedItem(int selectedItem)
    {
        return selectedItem - StartYear;
    }

    public override string GetItemText(int index)
    {
        return (StartYear + index).ToString();
    }
}