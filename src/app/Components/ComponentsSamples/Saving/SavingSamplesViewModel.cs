using System.Windows.Input;
using DIPS.Mobile.UI.MVVM;

namespace Components.ComponentsSamples.Saving;

public class SavingSamplesViewModel : ViewModel
{
    private bool m_isChecked;
    private bool m_isProgressing;

    public SavingSamplesViewModel()
    {
        SaveCommand = new Command(async () =>
        {
            IsProgressing = true;
            await Task.Delay(1500);
            IsChecked = !IsChecked;
        });
        CompletedCommand = new Command(async () =>
        {
            await Task.Delay(1000);
            Shell.Current.Navigation.PopAsync();
        });
    }

    public bool IsChecked
    {
        get => m_isChecked;
        private set => RaiseWhenSet(ref m_isChecked, value);
    }

    public bool IsProgressing
    {
        get => m_isProgressing;
        set => RaiseWhenSet(ref m_isProgressing, value);
    }

    public ICommand SaveCommand { get; }
    public ICommand CompletedCommand { get; }
}