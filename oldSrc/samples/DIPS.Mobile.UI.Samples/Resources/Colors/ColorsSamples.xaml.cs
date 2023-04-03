using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using DIPS.Mobile.UI.Extensions;
using DIPS.Mobile.UI.Resources.Colors;
using Xamarin.Forms;
using Xamarin.Forms.Internals;
using Xamarin.Forms.Xaml;
using Enum = DIPS.Mobile.UI.Extensions.Enum;

namespace DIPS.Mobile.UI.Samples.Resources.Colors
{
    [ResourceSample("Colors")]
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ColorsSamples
    {
        private Dictionary<string, Color> m_colors;
        private Dictionary<string, Color> m_allColors;

        public ColorsSamples()
        {
            InitializeComponent();
            Application.Current.RequestedThemeChanged += (sender, args) =>
            {
                OnPropertyChanged(nameof(OsAppTheme));
                Colors = GetColors();
                m_allColors = Colors;
            };
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            Colors = GetColors();
            m_allColors = Colors;
        }

        private static Dictionary<string, Color> GetColors()
        {
            var colors = new Dictionary<string, Color>();
            var colorNames = Enum.ToList<ColorName>();
            colorNames.ForEach(colorName =>
            {
                var color = UI.Resources.Colors.Colors.GetColor(colorName);
                var name = colorName.ToString();
                colors.Add(name, color);
            });
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

        public OSAppTheme OsAppTheme
        {
            get => Application.Current.RequestedTheme;
        }

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