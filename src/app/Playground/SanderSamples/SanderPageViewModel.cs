using System.Collections.ObjectModel;
using System.Windows.Input;
using DIPS.Mobile.UI.MVVM;
using Playground.HåvardSamples;

namespace Playground.SanderSamples;

public class SanderPageViewModel : ViewModel
{
    private bool m_isToggled;
    private bool m_isClosed;

    public SanderPageViewModel()
    {
        // TestCommand = new Command(() => IsToggled = !IsToggled);
        CloseCommand = new Command(() => IsClosed = true);
    }

    public bool IsClosed
    {
        get => m_isClosed;
        set => RaiseWhenSet(ref m_isClosed, value);
    }

    public ICommand TestCommand { get; }

    public bool IsToggled
    {
        get => m_isToggled;
        set => RaiseWhenSet(ref m_isToggled, value);
    }

    public ICommand CloseCommand { get; }
}