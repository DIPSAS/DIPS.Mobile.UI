using DIPS.Mobile.UI.Components.Pickers.ScrollPicker;

namespace Components.ComponentsSamples.Pickers.ScrollPickerComponents;

public class MonthScrollPickerComponent : BaseScrollPickerComponent<int>
{
    private readonly string[] m_monthNames;

    public MonthScrollPickerComponent()
    {
        m_monthNames = System.Globalization.CultureInfo.CurrentCulture.DateTimeFormat.MonthNames;
    }

    protected override int GetDefaultSelectedItem()
    {
        return DateTime.Now.Month;
    }

    protected override int GetItemBasedOnIndex(int? index)
    {
        return index!.Value;
    }

    public override int GetItemsCount()
    {
        return m_monthNames.Length - 1;
    }

    public override int? GetSelectedItemIndex(int selectedItem)
    {
        return selectedItem;
    }

    public override string GetItemText(int index)
    {
        return m_monthNames[index];
    }

    public int? SelectedItem { get; private set; } = DateTime.Now.Month;
}