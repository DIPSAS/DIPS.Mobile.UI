using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using DIPS.Mobile.UI.Extensions;
using DIPS.Mobile.UI.MVVM;
using DIPS.Mobile.UI.Samples.SampleData;

namespace DIPS.Mobile.UI.Samples.Components.Searching
{
    public class SearchBarSamplesViewModel : ViewModel
    {
        private List<Person> m_people;
        private readonly List<Person> m_originalPeople;
        public SearchBarSamplesViewModel()
        {
            People = SampleDataStorage.People.ToList();
            m_originalPeople = People.ToList();
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
            set => Set(ref m_people, value);
        }
    }
}