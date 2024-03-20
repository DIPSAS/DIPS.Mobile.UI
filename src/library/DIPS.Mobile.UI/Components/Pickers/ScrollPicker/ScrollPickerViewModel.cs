namespace DIPS.Mobile.UI.Components.Pickers.ScrollPicker;

public sealed class ScrollPickerViewModel(params IScrollPickerComponent[] components) : IScrollPickerViewModel
{
    public int GetComponentCount()
    {
        return components.Length;
    }

    public int GetRowsInComponent(int component)
    {
        return components[component].GetItemsCount();
    }

    public string GetTextForRowInComponent(int row, int component)
    {
        return components[component].GetItemText(row);
    }

    public void SelectedRowInComponent(int row, int component)
    {
        components[component].SetSelectedItem(row);
        OnAnySelectedIndexesChanged?.Invoke();
    }

    public int SelectedIndexForComponent(int component)
    {
        return components[component].GetSelectedItemIndex();
    }

    public event Action? OnAnySelectedIndexesChanged;
}

public class StandardScrollPickerComponent<TModel> : List<TModel>, IScrollPickerComponent
{
    public StandardScrollPickerComponent(IReadOnlyList<TModel> items, TModel? selectedItem = default) : base(items)
    {
        SelectedItem = selectedItem is null ? items[0] : selectedItem;
    }
    
    public void SetSelectedItem(int index)
    {
        SelectedItem = base[index];
    }

    public int GetItemsCount()
    {
        return Count;
    }

    public int GetSelectedItemIndex()
    {
        return SelectedItem is not null ? base.IndexOf(SelectedItem) : 0;
    }

    public string GetItemText(int index)
    {
        return base[index]?.ToString() ?? string.Empty;
    }

    public TModel SelectedItem { get; private set; }
}

public interface IScrollPickerComponent
{
    /// <summary>
    /// Sets the selected item
    /// </summary>
    void SetSelectedItem(int index);
    
    /// <summary>
    /// Gets the number of items in the component
    /// </summary>
    int GetItemsCount();
    
    /// <summary>
    /// Gets the index of the selected item
    /// </summary>
    int GetSelectedItemIndex();
    
    /// <summary>
    /// Gets the text for the specified item
    /// </summary>
    string GetItemText(int index);
}