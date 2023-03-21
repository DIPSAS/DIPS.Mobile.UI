using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Windows.Input;
using DIPS.Mobile.UI.Extensions;
using DIPS.Mobile.UI.MVVM;
using DIPS.Mobile.UI.Samples.SampleData;
using Xamarin.Forms;

namespace DIPS.Mobile.UI.Samples.Components.Searching
{
    public class SearchPageSamplesPageViewModel : ViewModel
    {
        private List<Person> m_people;
        private readonly List<Person> m_originalPeople;

        public SearchPageSamplesPageViewModel()
        {
            People = SampleDataStorage.People.ToList();
            m_originalPeople = People.ToList();
            SearchCommand = new Command<string>(FilterItems);
        }

        public void FilterItems(string filterText)
        {
            if (string.IsNullOrEmpty(filterText))
            {
                People = m_originalPeople.ToList();
            }
            else
            {
                People = m_originalPeople.Where(p => p.DisplayName.ToLower().Contains(filterText.ToLower())).ToList();
            }
        }

        public List<Person> People
        {
            get => m_people;
            set => RaiseWhenSet(ref m_people, value);
        }

        public ICommand SearchCommand { get; }
    }
}