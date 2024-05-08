using DIPS.Mobile.UI.Components.Pickers.ScrollPicker.Component;

namespace Components.ComponentsSamples.Pickers.ScrollPickerComponents;

public class MonthScrollPickerComponent : BaseScrollPickerComponent
{
    private readonly string[] m_monthNames;

    public MonthScrollPickerComponent()
    {
        m_monthNames = System.Globalization.CultureInfo.CurrentCulture.DateTimeFormat.MonthNames;
    }

    public override int GetItemsCount()
    {
        return m_monthNames.Length - 1;
    }

    protected override int GetDefaultIndex()
    {
        return DateTime.Now.Month - 1;
    }

    protected override bool ShouldBeNullable()
    {
        return true;
    }

    protected override bool ShouldDefaultValueOnlyBeSetOnOpen()
    {
        return false;
    }

    public override string GetTextAtIndex(int index)
    {
        return m_monthNames[index];
    }
}