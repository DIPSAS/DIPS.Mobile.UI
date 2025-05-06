using DIPS.Mobile.UI.Components.Shell;

namespace DIPS.Mobile.UI.API.TabBadge;

/// <summary>
/// Use this service to set badge counts and colors for tabs.
/// <remarks>The app must use DUI's <see cref="Components.Shell.Shell"/></remarks>
/// </summary>
public static class TabBadgeService
{
    internal static event Action? OnBadgeCountChanged;
    internal static event Action? OnBadgeColorChanged;
    
    internal static readonly Dictionary<int, Color> s_badgeColors = new();
    internal static readonly Dictionary<int, string?> s_badgeCounts = new();

    /// <summary>
    /// Sets the badge count for a specific tab.
    /// <remarks>Setting badge count to equal or less than 0 will remove the badge</remarks>
    /// </summary>
    /// <param name="tabIndex">The tab index</param>
    /// <param name="count">The number that shall be indicated in the badge</param>
    public static void SetCount(int tabIndex, int count)
    {
        s_badgeCounts[tabIndex] = FormatBadgeValue(count);
        OnBadgeCountChanged?.Invoke();
    }

    /// <summary>
    /// Sets the color for a specific tab.
    /// <remarks>Will use OS default if color is not set</remarks>
    /// </summary>
    /// <param name="tabIndex">The tab index</param>
    /// <param name="color">The color of the badge</param>
    public static void SetColor(int tabIndex, Color color)
    {
        s_badgeColors[tabIndex] = color;
        OnBadgeColorChanged?.Invoke();
    }
    
    private static string? FormatBadgeValue(int count)
    {
        if (count <= 0)
            return null;

        if (count > 99)
            return "99+";

        return count.ToString();
    }
}