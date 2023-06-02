using System.Windows.Input;
using Components.ComponentsSamples.Loading.Skeleton;

namespace Components.ComponentsSamples.Loading;

public class LoadingSamplesPageViewModel
{
    public LoadingSamplesPageViewModel()
    {
        NavigateToSkeletonLoadingCommand = new Command((() => Shell.Current.Navigation.PushAsync(new SkeletonLoadingSamplesPage())));
    }
    public ICommand NavigateToSkeletonLoadingCommand { get; } 
}