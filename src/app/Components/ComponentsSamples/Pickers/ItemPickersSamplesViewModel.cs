using Components.SampleData;
using DIPS.Mobile.UI.MVVM;

namespace Components.ComponentsSamples.Pickers;

public class ItemPickersSamplesViewModel : ViewModel
{
    private Person m_selectedPerson;
    public IEnumerable<Person> People { get; } = SampleDataStorage.People;

    public Person SelectedPerson
    {
        get => m_selectedPerson;
        set => RaiseWhenSet(ref m_selectedPerson, value);
    }
}