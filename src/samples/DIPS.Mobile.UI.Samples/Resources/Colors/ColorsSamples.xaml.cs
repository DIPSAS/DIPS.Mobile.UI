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

        public ColorsSamples()
        {
            InitializeComponent();
            Application.Current.RequestedThemeChanged += (sender, args) =>
            {
                OnPropertyChanged(nameof(OsAppTheme));
                Colors = GetColors();
            };
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            Colors = GetColors();
        }

        private static Dictionary<string, Color> GetColors()
        {
            var colors = new Dictionary<string, Color>();
            var colorNames = Enum.ToList<ColorName>();
            colorNames.ForEach(colorName =>
            {
                var color = UI.Resources.Colors.Colors.GetColor(colorName);
                var name = (Application.Current.RequestedTheme == OSAppTheme.Light)
                    ? colorName.ToString()
                    : colorName.ToString().Replace(UI.Resources.Colors.Colors.LightIdentifier,
                        UI.Resources.Colors.Colors.DarkIdentifier);
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
    }
}