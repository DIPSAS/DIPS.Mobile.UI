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
        if (!ColorResources.Colors.TryGetValue(colorName, out var value))
        {
            return Microsoft.Maui.Graphics.Colors.White;
        }

        return (alpha >= 0) ? value.WithAlpha(alpha) : value;
    }

    public static Color GetColor(ColorName colorName, float alpha = -1) => GetColor(colorName.ToString(), alpha);

    public Color ProvideValue(IServiceProvider serviceProvider) => GetColor(ColorName, Alpha);

    object IMarkupExtension.ProvideValue(IServiceProvider serviceProvider)
    {
        return (this as IMarkupExtension<Color>).ProvideValue(serviceProvider);
    }
}