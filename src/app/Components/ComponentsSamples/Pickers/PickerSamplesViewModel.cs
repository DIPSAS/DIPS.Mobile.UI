using Components.SampleData;
using DIPS.Mobile.UI.MVVM;

namespace Components.ComponentsSamples.Pickers;

public class PickerSamplesViewModel : ViewModel
{
    private Person m_selectedPerson;
    private DateTime m_selectedBirthday;
        
    public PickerSamplesViewModel()
    {
        People = SampleDataStorage.People;
        SelectedPerson = People.FirstOrDefault();
        SelectedBirthday = new DateTime(1989,01,28);
    }

    private void PersonSelected(Person person)
    {
        //TODO: Do something with person
    }
    public IEnumerable<Person> People { get; }

    public Person SelectedPerson
    {
        get => m_selectedPerson;
        set => RaiseWhenSet(ref m_selectedPerson, value);
    }

    public DateTime SelectedBirthday
    {
        get => m_selectedBirthday;
        set => RaiseWhenSet(ref m_selectedBirthday, value);
    }
}