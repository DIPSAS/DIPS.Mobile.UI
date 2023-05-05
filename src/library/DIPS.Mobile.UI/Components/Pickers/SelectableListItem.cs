namespace DIPS.Mobile.UI.Components.Pickers
{
    public class SelectableListItem
    {
        public SelectableListItem(int index, string displayName, bool isSelected)
        {
            Index = index;
            DisplayName = displayName;
            IsSelected = isSelected;
        }

        public int Index { get; }
        public string DisplayName { get;}
        public bool IsSelected { get; }
    }
}