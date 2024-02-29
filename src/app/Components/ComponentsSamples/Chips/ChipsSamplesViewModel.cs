using System.Collections;
using System.Collections.ObjectModel;
using DIPS.Mobile.UI.MVVM;

namespace Components.ComponentsSamples.Chips;

public class ChipsSamplesViewModel : ViewModel
{
    private List<string> m_selectedItems = ["Ja"];
    private List<Footballer> m_selectedItemsFootballers1 = new List<Footballer>() { new Footballer(){Name = "Odegaard"},new Footballer(){Name = "Haaland"} };

    public ChipsSamplesViewModel()
    {
        
    }
    
    public List<string> SelectedItems
    {
        get => m_selectedItems;
        set => RaiseWhenSet(ref m_selectedItems, value);
    }

    public List<Footballer> SelectedItemsFootballers
    {
        get => m_selectedItemsFootballers1;
        set => RaiseWhenSet(ref m_selectedItemsFootballers1, value);
    }

    public List<Footballer> Footballers => new List<Footballer>() { new Footballer(){Name = "Odegaard"},new Footballer(){Name = "Haaland"}, new Footballer(){Name = "Messi"}};

    public class Footballer
    {
        public string Name { get; set; }
    }
}