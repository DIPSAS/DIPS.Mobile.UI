using System.Collections.ObjectModel;
using System.Windows.Input;
using DIPS.Mobile.UI.Components.Alerting.Dialog;
using DIPS.Mobile.UI.MVVM;

namespace Playground.VetleSamples.CollectionViewTests;

public class TestItem
{
    public TestItem(string name)
    {
        Name = name;
        Command = new Command(() =>
        {
            DialogService.ShowMessage(config =>
            {
                config.SetDescription("Lol");
            });
        });
    }
    public string Name { get; }
    public ICommand Command { get; }
}

public class RegularCollectionViewModel : ViewModel
{
    private ObservableCollection<TestItem> m_testItems =
    [
        new TestItem("Lokalisasjon påkrevd - Kodeverk og egendefinert"),
        new TestItem("Lokalisasjon - Fritekst"),
        new TestItem("526321"),
        new TestItem("271351"),
        new TestItem("912512"),
        new TestItem("ABC"),
        new TestItem("ÅBE"),
        new TestItem("HAALAND"),
        new TestItem("ØDEGÅÅRD"),
        new TestItem("Testern"),
        new TestItem("Test"),
        new TestItem("ABC"),
        new TestItem("ÅBE"),
        new TestItem("HAALAND"),
        new TestItem("ØDEGÅÅRD"),
        new TestItem("Testern"),
        new TestItem("ABC"),
        new TestItem("ÅBE"),
        new TestItem("HAALAND"),
        new TestItem("ØDEGÅÅRD"),
        new TestItem("Testern"),
    ];

    public ObservableCollection<TestItem> TestItems
    {
        get => m_testItems;
        set => RaiseWhenSet(ref m_testItems, value);
    }

    public ICommand AddItemCommand => new Command(() =>
    {
        TestItems.Add(new TestItem("Nytt item"));
    });

    public ICommand ReplaceCommand => new Command(() =>
    {
        TestItems = new ObservableCollection<TestItem>
        {
            new TestItem("Erstattet item 1"),
            new TestItem("Erstattet item 2"),
            new TestItem("Erstattet item 3"),
            new TestItem("Erstattet item 4"),
            new TestItem("Erstattet item 5")
        };
    });

    public ICommand SortCommand => new Command(() =>
    {
        
    });
}