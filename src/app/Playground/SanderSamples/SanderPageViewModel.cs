using System.Collections.ObjectModel;
using System.Windows.Input;
using DIPS.Mobile.UI.MVVM;
using Playground.HåvardSamples;

namespace Playground.SanderSamples;

public class SanderPageViewModel
{
    // private bool m_isToggled = true;
    // private string m_testText;
    // private SanderViewModel m_viewModel;

    public SanderPageViewModel()
    {
         // DeviceDisplay.MainDisplayInfoChanged += DeviceDisplayOnMainDisplayInfoChanged;
        // Initialize();
        // TestCommand = new Command(() =>
        // {
        //     ViewModel = new ();
        // });
    }

    private void DeviceDisplayOnMainDisplayInfoChanged(object sender, DisplayInfoChangedEventArgs e)
    {
        
    }

    private void Initialize()
    {
        // ViewModel = new();
    }

    // public ICommand TestCommand { get; }
    // public SanderViewModel ViewModel
    // {
    //     get => m_viewModel;
    //     set => RaiseWhenSet(ref m_viewModel, value);
    // }
}

public class SanderViewModel : ViewModel
{
    private List<object> m_selectedItem;

    public SanderViewModel()
    {
        
    }
    
    public List<string> List => ["Tid", "Runde"];

    public List<object> SelectedItem
    {
        get => m_selectedItem;
        set => RaiseWhenSet(ref m_selectedItem, value);
    }
}