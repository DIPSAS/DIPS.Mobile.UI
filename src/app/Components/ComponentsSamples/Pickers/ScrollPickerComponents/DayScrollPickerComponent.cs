using DIPS.Mobile.UI.Components.Pickers.ScrollPicker;

namespace Components.ComponentsSamples.Pickers.ScrollPickerComponents;

public class DayScrollPickerComponent : BaseScrollPickerComponent<int?>
{
    public DayScrollPickerComponent(bool isNullable) : base(null, isNullable)
    {
    }

    protected override int? GetDefaultSelectedItem()
    {
        return DateTime.Now.Day;
    }

    public override void SetSelectedItem(int? index, IScrollPickerComponent.SetSelectedItemMode setSelectedItemMode = IScrollPickerComponent.SetSelectedItemMode.Programmatic)
    {
        SelectedItem = index + 1;
    }

    public override int GetItemsCount()
    {
        return 31;
    }

    public override int? GetSelectedItemIndex()
    {
        return SelectedItem - 1;
    }

    public override string GetItemText(int index)
    {
        return (index + 1).ToString();
    }
}