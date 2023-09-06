using System.Windows.Input;
using DIPS.Mobile.UI.Components.Sorting;
using DIPS.Mobile.UI.MVVM;

namespace Components.ComponentsSamples.Sorting;

public class SortingSamplesViewModel : ViewModel
{
    public SortingSamplesViewModel()
    {
        SortingDoneCommand = new Command<(object, SortOrder)>(SortList);
        
        SortList((InitialSelectedItem, InitialSortOrder));
    }
    
    public ICommand SortingDoneCommand { get; }
    
    private void SortList((object, SortOrder) sortResult)
    {
        var oldTestStrings = new List<string>(TestStrings);
        
        oldTestStrings.Sort(new SortOptionComparer(sortResult.Item1 as SortOption, sortResult.Item2));

        TestStrings = oldTestStrings;
        RaisePropertyChanged(nameof(TestStrings));
    }
    
    public List<SortOption> SortOptions { get; } = new()
    {
        new SortOption("Tall", "Number"),
        new SortOption("Ord", "Words"),
    };

    public SortOption InitialSelectedItem => SortOptions[1];
    
    public List<string> TestStrings { get; set; } = new()
    {
        "Bil",
        "123",
        "Hund",
        "Kaffe",
        "2022",
        "Sykkel",
        "987",
        "Solsikke",
        "Elefant",
        "555",
    };

    public SortOrder InitialSortOrder => SortOrder.Descending;
}

class SortOptionComparer : IComparer<string>
{
    private readonly SortOption m_sortOption;
    private readonly SortOrder m_sortOrder;

    public SortOptionComparer(SortOption sortOption, SortOrder sortOrder)
    {
        m_sortOption = sortOption;
        m_sortOrder = sortOrder;
    }
    
    public int Compare(string x, string y)
    {
        if (m_sortOption.Identifier == "Number")
        {
            var xIsNumber = int.TryParse(x, out var number1);
            var yIsNumber = int.TryParse(y, out var number2);

            var returnValue = 0;
            
            if (xIsNumber && yIsNumber)
            {
                returnValue = number1.CompareTo(number2);
            }

            if (xIsNumber && !yIsNumber)
                returnValue = 1;

            if (!xIsNumber && !yIsNumber)
                returnValue = -1;

            return m_sortOrder == SortOrder.Ascending ? returnValue : -returnValue;
        }
        else
        {
            var xIsNumber = int.TryParse(x, out var number1);
            var yIsNumber = int.TryParse(y, out var number2);

            var returnValue = 0;
            
            if (xIsNumber && yIsNumber)
                returnValue = -1;

            if (!xIsNumber && yIsNumber)
                returnValue = 1;

            if (!xIsNumber && !yIsNumber)
                returnValue = String.Compare(x, y, StringComparison.OrdinalIgnoreCase);
            
            return m_sortOrder == SortOrder.Ascending ? returnValue : -returnValue;
        }
    }
}

public class SortOption
{
    public SortOption(string text, string identifier)
    {
        Text = text;
        Identifier = identifier;
    }
    
    public string Text { get; }
    public string Identifier { get; }
}