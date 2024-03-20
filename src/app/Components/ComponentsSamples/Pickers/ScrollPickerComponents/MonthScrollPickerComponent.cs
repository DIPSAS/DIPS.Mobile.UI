using DIPS.Mobile.UI.Components.Pickers.ScrollPicker;

namespace Components.ComponentsSamples.Pickers.ScrollPickerComponents;

public class MonthScrollPickerComponent : IScrollPickerComponent
{
    private readonly string[] m_monthNames;

    public MonthScrollPickerComponent()
    {
        m_monthNames = System.Globalization.CultureInfo.CurrentCulture.DateTimeFormat.MonthNames;
    }
    
    public void SetSelectedItem(int index)
    {
        SelectedItem = index;
    }

    public int GetItemsCount()
    {
        return m_monthNames.Length - 1;
    }

    public int GetSelectedItemIndex()
    {
        return SelectedItem;
    }

    public string GetItemText(int index)
    {
        return m_monthNames[index];
    }

    public int SelectedItem { get; private set; } = DateTime.Now.Month;
}