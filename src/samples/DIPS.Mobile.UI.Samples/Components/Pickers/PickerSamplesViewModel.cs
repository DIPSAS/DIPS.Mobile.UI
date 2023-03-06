using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Windows.Input;
using DIPS.Mobile.UI.Extensions;
using DIPS.Mobile.UI.Samples.SampleData;
using Xamarin.Forms;

namespace DIPS.Mobile.UI.Samples.Components.Pickers
{
    public class PickerSamplesViewModel : INotifyPropertyChanged
    {
        private Person m_selectedPerson;
        private DateTime m_selectedBirthday;

        public PickerSamplesViewModel()
        {
            People = SampleDataStorage.People;
            PersonSelectedCommand = new Command<Person>(PersonSelected);
            m_selectedBirthday = new DateTime(1989,01,28);
        }

        private void PersonSelected(Person person)
        {
            //TODO: Do something with person
        }

        public event PropertyChangedEventHandler PropertyChanged;

        public IEnumerable<Person> People { get; }
        public ICommand PersonSelectedCommand { get; }

        public Person SelectedPerson
        {
            get => m_selectedPerson;
            set => PropertyChanged?.RaiseWhenSet(ref m_selectedPerson, value);
        }

        public DateTime SelectedBirthday
        {
            get => m_selectedBirthday;
            set => PropertyChanged?.RaiseWhenSet(ref m_selectedBirthday, value);
        }
    }
}