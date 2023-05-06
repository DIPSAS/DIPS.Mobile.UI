using Components.Resources.LocalizedStrings;
using Components.SampleData;

namespace Components.ComponentsSamples.Searching;

public partial class SearchPageSamples
{
    public SearchPageSamples()
    {
        InitializeComponent();
    }
    
    public override async Task<IEnumerable<object>> ProvideSearchResult(string searchQuery,
        CancellationToken searchCancellationToken)
    {
        await Task.Delay(1500, searchCancellationToken);
        var people = SampleDataStorage.People;
        return people.Where(p => p.DisplayName.ToLower().Contains(searchQuery.ToLower()))
            .ToList();
    }
}