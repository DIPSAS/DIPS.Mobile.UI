using System.Windows.Input;
using DIPS.Mobile.UI.API.PictureInPicture;
using DIPS.Mobile.UI.MVVM;

namespace Components.ComponentsSamples.PictureInPicture;

public class PictureInPictureSamplesViewModel : ViewModel
{
    private string m_notes = string.Empty;
    private string m_statusText = string.Empty;

    public PictureInPictureSamplesViewModel()
    {
        EnterPipCommand = new Command(EnterPip);
        PipService.PipModeChanged += OnPipModeChanged;
    }

    private void EnterPip()
    {
        PipService.Enter();
    }

    private void OnPipModeChanged(object? sender, bool isInPipMode)
    {
        StatusText = isInPipMode
            ? "App is in Picture in Picture mode."
            : "App returned from Picture in Picture mode.";
    }

    public void Unsubscribe()
    {
        PipService.PipModeChanged -= OnPipModeChanged;
    }

    public bool IsPipSupported => PipService.IsSupported;

    public string Notes
    {
        get => m_notes;
        set => RaiseWhenSet(ref m_notes, value);
    }

    public string StatusText
    {
        get => m_statusText;
        set
        {
            RaiseWhenSet(ref m_statusText, value);
            RaisePropertyChanged(nameof(HasStatus));
        }
    }

    public bool HasStatus => !string.IsNullOrEmpty(StatusText);

    public ICommand EnterPipCommand { get; }
}
