namespace DIPS.Mobile.UI.Components.Pickers.ScrollPicker.Component;

public class StandardScrollPickerComponent<TModel>(
    IList<TModel> items,
    TModel? defaultValue = default,
    Action<object?>? onSelectedItemChanged = null)
    : BaseScrollPickerComponent<TModel>(onSelectedItemChanged)
{
    protected override TModel GetDefaultSelectedItem()
    {
        return defaultValue ?? items[0];
    }

    protected override TModel GetItem(int index)
    {
        return items[index];
    }

    public override int GetItemsCount()
    {
        return items.Count;
    }

    protected override int IndexOfSelectedItem(TModel selectedItem)
    {
        return items.IndexOf(selectedItem);
    }

    public override string GetItemText(int index)
    {
        return items[index]?.ToString() ?? string.Empty;
    }
}