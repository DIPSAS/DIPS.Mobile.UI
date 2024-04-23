using System.Windows.Input;
using Components.SampleData;
using DIPS.Mobile.UI.MVVM;

namespace Components.ComponentsSamples.Pickers;

public class ItemPickersSamplesViewModel : ViewModel
{
    private Person m_selectedPerson;
    private List<Person> m_selectedItems = [SampleDataStorage.People[1]];
    private Person m_selectedItem;
    public IEnumerable<Person> People { get; } = SampleDataStorage.People;

    public Person SelectedPerson
    {
        get => m_selectedPerson;
        set => RaiseWhenSet(ref m_selectedPerson, value);
    }

    public List<Person> SelectedItems
    {
        get => m_selectedItems;
        set => RaiseWhenSet(ref m_selectedItems, value);
    }

    public Person SelectedItem
    {
        get => m_selectedItem;
        set => RaiseWhenSet(ref m_selectedItem, value);
    }
}