namespace DIPS.Mobile.UI.Components.TextFields.InputFields.MultiLineInputField.Dictation;

/// <summary>
/// Receives recognized text for the field that is currently dictating.
/// </summary>
public interface IDictationConsumerDelegate
{
    /// <summary>
    /// Adds recognized text to the field.
    /// </summary>
    /// <param name="textToAdd">The recognized text to append.</param>
    /// <remarks>
    /// May be called from any thread, because a speech engine can stream results off a background thread.
    /// Implementations must move to the main thread before touching text or platform views.
    /// </remarks>
    void UpdateTextWithDictationResult(string textToAdd);
}