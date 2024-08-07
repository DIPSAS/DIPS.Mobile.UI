using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Playground.HåvardSamples;

public partial class HåvardPage2
{
    private readonly int m_lengthOfResults;

    public HåvardPage2(int lengthOfResults)
    {
        m_lengthOfResults = lengthOfResults;
        InitializeComponent();
    }

    public override async Task<IEnumerable<object>> ProvideSearchResult(string searchQuery, CancellationToken searchCancellationToken)
    {
        await Task.Delay(100);
        var list = new List<string>();
        for (int i = 0; i < m_lengthOfResults; i++)
        {
            list.Add("Asd");
        }
        return list;
    }

    private void HåvardPage2_OnSearchBarFocused(object sender, EventArgs e)
    {
        
    }
}