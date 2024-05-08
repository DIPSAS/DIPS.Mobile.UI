using DIPS.Mobile.UI.Components.Pickers.ScrollPicker.Component;

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

    public void SetDefaultSelectedItemsForAllComponents(bool isOpening = false)
    {
        if (m_components == null)
            return;

        foreach (var component in m_components)
        {
            if (component.SelectedItem is not null)
                continue;
            
            component.SetDefaultSelectedItem(isOpening);
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