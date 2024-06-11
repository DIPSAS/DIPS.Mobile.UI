using System.Windows.Input;
using DIPS.Mobile.UI.MVVM;

namespace Playground.VetleSamples;

public class BottomSheetWithToolbarViewModel : ViewModel
{
    private bool m_lul;

    public BottomSheetWithToolbarViewModel()
    {
        TestCommand = new Command(() => Shell.Current.DisplayAlert("woo2", "", "cancel"));
        IsVisible = new Command(() => Lul = !Lul);
    }

    public bool Lul
    {
        get => m_lul;
        set => RaiseWhenSet(ref m_lul, value);
    }

    public ICommand TestCommand { get; }
    public ICommand IsVisible { get; }
}