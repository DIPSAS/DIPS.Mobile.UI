using System.Collections.ObjectModel;
using System.Windows.Input;
using DIPS.Mobile.UI.MVVM;

namespace Playground.VetleSamples.CollectionViewTests;

public class RegularCollectionViewModel2 : ViewModel
{
    public ObservableCollection<string> TestStrings { get; set; } =
    [
        "Lokalisasjon påkrevd - Kodeverk og egendefinert",
        /*"Lokalisasjon - Fritekst",
        "526321",
        "271351",
        "912512",
        "ABC",
        "ÅBE",
        "HAALAND",
        "ØDEGÅÅRD",
        "Testern",
        "Test",
        "ABC",
        "ÅBE",
        "HAALAND",
        "ØDEGÅÅRD",
        "Testern",
        "ABC",
        "ÅBE",
        "HAALAND",
        "ØDEGÅÅRD",
        "Testern",*/
    ];

    public ICommand AddItemCommand => new Command(() =>
    {
        TestStrings.Add("Nytt item");
    });
}