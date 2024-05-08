namespace DIPS.Mobile.UI.Components.Pickers.ScrollPicker.Component;

public interface IScrollPickerComponent
{
    /// <summary>
    /// The currently selected item
    /// </summary>
    /// <remarks>If you have set the <see cref="IsNullable"/> to true, a SelectedIndex of -1 means that it is null</remarks>
    int SelectedIndex { get; }
    
    /// <summary>
    /// Checks whether the scroll picker is nullable or not
    /// </summary>
    bool IsNullable { get; }

    /// <summary>
    /// Sets the default selected index
    /// </summary>
    void SetDefaultIndex(bool isOpening);
    
    /// <summary>
    /// Sets the selected item
    /// </summary>
    void SetSelectedIndex(int index, SetSelectedItemMode setSelectedItemMode = SetSelectedItemMode.Programmatic);
    
    /// <summary>
    /// Gets the number of items in the component
    /// </summary>
    int GetItemsCount();
    
    /// <summary>
    /// Gets the text for the specified item
    /// </summary>
    string GetTextAtIndex(int index);
    
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