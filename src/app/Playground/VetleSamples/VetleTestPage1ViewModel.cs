using System.Windows.Input;
using DIPS.Mobile.UI.Components.Alerting.Dialog;

namespace Playground.VetleSamples;

public class VetleTestPage1ViewModel
{
    public VetleTestPage1ViewModel()
    {
        Navigate = new Command(() => Shell.Current.Navigation.PushAsync(new VetleTestPage2()));
        Test = new Command(() => Console.Write("LKOL"));
    }
    
    public ICommand Navigate { get; }
    
    public ICommand Test { get; }
}