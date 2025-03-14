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
}