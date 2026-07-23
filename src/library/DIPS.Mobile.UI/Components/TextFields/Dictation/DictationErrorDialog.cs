using DIPS.Mobile.UI.Components.Alerting.Dialog;
using DIPS.Mobile.UI.Resources.LocalizedStrings.LocalizedStrings;

namespace DIPS.Mobile.UI.Components.TextFields.Dictation;

/// <summary>Shows the person a message when dictation could not start.</summary>
internal static class DictationErrorDialog
{
    public static void Show(string errorMessage)
    {
        if (string.IsNullOrWhiteSpace(errorMessage))
            return;

        _ = DialogService.ShowMessage(dialog => dialog
            .SetTitle(DUILocalizedStrings.Dictation)
            .SetDescription(errorMessage)
            .SetActionTitle(DUILocalizedStrings.Ok));
    }
}
