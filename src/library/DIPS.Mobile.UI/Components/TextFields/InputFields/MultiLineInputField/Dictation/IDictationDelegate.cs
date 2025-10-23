namespace DIPS.Mobile.UI.Components.TextFields.InputFields.MultiLineInputField.Dictation;

public interface IDictationConsumerDelegate
{
    void UpdateTextWithDictationResult(string textToAdd);
}