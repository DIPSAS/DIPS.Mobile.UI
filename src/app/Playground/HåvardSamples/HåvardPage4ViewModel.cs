using System.Windows.Input;
using DIPS.Mobile.UI.MVVM;

namespace Playground.HåvardSamples;

public class HåvardPage4ViewModel : ViewModel
{
    private bool m_isDeleteEnabled = true;
    private bool m_isContextMenuEnabled = true;

    public HåvardPage4ViewModel()
    {
        ToggleDeleteEnabledCommand = new Command(() => IsDeleteEnabled = !IsDeleteEnabled);
        ToggleContextMenuEnabledCommand = new Command(() => IsContextMenuEnabled = !IsContextMenuEnabled);
        ItemTappedCommand = new Command<string>(title => Console.WriteLine($"Tapped: {title}"));
    }

    public bool IsDeleteEnabled
    {
        get => m_isDeleteEnabled;
        set => RaiseWhenSet(ref m_isDeleteEnabled, value);
    }

    public bool IsContextMenuEnabled
    {
        get => m_isContextMenuEnabled;
        set => RaiseWhenSet(ref m_isContextMenuEnabled, value);
    }

    public ICommand ToggleDeleteEnabledCommand { get; }
    public ICommand ToggleContextMenuEnabledCommand { get; }
    public ICommand ItemTappedCommand { get; }
}
