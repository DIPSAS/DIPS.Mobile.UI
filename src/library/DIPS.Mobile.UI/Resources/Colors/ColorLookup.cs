using System;
using System.Linq;
using Enum = DIPS.Mobile.UI.Extensions.Enum;

namespace DIPS.Mobile.UI.Resources.Colors
{
    internal static class ColorLookup
    {
        public static readonly string DarkIdentifier = "_d";
        public static string GetColorName(ColorName colorName, bool isDarkMode)
        {
            var colorToLookup = colorName.ToString();
            
            if (isDarkMode)
            {
                var (hasDarkModeColor, darkModeColorName) = HasDarkModeColor(colorName);
                if (hasDarkModeColor)
                {
                    return darkModeColorName.ToString()!;
                }
            }
            else
            {
                if (colorToLookup.Contains("_d_")) //Wants to get a light mode color for a dark mode color
                {
                    colorToLookup = colorToLookup.Replace(DarkIdentifier + "_", "_");
                }
            }
            
            return colorToLookup;
        }
        
        public static Tuple<bool, ColorName?> HasDarkModeColor(ColorName colorName)
        {
            var colorToLookup = colorName.ToString();

            if (colorToLookup.Contains(DarkIdentifier + "_")) //The color is already a dark mode color
            {
                return new Tuple<bool, ColorName?>(true, colorName);
            }
            
            var indexOf = colorToLookup.LastIndexOf("_", StringComparison.InvariantCultureIgnoreCase);
            var darkModeColorNameString = colorToLookup.Insert(indexOf,DarkIdentifier);
            
            var allColors = Enum.ToList<ColorName>().Select(c => c.ToString());
            
            if (!allColors.Contains(darkModeColorNameString))
            {
                return new Tuple<bool, ColorName?>(false, null);
            }

            var darkModeColorNameObj = System.Enum.Parse(typeof(ColorName), darkModeColorNameString);
            if (darkModeColorNameObj is ColorName darkModeColorName)
            {
                return new Tuple<bool, ColorName?>(true, darkModeColorName);
            }

            return new Tuple<bool, ColorName?>(false, null);
        }
    }
}