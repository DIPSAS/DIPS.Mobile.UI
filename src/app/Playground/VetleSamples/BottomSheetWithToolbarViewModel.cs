using System.Windows.Input;
using DIPS.Mobile.UI.Components.Alerting.Dialog;
using DIPS.Mobile.UI.MVVM;

namespace Playground.VetleSamples;

public class BottomSheetWithToolbarViewModel : ViewModel
{
    private bool m_isVisible = true;
    private bool m_isEnabled = true;

    public BottomSheetWithToolbarViewModel()
    {
        TestCommand = new Command(() => IsVisible = !IsVisible);
        TestCommand2 = new Command(() => IsEnabled = !m_isEnabled);
        Test3 = new Command(() => { DialogService.ShowMessage(config => config.SetTitle("lol")); });
        CloseCommand = new Command<Action>(Close);
    }
    
    public ICommand TestCommand { get; }

    public ICommand TestCommand2 { get; }
    
    public bool IsVisible
    {
        get => m_isVisible;
        set => RaiseWhenSet(ref m_isVisible, value);
    }

    public bool IsEnabled
    {
        get => m_isEnabled;
        set => RaiseWhenSet(ref m_isEnabled, value);
    }

    public ICommand Test3 { get; }
    public ICommand CloseCommand { get; }

    private void Close(Action action)
    {
        action.Invoke();
    }
}