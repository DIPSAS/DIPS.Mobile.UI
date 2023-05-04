using Components.Resources.LocalizedStrings;
using DIPS.Mobile.UI.Resources.Colors;
using Enum = DIPS.Mobile.UI.Extensions.Enum;

namespace Components.ResourcesSamples.Colors
{
    [ResourceSample(nameof(LocalizedStrings.Colors))]
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
                    Colors = GetColors();
                    m_allColors = Colors;
                };
            }
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            Colors = GetColors();
            m_allColors = Colors;
        }

        private static Dictionary<string, Color> GetColors()
        {
            var theColors = new DIPS.Mobile.UI.Resources.Colors.Colors();
            var colors = new Dictionary<string, Color>();
            foreach (var colorPair in theColors)
            {
                if (colorPair.Value is Color color)
                {
                    colors.Add(colorPair.Key, color);
                }
            }
            return colors;
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