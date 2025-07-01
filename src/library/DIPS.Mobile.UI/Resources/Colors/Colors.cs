namespace DIPS.Mobile.UI.Resources.Colors;

public static class Colors
{
    /// <summary>
    /// Get the color value from a <see cref="ColorName"/>
    /// </summary>
    /// <param name="colorName">The name of the color to get</param>
    /// <param name="alpha">The color alpha</param>
    /// <returns><see cref="Color"/></returns>
    public static Color GetColor(ColorName colorName, float alpha = -1) => GetColor(colorName, (Application.Current != null)?Application.Current.RequestedTheme:AppTheme.Light, alpha);

    /// <summary>
    /// Get the color by <see cref="AppTheme"/>.
    /// </summary>
    /// <param name="colorName">The color name to get</param>
    /// <param name="themeToCompare">The <see cref="AppTheme"/> to use for comparison</param>
    /// <param name="alpha">The color alpha</param>
    /// <returns></returns>
    /// <remarks>If there is no corresponding color based on the <see cref="AppTheme"/> it returns the opposite color or <see cref="Color.Default"/></remarks>
    public static Color GetColor(ColorName colorName, AppTheme themeToCompare, float alpha = -1)
    {
        var colorToLookup = ColorLookup.GetColorName(colorName, themeToCompare == AppTheme.Dark);
        return ColorsExtension.GetColor(colorToLookup, alpha);
    }
}