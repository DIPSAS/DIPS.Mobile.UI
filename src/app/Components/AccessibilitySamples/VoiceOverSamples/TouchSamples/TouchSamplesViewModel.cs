using DIPS.Mobile.UI.MVVM;
using System.Windows.Input;

namespace Components.AccessibilitySamples.VoiceOverSamples.TouchSamples;

public class TouchSamplesViewModel : ViewModel
{
    private int m_tapCount;
    private string m_tapStatusText = "Card has not been opened.";

    public TouchSamplesViewModel()
    {
        OpenPatientCardCommand = new Command(OpenPatientCard);
    }

    public string PatientName => "Test Patient";

    public string PersonaliaText => "Born 01.01.1970, female";

    public string TapStatusText
    {
        get => m_tapStatusText;
        set => RaiseWhenSet(ref m_tapStatusText, value);
    }

    public ICommand OpenPatientCardCommand { get; }

    private void OpenPatientCard()
    {
        m_tapCount++;
        TapStatusText = $"Card opened {m_tapCount} time(s).";
    }
}