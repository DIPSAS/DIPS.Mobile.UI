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

        selectableItemViewModel.IsSelected = true;
        SelectedItem = selectableItemViewModel.Item;

        if (selectableItemViewModel.IsSelected)
        {
            SendDidSelect(selectableItemViewModel.Item);
            if (previousSelectedItem != null)
            {
                SendDidDeSelect(previousSelectedItem);
            }
        }
        else
        {
            SendDidDeSelect(selectableItemViewModel.Item);
            if (previousSelectedItem != null)
            {
                SendDidSelect(previousSelectedItem);
            }
        }
    }

    private void AddItemToSelectableItemsToPickFromSingleMode(
        ICollection<SelectableItemViewModel> listOfSelectableItems, object item)
    {
        listOfSelectableItems.Add(new SelectableItemViewModel(
            item.GetPropertyValue(ItemDisplayProperty) ?? string.Empty,
            item.Equals(SelectedItem), item));
    }
}