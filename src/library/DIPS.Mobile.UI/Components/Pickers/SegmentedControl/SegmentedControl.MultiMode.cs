namespace DIPS.Mobile.UI.Components.Pickers.SegmentedControl;

public partial class SegmentedControl
{
    private void OnSelectedItemsChanged()
    {
        if (SelectedItems == null) return;
        
        var itemsToBeSetAsSelected = new List<SelectableItemViewModel>();
        foreach (var selectedItem in SelectedItems)
        {
            var theItem = m_allSelectableItems.FirstOrDefault(s => s.Item.Equals(selectedItem));
            if (theItem != null)
            {
                itemsToBeSetAsSelected.Add(theItem);
            }    
        }
        foreach (var selectableItemViewModel in m_allSelectableItems.Where(s => !itemsToBeSetAsSelected.Contains(s))) //Deselect the ones that are not a part of the items to be set as selected
        {
            selectableItemViewModel.IsSelected = false;
        }
        
        itemsToBeSetAsSelected.ForEach(s => s.IsSelected = true);
    }

    private void ToggleItem(SelectableItemViewModel selectableItemViewModel)
    {
        var wasSelected = !selectableItemViewModel.IsSelected;
        
        var oldSelectedItems = new List<object>();
        if (SelectedItems != null)
        {
            oldSelectedItems.AddRange(SelectedItems.Cast<object?>()!);
        }
        
        var newSelectedItems = new List<object>(oldSelectedItems);
        if (wasSelected)
        {
            newSelectedItems.Add(selectableItemViewModel.Item);    
        }
        else
        {
            var oldItem = newSelectedItems.FirstOrDefault(i => i.Equals(selectableItemViewModel.Item));
            if (oldItem != null)
            {
                newSelectedItems.Remove(oldItem);
            }
        }
        
        SelectedItems = newSelectedItems;
        if (wasSelected)
        {
            SendDidSelect(selectableItemViewModel.Item);
        }
        else
        {
            SendDidDeSelect(selectableItemViewModel.Item);
        }
    }

    private void AddItemToSelectableItemsToPickFromMultiMode(ICollection<SelectableItemViewModel> listOfSelectableItems, object item)
    {
        var selectedItems = (SelectedItems ?? new List<object>()).Cast<object?>();
        listOfSelectableItems.Add(new SelectableItemViewModel(
            item.GetPropertyValue(ItemDisplayProperty) ?? string.Empty,
            selectedItems.Contains(item), item));
    }
}