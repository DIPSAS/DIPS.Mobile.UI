using System.ComponentModel;
using System.Windows.Input;
using DIPS.Mobile.UI.API.Library;
using DIPS.Mobile.UI.Components.Chips;
using DIPS.Mobile.UI.Components.Navigation.FloatingNavigationButton;

namespace Playground.HåvardSamples;

public partial class HåvardPage
{
    public HåvardPage()
    {
        InitializeComponent();
    }

    public ICommand SearchCommand { get; } = new Command<string>(s =>
    {

    });

    public override async Task<IEnumerable<object>> ProvideSearchResult(string searchQuery, CancellationToken searchCancellationToken)
    {
        await Task.Delay(100, searchCancellationToken);
        
        var result = new List<string>()
        {
            "First",
            "Second",
            "Third",
            "Fourth",
            "Fifth",
        };

        return result;
    }
}