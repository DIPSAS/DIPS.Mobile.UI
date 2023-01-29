using System.Windows.Input;

namespace DIPS.Mobile.UI.Components.Pickers
{
    internal class PickerItem
    {
        public PickerItem(string displayName, bool isSelected, ICommand isSelectedCommand)
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