using System.Collections.ObjectModel;
using System.Windows.Input;
using DIPS.Mobile.UI.MVVM;

namespace Playground.VetleSamples.CollectionViewTests;

public class GroupedCollectionViewModel : ViewModel
{
    public GroupedCollectionViewModel()
    {
        GroupedTest = [new GroupedTest(["Test"]), new GroupedTest(["Test2", "Test3", "Test4"])];
    }
    
    public ObservableCollection<GroupedTest> GroupedTest { get; }
    
    public ICommand AddItemCommand => new Command(() =>
    {
        GroupedTest.Add(new GroupedTest(["Test5", "Test6"]));
    });
    
    public ICommand AddItemAtEndInSectionCommand => new Command(() =>
    {
        GroupedTest[0].Add($"Test {GroupedTest[0].Count}");
    });
    
    public ICommand AddItemAtStartInSectionCommand => new Command(() =>
    {
        GroupedTest[0].Insert(0, "Test8");
    });
    
    public ICommand AddItemAtMiddleInSectionCommand => new Command(async () =>
    {
        GroupedTest[0].Clear();
        await Task.Delay(1);
        GroupedTest[0].Add("Test9");
        GroupedTest[0].Add("Test9");
        GroupedTest[0].Add("Test9");
    });
}