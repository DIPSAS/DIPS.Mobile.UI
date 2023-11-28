using System.Collections.ObjectModel;
using System.Windows.Input;
using DIPS.Mobile.UI.MVVM;
using Playground.HåvardSamples;

namespace Playground.SanderSamples;

public class SanderPageViewModel : ViewModel
{
    private bool m_isBusy = true; 

    public SanderPageViewModel()
    {
        TestCommand = new Command(() => IsBusy = !IsBusy);
        Initialize();
    }

    private void Initialize()
    {
        Thread.Sleep(10000);
        IsBusy = false;
    }

    public ICommand TestCommand { get; }

    public bool IsBusy
    {
        get => m_isBusy;
        set => RaiseWhenSet(ref m_isBusy, value);
    }

}