using System.Windows.Input;
using Components.ComponentsSamples.Navigation;

namespace Components.ComponentsSamples.FloatingActionButtons;

public class FloatingActionButtonSamplesViewModel
{
    public FloatingActionButtonSamplesViewModel()
    {
        NavigateToFloatingNavigationButtonSamples = new Command(() =>
            Shell.Current.Navigation.PushAsync(new FloatingNavigationButtonSamples()));
    }
    
    public ICommand NavigateToFloatingNavigationButtonSamples { get; }
}