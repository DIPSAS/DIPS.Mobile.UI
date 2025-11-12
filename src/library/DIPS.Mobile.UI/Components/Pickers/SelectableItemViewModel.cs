using DIPS.Mobile.UI.MVVM;
using DIPS.Mobile.UI.Resources.LocalizedStrings.LocalizedStrings;

namespace DIPS.Mobile.UI.Components.Pickers
{
    /// <summary>
    /// A selectable item view model is a very thin view model that can be used when displaying an item to people and when people has to select it.
    /// </summary>
    public class SelectableItemViewModel : ViewModel
    {
        private bool m_isSelected;

        public SelectableItemViewModel(string displayName, bool isSelected, object item)
        {
            DisplayName = displayName;
            IsSelected = isSelected;
            Item = item;
        }
        
        /// <summary>
        /// The name to use when displaying the view model for people.
        /// </summary>
        public string DisplayName { get;}

        /// <summary>
        /// Determines if the item has been selected by people.
        /// </summary>
        public bool IsSelected
        {
            get => m_isSelected;
            set
            {
                if (RaiseWhenSet(ref m_isSelected, value))
                {
                    RaisePropertyChanged(nameof(AccessibilityDescription));
                }
            }
        }

        /// <summary>
        /// The object that holds more information about the item.
        /// </summary>
        public object Item { get; }

        /// <summary>
        /// An accessibility description for the selectable item, that includes the selected state.
        /// </summary>
        public string AccessibilityDescription =>
            string.Join(",", DisplayName, IsSelected ? DUILocalizedStrings.Selected : DUILocalizedStrings.NotSelected) +
            ". " + DUILocalizedStrings.Button;
    }
}