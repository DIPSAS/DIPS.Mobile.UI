namespace DIPS.Mobile.UI.API.Library;

public partial class DUI
{
    [Flags]
    public enum ExperimentalFeatures
    {
        None = 0,
        DictationInTextFields = 1,
        ForceDarkMode = 2
        // Add more experimental features here as needed
    }

    private static ExperimentalFeatures s_enabledExperimentalFeatures = ExperimentalFeatures.None;

    /// <summary>
    /// Check if a specific experimental feature is enabled.
    /// </summary>
    /// <param name="feature">The experimental feature to check.</param>
    /// <returns>True if the feature is enabled, otherwise false.</returns>
    public static bool IsExperimentalFeatureEnabled(ExperimentalFeatures feature)
    {
        return (s_enabledExperimentalFeatures & feature) == feature;
    }

    /// <summary>
    /// Enable a specific experimental feature.
    /// </summary>
    /// <param name="feature">The experimental feature to enable.</param>
    public static void EnableExperimentalFeature(ExperimentalFeatures feature)
    {
        s_enabledExperimentalFeatures |= feature;
    }

    /// <summary>
    /// Disable a specific experimental feature.
    /// </summary>
    /// <param name="feature">The experimental feature to disable.</param>
    public static void DisableExperimentalFeature(ExperimentalFeatures feature)
    {
        s_enabledExperimentalFeatures &= ~feature;
    }
}