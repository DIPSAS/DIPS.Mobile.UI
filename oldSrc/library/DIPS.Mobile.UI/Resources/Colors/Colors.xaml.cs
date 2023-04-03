using System.ComponentModel;
using Xamarin.Forms;

namespace DIPS.Mobile.UI.Resources.Colors
{
    public partial class Colors : ResourceDictionary
    {
        public Colors()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Get the color value from a <see cref="ColorName"/>
        /// </summary>
        /// <param name="colorName">The name of the color to get</param>
        /// <returns><see cref="Color"/></returns>
        public static Color GetColor(ColorName colorName) => GetColor(colorName, Application.Current.RequestedTheme);

        /// <summary>
        /// Get the color by <see cref="OSAppTheme"/>.
        /// </summary>
        /// <param name="colorName">The color name to get</param>
        /// <param name="themeToCompare">The <see cref="OSAppTheme"/> to use for comparison</param>
        /// <returns></returns>
        /// <remarks>If there is no corresponding color based on the <see cref="OSAppTheme"/> it returns the opposite color or <see cref="Color.Default"/></remarks>
        public static Color GetColor(ColorName colorName, OSAppTheme themeToCompare)
        {
            var colorToLookup = ColorLookup.GetColorName(colorName, themeToCompare == OSAppTheme.Dark);
            return ColorsExtension.GetColor(colorToLookup);
        }
    }
}