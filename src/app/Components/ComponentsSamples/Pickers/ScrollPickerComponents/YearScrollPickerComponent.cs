using DIPS.Mobile.UI.Components.Pickers.ScrollPicker;
using DIPS.Mobile.UI.Components.Pickers.ScrollPicker.Component;

namespace Components.ComponentsSamples.Pickers.ScrollPickerComponents;

public class YearScrollPickerComponent : BaseScrollPickerComponent
{
    private const int StartYear = 0;
    private const int EndYear = 9999;

    public override int GetItemsCount()
    {
        return EndYear - StartYear + 1;
    }

    protected override int GetDefaultIndex()
    {
        return DateTime.Now.Year - 1;
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
        return (StartYear + index).ToString();
    }
}