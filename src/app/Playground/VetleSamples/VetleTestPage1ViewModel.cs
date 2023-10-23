using System.Windows.Input;
using DIPS.Mobile.UI.Components.Alerting.Dialog;

namespace Playground.VetleSamples;

public class VetleTestPage1ViewModel
{
    public VetleTestPage1ViewModel()
    {
        Navigate = new Command(() => Shell.Current.Navigation.PushModalAsync(new VetleTestPage2()));
        Test = new Command(() => Shell.Current.Navigation.PopModalAsync());
    }
    
    public ICommand Navigate { get; }
    
    public ICommand Test { get; }
}