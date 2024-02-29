using System.Collections;
using System.Collections.ObjectModel;
using System.Windows.Input;
using DIPS.Mobile.UI.MVVM;

namespace Components.ComponentsSamples.Chips;

public class ChipsSamplesViewModel : ViewModel
{
    private List<string> m_selectedItems = ["Ja"];
    private List<object> m_selectedItemsFootballers1 = new List<object>() { new Footballer(){Name = "Odegaard"},new Footballer(){Name = "Haaland"} };

    public ChipsSamplesViewModel()
    {
        
    }
    
    public List<string> SelectedItems
    {
        get => m_selectedItems;
        set => RaiseWhenSet(ref m_selectedItems, value);
    }

    public List<object> SelectedItemsFootballers
    {
        get => m_selectedItemsFootballers1;
        set => RaiseWhenSet(ref m_selectedItemsFootballers1, value);
    }

    public List<Footballer> Footballers => new List<Footballer>() { new Footballer(){Name = "Odegaard"},new Footballer(){Name = "Haaland"}, new Footballer(){Name = "Messi"}};
    public ICommand Test => new Command(Testeren);

    private void Testeren()
    {
        
    }
    
    public class Footballer
    {
        public string Name { get; set; }
    }
}