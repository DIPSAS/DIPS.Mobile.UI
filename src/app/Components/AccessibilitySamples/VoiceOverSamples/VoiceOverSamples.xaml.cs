using System.Windows.Input;

namespace Components.AccessibilitySamples.VoiceOverSamples;

public partial class VoiceOverSamples : DIPS.Mobile.UI.Components.Pages.ContentPage
{
    public VoiceOverSamples()
    {
        InitializeComponent();
        
        NavigateToGroupChildrenCommand = new Command(NavigateToGroupChildren);
        NavigateToExcludeChildrenCommand = new Command(NavigateToExcludeChildren);
    }

    public ICommand NavigateToGroupChildrenCommand { get; }
    public ICommand NavigateToExcludeChildrenCommand { get; }

    private void NavigateToGroupChildren()
    {
        Shell.Current.Navigation.PushAsync(new GroupChildrenSamples());
    }

    private void NavigateToExcludeChildren()
    {
        Shell.Current.Navigation.PushAsync(new ExcludeChildrenSamples());
    }
}