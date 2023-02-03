using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace DIPS.Mobile.UI.Resources.Colors
{
    [ContentProperty(nameof(ColorName))]
    public class ColorsExtension : IMarkupExtension<Color>
    {
        public ColorName ColorName { get; set; }

        public static Color GetColor(string colorName)
        {
            var colors = new Colors();
            if (!colors.ContainsKey(colorName))
            {
                return Color.Default;
            }

            if (!colors.TryGetValue(colorName, out var value))
            {
                return Color.Default;
            }

            if (value is Color color)
            {
                return color;
            }

            return Color.Default;
        }

        public static Color GetColor(ColorName colorName)
        {
            return GetColor(colorName.ToString());
        }

        public Color ProvideValue(IServiceProvider serviceProvider)
        {
            return GetColor(ColorName);
        }

        object IMarkupExtension.ProvideValue(IServiceProvider serviceProvider)
        {
            return (this as IMarkupExtension<Color>).ProvideValue(serviceProvider);
        }
    }
}