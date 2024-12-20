using System.Windows.Input;
using DIPS.Mobile.UI.Components.BottomSheets;
using DIPS.Mobile.UI.MVVM;
using Newtonsoft.Json;

namespace Components.ComponentsSamples.SyntaxHighlighting.Json;

public class SyntaxHighlightingSamplesViewModel : ViewModel
{
    private string m_code = string.Empty;
    private string m_language;

    public SyntaxHighlightingSamplesViewModel()
    {
        var people = SampleData.SampleDataStorage.People;
        Code = JsonConvert.SerializeObject(people);
        Language = "json";
        OpenCodeCommand = new Command(() => _ = BottomSheetService.Open(new CodeBottomSheet(Code, Language)));
    }

    public string Code
    {
        get => m_code;
        set => RaiseWhenSet(ref m_code, value);
    }

    public ICommand OpenCodeCommand { get; }

    public string Language
    {
        get => m_language;
        set => RaiseWhenSet(ref m_language, value);
    }
}