using Components.SampleData;
using DIPS.Mobile.UI.MVVM;

namespace Components.ComponentsSamples.Pickers;

public class PickerSamplesViewModel : ViewModel
{
    private Person m_selectedPerson;
    private DateTime m_selectedBirthday;
    private TimeSpan m_selectedGroceryShopping;
    private DateTime m_selectedDeadline;

    public PickerSamplesViewModel()
    {
        People = SampleDataStorage.People;
        SelectedPerson = People.FirstOrDefault();
        SelectedBirthday = new DateTime(1989,01,28);
        SelectedGroceryShopping = new TimeSpan(23, 23, 0);
        SelectedDeadline = new DateTime(2023, 01, 01, 23, 23, 0, DateTimeKind.Local);
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

    public TimeSpan SelectedGroceryShopping
    {
        get => m_selectedGroceryShopping;
        set => RaiseWhenSet(ref m_selectedGroceryShopping, value);
    }

    public DateTime SelectedDeadline
    {
        get => m_selectedDeadline;
        set => RaiseWhenSet(ref m_selectedDeadline, value);
    }
}