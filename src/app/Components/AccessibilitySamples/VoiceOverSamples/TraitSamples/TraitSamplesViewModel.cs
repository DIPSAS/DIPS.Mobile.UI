using DIPS.Mobile.UI.MVVM;
using DIPS.Mobile.UI.Effects.Accessibility;
using System.Windows.Input;

namespace Components.AccessibilitySamples.VoiceOverSamples.TraitSamples;

public class TraitSamplesViewModel : ViewModel
{
    private bool m_isFirstItemSelected;
    private bool m_isSecondItemSelected = true;
    private bool m_isThirdItemSelected;

    public TraitSamplesViewModel()
    {
        ToggleFirstItemCommand = new Command(() => IsFirstItemSelected = !IsFirstItemSelected);
        ToggleSecondItemCommand = new Command(() => IsSecondItemSelected = !IsSecondItemSelected);
        ToggleThirdItemCommand = new Command(() => IsThirdItemSelected = !IsThirdItemSelected);
        ButtonTappedCommand = new Command(() => { /* Demo button */ });
    }

    public bool IsFirstItemSelected
    {
        get => m_isFirstItemSelected;
        set => RaiseWhenSet(ref m_isFirstItemSelected, value);
    }

    public bool IsSecondItemSelected
    {
        get => m_isSecondItemSelected;
        set => RaiseWhenSet(ref m_isSecondItemSelected, value);
    }

    public bool IsThirdItemSelected
    {
        get => m_isThirdItemSelected;
        set => RaiseWhenSet(ref m_isThirdItemSelected, value);
    }

    public ICommand ToggleFirstItemCommand { get; }
    public ICommand ToggleSecondItemCommand { get; }
    public ICommand ToggleThirdItemCommand { get; }
    public ICommand ButtonTappedCommand { get; }
}
