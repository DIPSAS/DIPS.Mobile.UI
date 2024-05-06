using DIPS.Mobile.UI.Components.Pickers.ScrollPicker;

namespace Components.ComponentsSamples.Pickers.ScrollPickerComponents;

public class MonthScrollPickerComponent : BaseScrollPickerComponent<int>
{
    private readonly string[] m_monthNames;

    public MonthScrollPickerComponent()
    {
        m_monthNames = System.Globalization.CultureInfo.CurrentCulture.DateTimeFormat.MonthNames;
    }

    public override void SetDefaultSelectedItem(IScrollPickerComponent.SetSelectedItemMode setSelectedItemMode = IScrollPickerComponent.SetSelectedItemMode.Programmatic)
    {
        SelectedItem = DateTime.Now.Month;
    }

    public override void SetSelectedItem(int? index, IScrollPickerComponent.SetSelectedItemMode setSelectedItemMode = IScrollPickerComponent.SetSelectedItemMode.Programmatic)
    {
        SelectedItem = index;
    }

    public override int GetItemsCount()
    {
        return m_monthNames.Length - 1;
    }

    public override int? GetSelectedItemIndex()
    {
        return SelectedItem;
    }

    public override string GetItemText(int index)
    {
        return m_monthNames[index];
    }

    public int? SelectedItem { get; private set; } = DateTime.Now.Month;
}