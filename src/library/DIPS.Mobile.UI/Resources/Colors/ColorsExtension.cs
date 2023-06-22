namespace DIPS.Mobile.UI.Resources.Colors
{
    [ContentProperty(nameof(ColorName))]
    public class ColorsExtension : IMarkupExtension<Color>
    {
        public ColorName ColorName { get; set; }

        public static Color GetColor(string colorName)
        {
            if (!ColorResources.Colors.TryGetValue(colorName, out var value))
            {
                return Microsoft.Maui.Graphics.Colors.White;
            }

            return value;
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