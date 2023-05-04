using Components.Resources.LocalizedStrings;

namespace Components.ComponentsSamples.Searching;

[ComponentSample(nameof(LocalizedStrings.SearchBar))]
public partial class SearchBarSamples
{
    public SearchBarSamples()
    {
        InitializeComponent();
    }
    
    private void InputView_OnTextChanged(object sender, TextChangedEventArgs e)
    {
        if (BindingContext is SearchBarSamplesViewModel searchBarSamplesViewModel)
        {
            searchBarSamplesViewModel.FilterItems(e.NewTextValue);
        }
    }
}