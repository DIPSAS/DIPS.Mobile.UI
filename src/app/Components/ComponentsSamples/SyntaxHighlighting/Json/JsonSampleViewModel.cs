using System.Windows.Input;
using DIPS.Mobile.UI.Components.BottomSheets;
using DIPS.Mobile.UI.MVVM;
using Newtonsoft.Json;

namespace Components.ComponentsSamples.SyntaxHighlighting.Json;

public class JsonSampleViewModel : ViewModel
{
    private string m_json = string.Empty;

    public JsonSampleViewModel()
    {
        var people = SampleData.SampleDataStorage.People;
        Json = JsonConvert.SerializeObject(people);
        OpenJsonCommand = new Command(() => BottomSheetService.Open(new JsonBottomSheet(Json)));
    }
    
    public string Json
    {
        get => m_json;
        set => RaiseWhenSet(ref m_json, value);
    }

    public ICommand OpenJsonCommand { get; }
}