namespace DIPS.Mobile.UI.Components.Pickers.ScrollPicker;

internal class ScrollPickerViewModel : IScrollPickerViewModel
{
    private readonly IReadOnlyList<IScrollPickerComponent>? m_components;

    public ScrollPickerViewModel(IReadOnlyList<IScrollPickerComponent>? components)
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
        
        if(SelectedIndexForComponent(component) == row)
            return;
        
        m_components[component].SetSelectedItem(row, IScrollPickerComponent.SetSelectedItemMode.Tapped);
        OnAnySelectedIndexesChanged?.Invoke();
    }

    public int SelectedIndexForComponent(int component)
    {
        return m_components is null ? 0 : m_components[component].GetSelectedItemIndex() ?? 0;
    }

    public void SetDefaultSelectedIndicesForAllComponents()
    {
        if (m_components == null)
            return;

        foreach (var component in m_components)
        {
            if (component.GetSelectedItemIndex() is not null || component.IsNullable)
                continue;

            component.SetSelectedItem(0);
        }
    }

#if __IOS__
    public void SetDefaultSelectedIndicesWhenOpeningPopover()
    {
        if (m_components == null)
            return;

        foreach (var component in m_components)
        {
            var selectedIndex = component.GetSelectedItemIndex();
            if (selectedIndex is not null)
                continue;

            component.SetDefaultSelectedItem(IScrollPickerComponent.SetSelectedItemMode.Tapped);
        }
    }
#endif

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

    public bool IsComponentsSelectedIndicesNull => m_components?.Any(scrollPickerComponent => scrollPickerComponent.GetSelectedItemIndex() is null) ?? true;

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
    private readonly Action<TModel?>? m_onSelectedItemChanged;
    private readonly TModel? m_defaultSelectedItem;

    public StandardScrollPickerComponent(IList<TModel> items, TModel? selectedItem = default, Action<TModel?>? onSelectedItemChanged = null, bool isNullable = false) : base(isNullable)
    {
        m_items = items;
        m_onSelectedItemChanged = onSelectedItemChanged;
        m_defaultSelectedItem = selectedItem;
    }

    public override void SetDefaultSelectedItem(IScrollPickerComponent.SetSelectedItemMode setSelectedItemMode = IScrollPickerComponent.SetSelectedItemMode.Programmatic)
    {
        SelectedItem = m_defaultSelectedItem ?? m_items[0];
        
        if (setSelectedItemMode == IScrollPickerComponent.SetSelectedItemMode.Tapped)
            m_onSelectedItemChanged?.Invoke(SelectedItem);            
    }

    public override void SetSelectedItem(int? index, IScrollPickerComponent.SetSelectedItemMode setSelectedItemMode = IScrollPickerComponent.SetSelectedItemMode.Programmatic)
    {
        SelectedItem = index is null ? default : m_items[index.Value];
        
        if(setSelectedItemMode == IScrollPickerComponent.SetSelectedItemMode.Tapped)
            m_onSelectedItemChanged?.Invoke(SelectedItem);
    }

    public override int GetItemsCount()
    {
        return m_items.Count;
    }

    public override int? GetSelectedItemIndex()
    {
        return SelectedItem is null ? null : m_items.IndexOf(SelectedItem);
    }

    public override string GetItemText(int index)
    {
        return m_items[index]?.ToString() ?? string.Empty;
    }
}

public abstract class BaseScrollPickerComponent<TModel> : IScrollPickerComponent
{
    public BaseScrollPickerComponent(bool isNullable = false)
    {
        IsNullable = isNullable;
    }
    
    public TModel? SelectedItem { get; protected set; }
    public bool IsNullable { get; }
    public abstract void SetDefaultSelectedItem(IScrollPickerComponent.SetSelectedItemMode setSelectedItemMode = IScrollPickerComponent.SetSelectedItemMode.Programmatic);
    public abstract void SetSelectedItem(int? index, IScrollPickerComponent.SetSelectedItemMode setSelectedItemMode = IScrollPickerComponent.SetSelectedItemMode.Programmatic);
    public abstract int GetItemsCount();
    public abstract int? GetSelectedItemIndex();
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
    /// Checks whether the generic implementation of TModel is nullable
    /// </summary>
    bool IsNullable { get; }

    /// <summary>
    /// Sets the default selected item
    /// </summary>
    void SetDefaultSelectedItem(SetSelectedItemMode setSelectedItemMode = SetSelectedItemMode.Programmatic);
    
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
    int? GetSelectedItemIndex();
    
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