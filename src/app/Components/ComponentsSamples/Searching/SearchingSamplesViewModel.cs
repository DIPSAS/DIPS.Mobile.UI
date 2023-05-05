using System.Windows.Input;

namespace Components.ComponentsSamples.Searching;

public class SearchingSamplesViewModel
{
    public SearchingSamplesViewModel()
    {
        NavigateToSearchBar = new Command(() => Shell.Current.Navigation.PushAsync(new SearchBarSamples()));
        NavigateToSearchPage = new Command(() => Shell.Current.Navigation.PushAsync(new SearchPageSamples()));
    }
    
    public ICommand NavigateToSearchBar { get; }
    
    public ICommand NavigateToSearchPage { get; }
}