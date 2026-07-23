using DIPS.Mobile.UI.API.Library;

namespace DIPS.Mobile.UI.Components.TextFields.Dictation;

/// <summary>Tells whether keyboard dictation is turned on and wired to a speech engine.</summary>
internal static class DictationFeature
{
    public static bool IsAvailable =>
        DUI.IsExperimentalFeatureEnabled(DUI.ExperimentalFeatures.DictationInTextFields)
        && DUI.StartDictationDelegate is not null;
}
