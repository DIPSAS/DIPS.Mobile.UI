using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Playground.HåvardSamples;

public partial class HåvardPage2
{
    public HåvardPage2()
    {
        InitializeComponent();
    }

    private void Button_OnClicked(object sender, EventArgs e)
    {
        Shell.Current.Navigation.PushAsync(new HåvardPage3());
    }

    public override async Task<IEnumerable<object>> ProvideSearchResult(string searchQuery, CancellationToken searchCancellationToken)
    {
        await Task.Delay(100);
        return new[] {"Asd"};
    }
}