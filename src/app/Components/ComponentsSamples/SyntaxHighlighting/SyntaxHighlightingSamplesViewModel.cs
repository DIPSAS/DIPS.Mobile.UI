using System.Windows.Input;
using Components.ComponentsSamples.SyntaxHighlighting.Json;

namespace Components.ComponentsSamples.SyntaxHighlighting;

public class SyntaxHighlightingSamplesViewModel
{
    public SyntaxHighlightingSamplesViewModel()
    {
        NavigateToJsonSampleCommand = new Command(() => Shell.Current.Navigation.PushAsync(new JsonSample()));
    }
    
    public ICommand NavigateToJsonSampleCommand { get; }
}