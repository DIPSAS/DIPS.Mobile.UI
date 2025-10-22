namespace DIPS.Mobile.UI.Components.TextFields.InputFields.MultiLineInputField;

public class StartDictationResult
{
    public CancellationToken CancellationToken { get; set; }

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
