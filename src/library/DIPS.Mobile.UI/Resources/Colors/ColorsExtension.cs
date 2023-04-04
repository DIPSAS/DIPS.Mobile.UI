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
                return Microsoft.Maui.Graphics.Colors.White;
            }

            if (!colors.TryGetValue(colorName, out var value))
            {
                return Microsoft.Maui.Graphics.Colors.White;
            }

            if (value is Color color)
            {
                return color;
            }

            return Microsoft.Maui.Graphics.Colors.White;
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