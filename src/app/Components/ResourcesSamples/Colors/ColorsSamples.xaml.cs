using Components.Resources.LocalizedStrings;
using DIPS.Mobile.UI.Resources.Colors;
using Enum = DIPS.Mobile.UI.Extensions.Enum;

namespace Components.ResourcesSamples.Colors
{
    public partial class ColorsSamples
    {
        private Dictionary<string, Color> m_colors;
        private Dictionary<string, Color> m_allColors;

        public ColorsSamples()
        {

            InitializeComponent();
            if (Application.Current != null)
            {
                Application.Current.RequestedThemeChanged += (sender, args) =>
                {
                    OnPropertyChanged(nameof(AppTheme));
                    Colors = ColorResources.Colors;
                    m_allColors = Colors;
                };
            }
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            Colors = ColorResources.Colors;
            m_allColors = Colors;
        }
        
        public Dictionary<string, Color> Colors
        {
            get => m_colors;
            private set
            {
                m_colors = value;
                OnPropertyChanged();
            }
        }

        public AppTheme AppTheme => Application.Current != null ? Application.Current.RequestedTheme : AppTheme.Unspecified;

        private void InputView_OnTextChanged(object sender, TextChangedEventArgs e)
        {
            if (string.IsNullOrEmpty(e.NewTextValue))
            {
                Colors = m_allColors;
            }
            else
            {
                var matchingColors = m_allColors.Where(c => c.Key.ToLower().Contains(e.NewTextValue.ToLower()));
                Colors = matchingColors.ToDictionary(matchingColor => matchingColor.Key,
                    matchingColor => matchingColor.Value);
            }
        }
    }
}