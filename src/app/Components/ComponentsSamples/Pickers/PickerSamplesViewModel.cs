using System.Windows.Input;
using Components.SampleData;
using DIPS.Mobile.UI.MVVM;

namespace Components.ComponentsSamples.Pickers;

public class PickerSamplesViewModel : ViewModel
{
    public PickerSamplesViewModel()
    {
        NavigateToItemPickersSamplesCommand =
            new Command(() => Shell.Current.Navigation.PushAsync(new ItemPickersSamples()));
        NavigateToDateTimePickersSamplesCommand = new Command(() =>
            Shell.Current.Navigation.PushAsync(new DateTimePickersSamples()));
    }


    public ICommand NavigateToItemPickersSamplesCommand { get; }
    public ICommand NavigateToDateTimePickersSamplesCommand { get; }
}