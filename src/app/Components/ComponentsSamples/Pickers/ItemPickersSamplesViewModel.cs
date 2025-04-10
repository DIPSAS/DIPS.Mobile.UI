using Components.SampleData;
using DIPS.Mobile.UI.MVVM;

namespace Components.ComponentsSamples.Pickers;

public class ItemPickersSamplesViewModel : ViewModel
{
    private Person m_selectedPerson;
    private List<Person> m_selectedItems = [SampleDataStorage.People[1]];
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

    public Func<string, Person> PersonFactory { get; } = fullName =>
    {
        var splitNames = fullName.Split(" ");
        var firstName = splitNames[0];

        var middleName = string.Empty;
        var lastName = string.Empty;
        if (splitNames.Length == 2)
        {
            lastName = splitNames[1];
        }
        else if (splitNames.Length > 2)
        {
            middleName = splitNames[1];
            lastName = splitNames[2];
        }

        return new Person(firstName, lastName, middleName);
    };
}