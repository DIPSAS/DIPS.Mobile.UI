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
                new Person("Erika", "Isa"),
                new Person("Adam", "Lehi"),
                new Person("Emmerson", "Harve"),
                new Person("Sylvester", "Christel"),
                new Person("Hanne", "Dexter"),
                new Person("Ava", "Karl"),
                new Person("Knut","Arne","Johansen"),
                new Person("Mina", "Shawn"),
                new Person("Per","Gunnar", "Hansen"),
                new Person("Pablo", "Picasso","Diego José Francisco de Paula Juan Nepomuceno María de los Remedios Cipriano de la Santísima Trinidad Ruiz y"),
            };
            PersonSelectedCommand = new Command<Person>(PersonSelected);
        }

        private void PersonSelected(Person person)
        {
            //TODO: Do something with person
        }

        public event PropertyChangedEventHandler PropertyChanged;

        public IEnumerable<Person> Persons { get; }
        public ICommand PersonSelectedCommand { get; }

        public Person SelectedPerson
        {
            get => m_selectedPerson;
            set => PropertyChanged?.RaiseWhenSet(ref m_selectedPerson, value);
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