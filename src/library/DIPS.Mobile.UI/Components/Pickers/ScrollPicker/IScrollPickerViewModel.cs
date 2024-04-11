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
    
    void SendSelectedIndexesChanged();

    event Action OnAnySelectedIndexesChanged;
    event Action OnAnyComponentsDataInvalidated;
}