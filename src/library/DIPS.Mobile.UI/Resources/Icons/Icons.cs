namespace DIPS.Mobile.UI.Resources.Icons;

public static partial class Icons
{
    /// <summary>
    /// Get the icon value from a <see cref="IconName"/>
    /// </summary>
    /// <param name="iconName">The name of the color to get</param>
    /// <returns><see cref="string"/></returns>
    public static string GetIcon(IconName iconName)
    {
        if (!IconResources.Icons.TryGetValue(iconName.ToString(), out var value))
        {
            return string.Empty;
        }

        return value;
    }
    
    /// <summary>
    /// Get the icon value from a <see cref="IconName"/> as <see cref="ImageSource"/>
    /// </summary>
    /// <param name="iconName">The name of the color to get</param>
    /// <returns></returns>
    public static ImageSource GetIconAsImageSource(IconName iconName)
    {
        return ImageSource.FromFile(GetIcon(iconName));
    }

    public static string GetIconName(IconName iconName) => iconName.ToString();
}