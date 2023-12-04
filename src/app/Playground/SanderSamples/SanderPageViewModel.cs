using System.Collections.ObjectModel;
using System.Windows.Input;
using DIPS.Mobile.UI.MVVM;
using Playground.HåvardSamples;

namespace Playground.SanderSamples;

public class SanderPageViewModel : ViewModel
{
    private bool m_isToggled = true; 

    public SanderPageViewModel()
    {
        NavigateCommand = new Command(() => Console.WriteLine("tHis is a test"));
        Initialize();
    }

    private void Initialize()
    {
        IsToggled = false;
    }

    public ICommand TestCommand { get; }

    public bool IsToggled
    {
        get => m_isToggled;
        set => RaiseWhenSet(ref m_isToggled, value);
    }

    public ICommand NavigateCommand { get; }
}