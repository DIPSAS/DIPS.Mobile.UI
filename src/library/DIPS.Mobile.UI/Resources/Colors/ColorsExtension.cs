using DIPS.Mobile.UI.API.Library;

namespace DIPS.Mobile.UI.Resources.Colors;

[AcceptEmptyServiceProvider]
[ContentProperty(nameof(ColorName))]
public class ColorsExtension : IMarkupExtension<Color>
{
    /// <summary>
    /// The <see cref="ColorName"/> to look for.
    /// </summary>
    public ColorName ColorName { get; set; }

    /// <summary>
    /// The alpha value of the color.
    /// </summary>
    public float Alpha { get; set; } = -1; //-1 = not set

    public static Color GetColor(string colorName, float alpha = -1)
    {
        if (DUI.IsExperimentalFeatureEnabled(DUI.ExperimentalFeatures.ForceDarkMode) || Application.Current?.RequestedTheme == AppTheme.Dark)
        {
            colorName += UnifiedColorResources.DarkModeSuffix;
        }
        
        if (UnifiedColorResources.Colors.TryGetValue(colorName, out var color))
        {
            return (alpha >= 0) ? color.WithAlpha(alpha) : color;
        }

        return Microsoft.Maui.Graphics.Colors.White;
    }

    public static Color GetColor(ColorName colorName, float alpha = -1) => GetColor(colorName.ToString(), alpha);

    public Color ProvideValue(IServiceProvider serviceProvider) => GetColor(ColorName, Alpha);

    object IMarkupExtension.ProvideValue(IServiceProvider serviceProvider)
    {
        return (this as IMarkupExtension<Color>).ProvideValue(serviceProvider);
    }
}