using System.Windows.Input;
using DIPS.Mobile.UI.MVVM;

namespace Components.ComponentsSamples.ListItems;

public class ListItemsSamplesViewModel : ViewModel
{
    private bool m_isBusy = true;
    private bool m_isError;


    public ListItemsSamplesViewModel()
    {
        RefreshCommand = new Command(async () =>
        {
            IsBusy = true;
            IsError = false;
            await Task.Delay(1500);
            IsBusy = false;
        });

        Test = new Command(() => {});

        _ = Initialize();
    }

    private async Task Initialize()
    {
        await Task.Delay(5000);
        IsError = true;
        IsBusy = false;
    }
    
    public bool IsBusy
    {
        get => m_isBusy;
        set => RaiseWhenSet(ref m_isBusy, value);
    }

    public bool IsError
    {
        get => m_isError;
        set => RaiseWhenSet(ref m_isError, value);
    }
    
    public ICommand RefreshCommand { get; }
    public ICommand Test { get; }
}