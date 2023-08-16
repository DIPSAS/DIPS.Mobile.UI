using System.Collections;
using System.Windows.Input;
using DIPS.Mobile.UI.MVVM;

namespace Playground.HåvardSamples;

public class HåvardPageViewModel : ViewModel
{
    private object m_selectedItem;
    private List<Contact> m_items;
    private List<Contact> m_selectedItems;

    public ICommand RefreshCommand { get; set; }

    public HåvardPageViewModel()
    {
        Items = GetContacts();

        m_selectedItem = Items.First();

        m_selectedItems = Items.Take(new Range(0, 3)).ToList();

        RefreshCommand = new Command(Refresh);
    }

    private static List<Contact> GetContacts()
    {
        return new List<Contact>()
        {
            new Contact(1, "948 19 788"),
            new Contact(2, "936 15 719"),
            new Contact(3, "959 94 238"),
            new Contact(4, "464 51 717"),
            new Contact(5, "916 18 816"),
            new Contact(6, "998 15 227"),
            new Contact(7, "413 10 076"),
            new Contact(8, "439 14 037"),
            new Contact(9, "979 82 486"),
            new Contact(10, "453 26 392"),
            new Contact(111, "986 77 073"),
        };
    }

    public void Refresh()
    {
        Items = GetContacts();
        if (Items.FirstOrDefault(i => Equals(i, m_selectedItem)) != null)
        {
            SelectedItem = null;
        }
    }

    public List<Contact> Items
    {
        get => m_items;
        set => RaiseWhenSet(ref m_items, value);
    }

    public object SelectedItem
    {
        get => m_selectedItem;
        set => RaiseWhenSet(ref m_selectedItem, value);
    }

    public List<Contact> SelectedItems
    {
        get => m_selectedItems;
        set => RaiseWhenSet(ref m_selectedItems, value);
    }

    public class Contact
    {
        public int Id { get; }
        public string Name { get; }

        public Contact(int id, string name)
        {
            Id = id;
            Name = name;
        }

        public override bool Equals(object obj)
        {
            if (obj is Contact otherContact)
            {
                return otherContact.Id == this.Id;
            }

            return false;
        }

        protected bool Equals(Contact other)
        {
            return Id == other.Id && Name == other.Name;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(Id, Name);
        }
    }
}