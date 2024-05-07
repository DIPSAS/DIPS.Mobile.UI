namespace DIPS.Mobile.UI.Components.Pickers.ScrollPicker;

internal interface IScrollPickerViewModel : IDisposable
{
    /// <summary>
    /// The number of components (scroll pickers) in the scroll picker
    /// </summary>
    int GetComponentCount();

    /// <summary>
    /// The number of rows in the specified component
    /// </summary>
    int GetRowsInComponent(int component);
    
    /// <summary>
    /// Get the text for the specified row in the specified component
    /// </summary>
    string GetTextForRowInComponent(int row, int component);
    
    /// <summary>
    /// Called when a row is selected in a component
    /// </summary>
    void SelectedRowInComponent(int row, int component);
    
    /// <summary>
    /// The selected index in the specified component
    /// </summary>
    int SelectedIndexForComponent(int component);
    
    /// <summary>
    /// Sets the default selected index for all components that are not nullable
    /// </summary>
    void SetDefaultSelectedItemsForAllComponents();

    void SendSelectedIndicesChanged();
    
    /// <summary>
    /// Sets all components' selected item to null
    /// </summary>
    void SetToNull();
    
    /// <summary>
    /// Checks if any of the components' implemented TModel is of a nullable type
    /// </summary>
    bool IsNullable { get; }
    
    /// <summary>
    /// Whether the ScrollPickerViewModel is null, if only one component's selected item is null this will be true
    /// </summary>
    bool IsComponentsSelectedItemNull { get; }
    
    event Action OnAnySelectedIndexesChanged;
    event Action OnAnyComponentsDataInvalidated;
}