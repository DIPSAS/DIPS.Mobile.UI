using DIPS.Mobile.UI.MVVM;

namespace Playground.VetleSamples;

public class ChipGroupTestViewModel : ViewModel
{
    private readonly VetlePageViewModel m_vetlePageViewModel;
    private List<object> m_selectedItems;

    public ChipGroupTestViewModel(VetlePageViewModel vetlePageViewModel, int selectedIndex)
    {
        m_vetlePageViewModel = vetlePageViewModel;

        SelectedIndex = selectedIndex;
        
        m_selectedItems = [(TestObject2)ItemsSource[SelectedIndex]];
    }
    
    public int SelectedIndex { get; set; }

    public List<object> SelectedItems
    {
        get => m_selectedItems;
        set
        {
            RaiseWhenSet(ref m_selectedItems, value);

            if (value[0] is TestObject2 testObject2)
            {   
                var test = testObject2.Name == "Test 1" || testObject2.Name == "Test 2";
                if (!test)
                {
                    SelectedIndex = ItemsSource.IndexOf(testObject2);
                    m_vetlePageViewModel.Reload();
                }
            }
        }
    }

    public List<TestObject2> ItemsSource { get; } = new List<TestObject2>
    {
        new TestObject2("Test 1"),
        new TestObject2("Test 2"),
        new TestObject2("Test 3"),
        new TestObject2("Test 4"),
        new TestObject2("Test 5"),
    };
}