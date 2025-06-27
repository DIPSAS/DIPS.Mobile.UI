namespace DIPS.Mobile.UI.Resources.Colors;

internal static class UnifiedColorResources
{   
    public static readonly IReadOnlyDictionary<string, Color> Colors;

    static UnifiedColorResources()
    {
        var merged = new Dictionary<string, Color>(ColorResources.Colors);

        // Add keys from ColorResourcesLight, this contains semantic colors
        foreach (var kvp in ColorResourcesLight.Colors)
        {
            if (!merged.ContainsKey(kvp.Key))
            {
                merged[kvp.Key] = kvp.Value;
            }
        }
        
        // Add keys from ColorResourcesDark, this contains semantic colors
        /* TODO: Implement when dark mode is supported
        foreach (var kvp in ColorResourcesDark.Colors)
        {
            if (!merged.ContainsKey(kvp.Key))
            {
                merged[kvp.Key] = kvp.Value;
            }
        }*/

        Colors = merged;
    }
}