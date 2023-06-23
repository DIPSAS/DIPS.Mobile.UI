using System.Windows.Input;

namespace Playground.VetleSamples;

public class VetlePageViewModel
{
    public VetlePageViewModel()
    {
        Navigate = new Command(Navigatee);
    }

    private void Navigatee()
    {
        var page = new VetleTestPage1();
        var navigationPage = new NavigationPage(page);
        NavigationPage.SetHasNavigationBar(page, true);
        NavigationPage.SetHasBackButton(page, true);
        Shell.Current.Navigation.PushModalAsync(navigationPage);
    }
    
    public ICommand Navigate { get; }
    public ICommand Test { get; }
}