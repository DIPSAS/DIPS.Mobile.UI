using DIPS.Mobile.UI.Components.Pickers.ScrollPicker;
using DIPS.Mobile.UI.Components.Pickers.ScrollPicker.Component;

namespace Components.ComponentsSamples.Pickers.ScrollPickerComponents;

public class DayScrollPickerComponent : BaseScrollPickerComponent
{
    public override int GetItemsCount()
    {
        return 31;
    }

    protected override int GetDefaultIndex()
    {
        return DateTime.Now.Day - 1;
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
        return (index + 1).ToString();
    }
}