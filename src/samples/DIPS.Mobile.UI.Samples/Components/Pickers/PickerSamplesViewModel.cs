using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Windows.Input;
using DIPS.Mobile.UI.Extensions;
using Xamarin.Forms;

namespace DIPS.Mobile.UI.Samples.Components.Pickers
{
    public class PickerSamplesViewModel : INotifyPropertyChanged
    {
        private Person m_selectedPerson;

        public PickerSamplesViewModel()
        {
            Persons = new List<Person>()
            {
                new Person("FirstName1", "Lastname1"),
                new Person("FirstName2", "Lastname2"),
                new Person("FirstName3", "Lastname3"),
                new Person("FirstName4", "Lastname4"),
                new Person("FirstName5", "Lastname5"),
                new Person("FirstName6", "Lastname6"),
                new Person("FirstName7", "Lastname7"),
                new Person("FirstName8", "Lastname8"),
                new Person("FirstName9", "Lastname9"),
                new Person("FirstNaaaaame10", "Lastnaaaaame10", "MiddleNaaaame 10"),
            };
            SelectedPerson = Persons.First();
            PersonSelectedCommand = new Command<Person>(PersonSelected);
        }

        private void PersonSelected(Person person)
        {
        }

        public event PropertyChangedEventHandler PropertyChanged;

        public IEnumerable<Person> Persons { get; }
        public ICommand PersonSelectedCommand { get; }

        public Person SelectedPerson
        {
            get => m_selectedPerson;
            set => PropertyChanged?.RaiseWhenSet(ref m_selectedPerson, value);
        }

        public void Initialize()
        {
            SelectedPerson = Persons.First();
        }
    }

    public class Person
    {
        public string FirstName { get; }
        public string LastName { get; }
        public string MiddleName { get; }

        public Person(string firstName, string lastName, string middleName = null)
        {
            FirstName = firstName;
            LastName = lastName;
            MiddleName = middleName ?? string.Empty;
        }

        public string DisplayName => string.IsNullOrEmpty(MiddleName)
            ? $"{FirstName}, {LastName}"
            : $"{FirstName} {MiddleName}, {LastName}";
    }
}