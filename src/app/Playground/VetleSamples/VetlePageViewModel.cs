using System.Windows.Input;

namespace Playground.VetleSamples;

public class VetlePageViewModel
{
    public VetlePageViewModel()
    {
        Navigate = new Command(() => Shell.Current.Navigation.PushAsync(new VetleTestPage1()));
    }
    
    public ICommand Navigate { get; }
}