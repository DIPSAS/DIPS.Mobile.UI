namespace DIPS.Mobile.UI.Components.Pickers.ScrollPicker.Component;

public abstract class BaseScrollPickerComponent<TModel>(Action<object>? onSelectedItemChanged = null)
    : IScrollPickerComponent
{
    public object? SelectedItem { get; private set; }
    public bool IsNullable => false;
    protected abstract TModel GetDefaultSelectedItem();
    public void SetDefaultSelectedItem(bool isOpening)
    {
        SelectedItem = GetDefaultSelectedItem();
    }

    public void SetSelectedItem(int? index, IScrollPickerComponent.SetSelectedItemMode setSelectedItemMode = IScrollPickerComponent.SetSelectedItemMode.Programmatic)
    {
        SelectedItem = GetItem(index!.Value);
        
        if(setSelectedItemMode == IScrollPickerComponent.SetSelectedItemMode.Tapped)
            onSelectedItemChanged?.Invoke(SelectedItem!);
    }

    public int GetSelectedItemIndex()
    {
        return IndexOfSelectedItem((TModel)SelectedItem!);
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