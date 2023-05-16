namespace DIPS.Mobile.UI.Resources.Icons;

public static class IconLookup
{
    public static ImageSource GetIcon(IconName iconName)
    {
        if (!new Icons().TryGetValue(iconName.ToString(), out var value))
        {
            return string.Empty;
        }

        return value switch
        {
            null => string.Empty,
            string theString => ImageSource.FromFile(theString),
            _ => string.Empty
        };
    }
}