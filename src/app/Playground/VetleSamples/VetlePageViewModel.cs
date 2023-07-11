using System.Collections.ObjectModel;
using System.Windows.Input;
using DIPS.Mobile.UI.Components.Alerting.Dialog;
using DIPS.Mobile.UI.MVVM;

namespace Playground.VetleSamples;

public class VetlePageViewModel : ViewModel
{
    private bool m_isChecked;
    private bool m_isProgressing = true;
    private bool m_isError;

    public VetlePageViewModel()
    {
        Navigate = new Command(Navigatee);
        Test = new Command(async () =>
        {
            IsProgressing = true;
            await Task.Delay(1000);
            /*IsProgressing = false;
            await Task.Delay(1000);
            IsError = true;*/
            await Task.Delay(1000);
        });

        CompletedCommand = new Command(() => DialogService.ShowMessage("Test", "test", "ok"));

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
    
    public ICommand CompletedCommand { get; }

    public bool IsChecked
    {
        get => m_isChecked;
        set => RaiseWhenSet(ref m_isChecked, value);
    }

    public bool IsProgressing
    {
        get => m_isProgressing;
        set => RaiseWhenSet(ref m_isProgressing, value);
    }

}