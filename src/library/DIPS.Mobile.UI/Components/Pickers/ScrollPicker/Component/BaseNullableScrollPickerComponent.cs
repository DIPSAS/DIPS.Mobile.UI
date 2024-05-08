namespace DIPS.Mobile.UI.Components.Pickers.ScrollPicker.Component;

public abstract class BaseNullableScrollPickerComponent<TModel>(Action<object?>? onSelectedItemChanged = null)
    : IScrollPickerComponent
{
    public object? SelectedItem { get; private set; }
    public bool IsNullable => true;
    protected abstract TModel GetDefaultSelectedItem();
    protected abstract bool ShouldDefaultValueOnlyBeSetOnOpen();
    public void SetDefaultSelectedItem(bool isOpening)
    {
        if(ShouldDefaultValueOnlyBeSetOnOpen() && !isOpening)
            return;
        
        SelectedItem = GetDefaultSelectedItem();
    }
    public void SetSelectedItem(int? index, IScrollPickerComponent.SetSelectedItemMode setSelectedItemMode = IScrollPickerComponent.SetSelectedItemMode.Programmatic)
    {
        SelectedItem = index is null ? null : GetItem(index.Value);
        
        if(setSelectedItemMode == IScrollPickerComponent.SetSelectedItemMode.Tapped)
            onSelectedItemChanged?.Invoke(SelectedItem);
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