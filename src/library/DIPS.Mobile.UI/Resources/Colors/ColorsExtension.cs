using DIPS.Mobile.UI.Resources.Colors.Deprecated;

namespace DIPS.Mobile.UI.Resources.Colors
{
    [ContentProperty(nameof(ColorName))]
    public class ColorsExtension : IMarkupExtension<Color>
    {
        private Theme.Identifier m_theme;
        private bool m_themeWasSet;
        private StatusColorPalette.Identifier m_statusColorPalette;
        private bool m_statusColorPaletteWasSet;
        private ColorPalette.Identifier m_colorPalette;
        private bool m_colorPaletteWasSet;

        /// <inheritdoc cref="Theme.Identifier"/>
        public Theme.Identifier Theme
        {
            get => m_theme;
            set
            {
                m_theme = value;
                m_themeWasSet = true;
            }
        }

        /// <inheritdoc cref="ColorPalette.Identifier"/>
        public StatusColorPalette.Identifier StatusColorPalette
        {
            get => m_statusColorPalette;
            set
            {
                m_statusColorPalette = value;
                m_statusColorPaletteWasSet = true;
            }
        }

        /// <inheritdoc cref="ColorPalette.Identifier"/>
        public ColorPalette.Identifier ColorPalette
        {
            get => m_colorPalette;
            set
            {
                m_colorPalette = value;
                m_colorPaletteWasSet = true;
            }
        }
        public ColorName ColorName { get; set; }

        public static Color GetColor(string colorName)
        {
            var colors = new Colors();
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