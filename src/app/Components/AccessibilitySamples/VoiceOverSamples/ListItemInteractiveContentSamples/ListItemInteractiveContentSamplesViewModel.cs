using DIPS.Mobile.UI.MVVM;

namespace Components.AccessibilitySamples.VoiceOverSamples.ListItemInteractiveContentSamples;

public class ListItemInteractiveContentSamplesViewModel : ViewModel
{
    private bool m_isToggled;

    public bool IsToggled
    {
        get => m_isToggled;
        set => RaiseWhenSet(ref m_isToggled, value);
    }
}
