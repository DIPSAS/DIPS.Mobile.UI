namespace DIPS.Mobile.UI.Components.Pickers.ScrollPicker;

internal class ScrollPickerViewModel : IScrollPickerViewModel
{
    private readonly IReadOnlyList<IScrollPickerComponent>? m_components;

    internal ScrollPickerViewModel(IReadOnlyList<IScrollPickerComponent>? components)
    {
        m_components = components;

        if (m_components is null)
            return;
        
        foreach (var scrollPickerComponent in m_components)
        {
            scrollPickerComponent.OnDataInvalidated += DataInvalidated;
        }
    }

    private void DataInvalidated()
    {
        OnAnyComponentsDataInvalidated?.Invoke();
    }

    public int GetComponentCount()
    {
        return m_components?.Count ?? 0;
    }

    public int GetRowsInComponent(int component)
    {
        return m_components is null ? 0 : m_components[component].GetItemsCount();
    }

    public string GetTextForRowInComponent(int row, int component)
    {
        return m_components is null ? string.Empty : m_components[component].GetItemText(row);
    }

    public void SelectedRowInComponent(int row, int component)
    {
        if(m_components is null)
            return;
        
        m_components[component].SetSelectedItem(row, IScrollPickerComponent.SetSelectedItemMode.Tapped);
        OnAnySelectedIndexesChanged?.Invoke();
    }

    public int SelectedIndexForComponent(int component)
    {
        return m_components is null ? 0 : m_components[component].GetSelectedItemIndex();
    }

    public void SetDefaultSelectedItemsForAllComponents()
    {
        if (m_components == null)
            return;

        foreach (var component in m_components)
        {
            if (component.SelectedItem is not null)
                continue;
            
            component.SetDefaultSelectedItem();
        }
    }

    public void SendSelectedIndicesChanged()
    {
        OnAnySelectedIndexesChanged?.Invoke();
    }

    public void SetToNull()
    {
        if (m_components is null)
            return;
        
        foreach (var scrollPickerComponent in m_components)
        {
            scrollPickerComponent.SetSelectedItem(null, IScrollPickerComponent.SetSelectedItemMode.Tapped);
        }
        
        OnAnySelectedIndexesChanged?.Invoke();
    }

    public bool IsNullable => m_components?.Any(component => component.IsNullable) ?? false;

    public bool IsComponentsSelectedItemNull => m_components?.Any(scrollPickerComponent => scrollPickerComponent.SelectedItem is null) ?? true;

    public event Action? OnAnySelectedIndexesChanged;
    public event Action? OnAnyComponentsDataInvalidated;

    public void Dispose()
    {
        if (m_components is null)
            return;
        
        foreach (var scrollPickerComponent in m_components)
        {
            scrollPickerComponent.OnDataInvalidated -= DataInvalidated;
        }
    }
}

public class StandardScrollPickerComponent<TModel> : BaseScrollPickerComponent<TModel>
{
    private readonly IList<TModel> m_items;
   
    private readonly TModel? m_defaultSelectedItem;

    public StandardScrollPickerComponent(IList<TModel> items, TModel? selectedItem = default, Action<object?>? onSelectedItemChanged = null, bool isNullable = false) : base(onSelectedItemChanged, isNullable)
    {
        m_items = items;
        m_defaultSelectedItem = selectedItem;
    }

    protected override TModel? GetDefaultSelectedItem()
    {
        if (IsNullable)
        {
            return m_defaultSelectedItem;
        }
        
        return m_defaultSelectedItem ?? m_items[0];
    }

    protected override TModel GetItem(int index)
    {
        return m_items[index];
    }

    public override int GetItemsCount()
    {
        return m_items.Count;
    }

    protected override int IndexOfSelectedItem(TModel selectedItem)
    {
        return m_items.IndexOf(selectedItem);
    }

    public override string GetItemText(int index)
    {
        return m_items[index]?.ToString() ?? string.Empty;
    }
}

public abstract class BaseScrollPickerComponent<TModel> : IScrollPickerComponent
{
    private readonly Action<object?>? m_onSelectedItemChanged;
    
    protected BaseScrollPickerComponent(Action<object?>? onSelectedItemChanged = null, bool isNullable = false)
    {
        m_onSelectedItemChanged = onSelectedItemChanged;
        IsNullable = isNullable;
    }

    public object? SelectedItem { get; private set; }
    public bool IsNullable { get; }
    protected abstract TModel? GetDefaultSelectedItem();
    public void SetDefaultSelectedItem()
    {
        SelectedItem = GetDefaultSelectedItem();
    }
    public void SetSelectedItem(int? index, IScrollPickerComponent.SetSelectedItemMode setSelectedItemMode = IScrollPickerComponent.SetSelectedItemMode.Programmatic)
    {
        SelectedItem = index is null ? null : GetItem(index.Value);
        
        if(setSelectedItemMode == IScrollPickerComponent.SetSelectedItemMode.Tapped)
            m_onSelectedItemChanged?.Invoke(SelectedItem);
    }

    public int GetSelectedItemIndex()
    {
        return SelectedItem is not null ? IndexOfSelectedItem((TModel)SelectedItem) : 0;
    }
    
    protected abstract TModel GetItem(int index);
    public abstract int GetItemsCount();
    protected abstract int IndexOfSelectedItem(TModel selectedItem);
    public abstract string GetItemText(int index);
    public event Action? OnDataInvalidated;
    public void InvalidateData()
    {
        OnDataInvalidated?.Invoke();
    }
}

public interface IScrollPickerComponent
{
    /// <summary>
    /// The currently selected item
    /// </summary>
    object? SelectedItem { get; }
    
    /// <summary>
    /// Checks whether the generic implementation of TModel is nullable
    /// </summary>
    bool IsNullable { get; }

    /// <summary>
    /// Sets the default selected item
    /// </summary>
    void SetDefaultSelectedItem();
    
    /// <summary>
    /// Sets the selected item
    /// </summary>
    void SetSelectedItem(int? index, SetSelectedItemMode setSelectedItemMode = SetSelectedItemMode.Programmatic);
    
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
    
    /// <summary>
    /// Invoked when the selected item is changed
    /// </summary>
    event Action? OnDataInvalidated;
    
    /// <summary>
    /// Call this when you have changed the selected item
    /// </summary>
    void InvalidateData();
    
    public enum SetSelectedItemMode
    {
        /// <summary>
        /// If the user has tapped/scrolled in the ScrollPicker to set the selected item
        /// </summary>
        Tapped,
        /// <summary>
        /// If the selected item is set programmatically
        /// </summary>
        Programmatic
    }
}