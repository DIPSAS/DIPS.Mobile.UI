namespace DIPS.Mobile.UI.Components.Pickers.ScrollPicker.Component;

public abstract class BaseScrollPickerComponent(Action<int>? onSelectedIndexChanged = null)
    : IScrollPickerComponent
{
    public abstract string GetTextAtIndex(int index);
    public abstract int GetItemsCount();
    
    /// <summary>
    /// Gets the default index for the picker.
    /// </summary>
    /// <returns></returns>
    protected abstract int GetDefaultIndex();
    
    /// <summary>
    /// Determines whether the picker should be nullable.
    /// </summary>
    /// <returns></returns>
    protected abstract bool ShouldBeNullable();
    
    /// <summary>
    /// Determines whether the default value should only be shown when the picker is opened.
    /// </summary>
    /// <returns></returns>
    protected abstract bool ShouldDefaultValueOnlyBeSetOnOpen();
    
    public void SetDefaultIndex(bool isOpening)
    {
        if(IsNullable && ShouldDefaultValueOnlyBeSetOnOpen() && !isOpening)
            return;
        
        SelectedIndex = GetDefaultIndex();
    }

    public void SetSelectedIndex(int index, IScrollPickerComponent.SetSelectedItemMode setSelectedItemMode = IScrollPickerComponent.SetSelectedItemMode.Programmatic)
    {
        SelectedIndex = index;
        
        if(setSelectedItemMode == IScrollPickerComponent.SetSelectedItemMode.Tapped)
            onSelectedIndexChanged?.Invoke(SelectedIndex);
    }
    
    public void InvalidateData()
    {
        OnDataInvalidated?.Invoke();
    }

    public int SelectedIndex { get; private set; } = -1;
    public bool IsNullable => ShouldBeNullable();
    public event Action? OnDataInvalidated;
}