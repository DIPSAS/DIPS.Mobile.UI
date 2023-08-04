using Components.SampleData;
using DIPS.Mobile.UI.MVVM;

namespace Components.ComponentsSamples.Pickers;

public class ItemPickersSamplesViewModel : ViewModel
{
    private Person m_selectedPerson;

    public IEnumerable<Person> People { get; } = SampleDataStorage.People;

    public IEnumerable<Contact> Contacts { get; set; } = new List<Contact>()
    {
        new Contact("Poliklinikk | 29. des 2022 kI 12:00 - 29. des 2022 kl 15:15 | KIR", "Poli.|KIR"),
        new Contact("Poliklinikk | 21. des 2022 kI 09:30 - 21. des 2022 kI 10:25 | KIR", "Poli.|KIR"),
        new Contact("Poliklinikk | 05. des 2022 kl 09:00 - 05. des 2022 kI 10:00 | KIR", "Poli.|KIR"),
        new Contact("Innleggelse | 30. nov 2022 kI 09:21 - 24. apr 2023 kl 14:41 | KIR", "Innleg.|KIR"),
    };

    public Person SelectedPerson
    {
        get => m_selectedPerson;
        set => RaiseWhenSet(ref m_selectedPerson, value);
    }
}

public class Contact
{
    public Contact(string displayName, string shortDisplayName)
    {
        DisplayName = displayName;
        ShortDisplayName = shortDisplayName;
    }
    
    public string DisplayName { get; set; }
    public string ShortDisplayName { get; set; }
}