namespace DIPS.Mobile.UI.Components.Pickers.SegmentedControl;

public partial class SegmentedControl
{
    private void SelectedItemChanged()
    {
        if (SelectionMode != SelectionMode.Single) return;

        if (SelectedItem == null)
        {
            return;
        }

        var selectableItem = m_allSelectableItems.FirstOrDefault(s => s.Item.Equals(SelectedItem));
        if (selectableItem is {IsSelected: false})
        {
            selectableItem.IsSelected = true;
        }
    }

    private void SelectItem(SelectableItemViewModel selectableItemViewModel)
    {
        var previousSelectedItem = SelectedItem;
        foreach (var selectableItem in m_allSelectableItems)
        {
            selectableItem.IsSelected = false;
        }

        if (previousSelectedItem is not null && previousSelectedItem.Equals(selectableItemViewModel.Item) && ShouldDeSelectOnSameItemTapped)
        {
            SelectedItem = null;
            SendDidDeSelect(previousSelectedItem);
            return;
        }
        
        selectableItemViewModel.IsSelected = true;
        SelectedItem = selectableItemViewModel.Item;

        if (previousSelectedItem is not null)
        {
            SendDidDeSelect(previousSelectedItem);
        }
        
        SendDidSelect(selectableItemViewModel.Item);
    }

    private void AddItemToSelectableItemsToPickFromSingleMode(
        ICollection<SelectableItemViewModel> listOfSelectableItems, object item)
    {
        listOfSelectableItems.Add(new SelectableItemViewModel(
            item.GetPropertyValue(ItemDisplayProperty) ?? string.Empty,
            item.Equals(SelectedItem), item));
    }
}