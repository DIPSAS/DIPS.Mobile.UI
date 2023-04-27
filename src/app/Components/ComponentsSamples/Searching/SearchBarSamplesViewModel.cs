using Components.SampleData;
using DIPS.Mobile.UI.MVVM;

namespace Components.ComponentsSamples.Searching
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
            set => RaiseWhenSet(ref m_people, value);
        }
    }
}