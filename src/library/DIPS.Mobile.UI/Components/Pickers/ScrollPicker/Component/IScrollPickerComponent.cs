namespace DIPS.Mobile.UI.Components.Pickers.ScrollPicker.Component;

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
    /// <param name="isOpening"></param>
    void SetDefaultSelectedItem(bool isOpening);
    
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