using System.Windows.Input;

namespace Playground.VetleSamples;

public class VetleTestPage1ViewModel
{
    public VetleTestPage1ViewModel()
    {
        Navigate = new Command(() => Shell.Current.Navigation.PushAsync(new VetleTestPage2()));
    }
    
    public ICommand Navigate { get; }
}