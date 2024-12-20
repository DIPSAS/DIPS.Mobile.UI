using System.Windows.Input;
using DIPS.Mobile.UI.MVVM;

namespace Components.ComponentsSamples.SyntaxHighlighting;

public class CodeBottomSheetViewModel : ViewModel
{
    public CodeBottomSheetViewModel(string code, string language)
    {
        Language = language;
        Code = code;
        CopyCodeCommand = new Command(() => Clipboard.SetTextAsync(Code));
    }
    
    private string m_code;
    private string m_language;

    public string Code
    {
        get => m_code;
        set => RaiseWhenSet(ref m_code, value);
    }

    public string Language
    {
        get => m_language;
        set => RaiseWhenSet(ref m_language, value);
    }

    public ICommand CopyCodeCommand { get; }
}