namespace DIPS.Mobile.UI.Components.Pickers.ItemPicker
{
    public class SelectableItem
    {
        public SelectableItem(string displayName, bool isSelected)
        {
            DisplayName = displayName;
            IsSelected = isSelected;
        }

        public string DisplayName { get;}
        public bool IsSelected { get; }
    }
}