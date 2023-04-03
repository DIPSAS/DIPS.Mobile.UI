using System.Windows.Input;

namespace DIPS.Mobile.UI.Components.Pickers
{
    public class SelectableItem
    {
        public SelectableItem(string displayName, bool isSelected, ICommand isSelectedCommand)
        {
            DisplayName = displayName;
            IsSelected = isSelected;
            IsSelectedCommand = isSelectedCommand;
        }

        public ICommand IsSelectedCommand { get;}
        public string DisplayName { get;}
        public bool IsSelected { get; }
    }
}