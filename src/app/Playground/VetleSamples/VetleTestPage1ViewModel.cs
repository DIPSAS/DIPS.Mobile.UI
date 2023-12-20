using System.Windows.Input;
using DIPS.Mobile.UI.Components.Alerting.Dialog;
using DIPS.Mobile.UI.MVVM;

namespace Playground.VetleSamples;

public class VetleTestPage1ViewModel : ViewModel
{
    private bool m_isSavingCompleted;
    private bool m_isSaving;

    public VetleTestPage1ViewModel()
    {
        Navigate = new Command(() => Shell.Current.Navigation.PushAsync(new VetleTestPage2()));
        Test = new Command(() => Shell.Current.Navigation.PopModalAsync());

        SaveCommand = new Command(async () =>
        {
            IsSaving = true;
            await Task.Delay(2000);
            IsSavingCompleted = true;
            IsSaving = false;
        });
    }
    
    public ICommand Navigate { get; }
    
    public ICommand Test { get; }

    public bool IsSavingCompleted
    {
        get => m_isSavingCompleted;
        set => RaiseWhenSet(ref m_isSavingCompleted, value);
    }

    public bool IsSaving
    {
        get => m_isSaving;
        set => RaiseWhenSet(ref m_isSaving, value);
    }

    public ICommand SaveCommand { get; }
}