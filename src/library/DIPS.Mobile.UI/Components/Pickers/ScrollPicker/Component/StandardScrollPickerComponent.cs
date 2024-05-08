namespace DIPS.Mobile.UI.Components.Pickers.ScrollPicker.Component;

public class StandardScrollPickerComponent<TModel>(
    IList<TModel> items,
    int defaultIndex = default,
    Action<int>? onSelectedIndexChanged = null, bool isNullable = false, bool defaultValueOnlySetOnOpen = false)
    : BaseScrollPickerComponent(onSelectedIndexChanged)
{
    public override string GetTextAtIndex(int index)
    {
        return items[index]?.ToString() ?? string.Empty;
    }

    public override int GetItemsCount()
    {
        return items.Count;
    }

    protected override int GetDefaultIndex()
    {
        return defaultIndex;
    }

    protected override bool ShouldBeNullable()
    {
        return isNullable;
    }

    protected override bool ShouldDefaultValueOnlyBeSetOnOpen()
    {
        return defaultValueOnlySetOnOpen;
    }
}