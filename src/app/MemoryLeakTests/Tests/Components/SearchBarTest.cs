using SearchBar = DIPS.Mobile.UI.Components.Searching.SearchBar;

namespace MemoryLeakTests.Tests;

public class SearchBarTest : UITest
{
    public override void BeforeTest(ContentPage contentPage)
    {
        contentPage.Content = new SearchBar { Placeholder = "Search" };
    }

    public override string Name => "SearchBar";
}
