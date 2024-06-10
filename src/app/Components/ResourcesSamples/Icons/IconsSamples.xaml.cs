using System.Windows.Input;
using Components.ComponentsSamples.BottomSheets;
using DIPS.Mobile.UI.Components.BottomSheets;
using DIPS.Mobile.UI.Resources.Icons;

namespace Components.ResourcesSamples.Icons
{
    public partial class IconsSamples
    {
        private Dictionary<string, string> m_icons;
        private Dictionary<string, string> m_allIcons;

        public ICommand OpenIconCommand => new Command<string>(iconName =>
        {
            BottomSheetService.Open(new IconBottomSheet(iconName));
        });

        public IconsSamples()
        {
            InitializeComponent();
            Icons = IconResources.Icons;
            m_allIcons = Icons;
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            Icons = IconResources.Icons;
            m_allIcons = Icons;
        }

        public Dictionary<string, string> Icons
        {
            get => m_icons;
            private set
            {
                m_icons = value;
                OnPropertyChanged();
            }
        }


        private void InputView_OnTextChanged(object sender, TextChangedEventArgs e)
        {
            if (string.IsNullOrEmpty(e.NewTextValue))
            {
                Icons = m_allIcons;
            }
            else
            {
                var matchingIcons = m_allIcons.Where(c => c.Key.ToLower().Contains(e.NewTextValue.ToLower()));
                Icons = matchingIcons.ToDictionary(icon => icon.Key,
                    matchingIcon => matchingIcon.Value);
            }
        }
    }
}