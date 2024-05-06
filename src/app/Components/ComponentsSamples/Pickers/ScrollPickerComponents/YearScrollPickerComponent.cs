using DIPS.Mobile.UI.Components.Pickers.ScrollPicker;

namespace Components.ComponentsSamples.Pickers.ScrollPickerComponents;

public class YearScrollPickerComponent : BaseScrollPickerComponent<int?>
{
    private const int StartYear = 0;
    private const int EndYear = 9999;

    public YearScrollPickerComponent(bool isNullable) : base(isNullable)
    {
    }
    
    public override void SetDefaultSelectedItem(IScrollPickerComponent.SetSelectedItemMode setSelectedItemMode = IScrollPickerComponent.SetSelectedItemMode.Programmatic)
    {
        SelectedItem = DateTime.Now.Year;
    }

    public override void SetSelectedItem(int? index, IScrollPickerComponent.SetSelectedItemMode setSelectedItemMode = IScrollPickerComponent.SetSelectedItemMode.Programmatic)
    {
        SelectedItem = index;
    }

    public override int GetItemsCount()
    {
        return EndYear - StartYear + 1;
    }

    public override int? GetSelectedItemIndex()
    {
        return SelectedItem - StartYear;
    }

    public override string GetItemText(int index)
    {
        return (StartYear + index).ToString();
    }
}