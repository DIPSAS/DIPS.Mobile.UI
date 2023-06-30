using System.Windows.Input;
using DIPS.Mobile.UI.MVVM;

namespace Playground.SanderSamples;

public class SanderPageViewModel : ViewModel
{
    private bool m_isEnabled = true;

    public SanderPageViewModel()
    {
        TestCommand = new Command(() =>
        {
            Console.WriteLine("Tapped");
            IsEnabled = false;
        });
        TestLongPressCommand = new Command(() => Console.WriteLine("Looooong press"));
        SwapTouchIsEnabledCommand = new Command(() => IsEnabled = !IsEnabled);
    }

    public bool IsEnabled
    {
        get => m_isEnabled;
        set => RaiseWhenSet(ref m_isEnabled, value);
    }
    
    public ICommand TestCommand { get; }
    public ICommand TestLongPressCommand { get; }
    public ICommand SwapTouchIsEnabledCommand { get; }
}