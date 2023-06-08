using System.Windows.Input;

namespace Playground.VetleSamples;

public class BottomSheetWithToolbarViewModel
{
    public BottomSheetWithToolbarViewModel()
    {
        TestCommand = new Command(() => Shell.Current.DisplayAlert("woo2", "", "cancel"));
    }
    
    public ICommand TestCommand { get; }

}