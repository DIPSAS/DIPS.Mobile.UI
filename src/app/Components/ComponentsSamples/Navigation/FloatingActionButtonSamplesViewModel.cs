using System.Windows.Input;

namespace Components.ComponentsSamples.Navigation;

public class FloatingActionButtonSamplesViewModel
{
    public FloatingActionButtonSamplesViewModel()
    {
        NavigateToFloatingNavigationButtonSamples = new Command(() =>
            Shell.Current.Navigation.PushAsync(new FloatingNavigationButtonSamples()));
    }
    
    public ICommand NavigateToFloatingNavigationButtonSamples { get; }
}