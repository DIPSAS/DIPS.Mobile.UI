namespace DIPS.Mobile.UI.Components.TextFields.InputFields.MultiLineInputField.Dictation;

// TODO: Add more explicit error handling in dictation button implementation.
public class StartDictationResult
{
    public string ErrorMessage = string.Empty;

    public bool IsError => !string.IsNullOrWhiteSpace(ErrorMessage);

    public StartDictationResult()
    {
    }
    
    public StartDictationResult(string errorMessage)
    {
        ErrorMessage = errorMessage;
    }    
}
