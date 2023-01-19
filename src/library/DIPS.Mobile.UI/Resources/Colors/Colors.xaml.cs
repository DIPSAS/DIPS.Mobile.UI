using System.ComponentModel;
using System.Xml;
using Xamarin.Forms;

namespace DIPS.Mobile.UI.Resources.Colors
{
    [EditorBrowsable(EditorBrowsableState.Never)]
    public partial class Colors
    {
        public static readonly string LightIdentifier = "_light_";
        public static readonly string DarkIdentifier = "_dark_";

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
            var colorToLookup = colorName.ToString();
            if (themeToCompare == OSAppTheme.Dark)
            {
                if (colorToLookup.Contains(LightIdentifier))
                {
                    colorToLookup = colorToLookup.Replace(LightIdentifier, DarkIdentifier);
                }
            }
            else
            {
                if (colorToLookup.Contains(DarkIdentifier))
                {
                    colorToLookup = colorToLookup.Replace(DarkIdentifier, LightIdentifier);
                }
            }

            return ColorsExtension.GetColor(colorToLookup) ?? ColorsExtension.GetColor(colorName) ?? Color.Default;
        }
    }
}