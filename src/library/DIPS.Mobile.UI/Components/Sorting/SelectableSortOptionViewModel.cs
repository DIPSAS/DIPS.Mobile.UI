using DIPS.Mobile.UI.Components.Pickers;

namespace DIPS.Mobile.UI.Components.Sorting;

public class SelectableSortOptionViewModel : SelectableItemViewModel
{
    public SelectableSortOptionViewModel(string displayName, bool isSelected, object item, bool isLastItem) : base(displayName, isSelected, item)
    {
        IsLastItem = isLastItem;
    }
    
    /// <summary>
    /// Determines if the item is the last item in the list
    /// </summary>
    public bool IsLastItem { get; }
}