using DIPS.Mobile.UI.MVVM;

namespace DIPS.Mobile.UI.Components.Pickers.ItemPicker
{
    public class SelectableListItem : ViewModel
    {
        private bool m_isSelected;

        public SelectableListItem(string displayName, bool isSelected, object item)
        {
            DisplayName = displayName;
            IsSelected = isSelected;
            Item = item;
        }

        public string DisplayName { get;}

        public bool IsSelected
        {
            get => m_isSelected;
            set => RaiseWhenSet(ref m_isSelected, value);
        }

        public object Item { get; }
    }
}