using System.Collections.ObjectModel;
using System.Windows.Input;
using DIPS.Mobile.UI.MVVM;
using Playground.HåvardSamples;

namespace Playground.SanderSamples;

public class SanderPageViewModel : ViewModel
{
    private bool m_isToggled = true;
    private string m_testText;

    public SanderPageViewModel()
    {
        Initialize();
        TestCommand = new Command(() =>
        {
            TestText = IsToggled ? "This is a test" : string.Empty;
            IsToggled = !IsToggled;
        });
    }

    private void Initialize()
    {
    }

    public ICommand TestCommand { get; }

    public bool IsToggled
    {
        get => m_isToggled;
        set => RaiseWhenSet(ref m_isToggled, value);
    }

    public string TestText
    {
        get => m_testText;
        set => RaiseWhenSet(ref m_testText, value);
    }

    public ICommand NavigateCommand { get; }
}