using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using DIPS.Mobile.UI.Samples.SampleData;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace DIPS.Mobile.UI.Samples.Components.Searching
{
    [ComponentSample("SearchPage")]
    [XamlCompilation(XamlCompilationOptions.Compile)]
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
}