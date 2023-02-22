using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using DIPS.Mobile.UI.Components.BottomSheets;
using DIPS.Mobile.UI.Extensions;
using Xamarin.Forms;
using ListView = DIPS.Mobile.UI.Components.Lists.ListView;
using CheckBox = DIPS.Mobile.UI.Components.CheckBoxes.CheckBox;
using SearchBar = DIPS.Mobile.UI.Components.Searching.SearchBar;

namespace DIPS.Mobile.UI.Components.Pickers
{
    public class PickerBottomSheet : BottomSheet
    {
        private readonly Picker m_picker;
        private List<SelectableItem> m_items;
        private readonly List<SelectableItem> m_originalItems;
        private readonly SearchBar m_searchBar;
        private readonly ListView m_listView;

        public PickerBottomSheet(Picker picker)
        {
            m_picker = picker;
            m_originalItems = new List<SelectableItem>();
            if (m_picker.ItemsSource != null)
            {
                foreach (var item in m_picker.ItemsSource)
                {
                    m_originalItems.Add(new SelectableItem(item.GetPropertyValue(m_picker.ItemDisplayProperty),
                        item == m_picker.SelectedItem,
                        new Command(() =>
                        {
                            m_picker.SelectedItem = item;
                            Close();
                        })));
                }
            }
            
            Items = m_originalItems;

            m_searchBar = new SearchBar(){HasCancelButton = false};
            m_listView = new ListView()
            {
                HasUnevenRows = true,
                ItemTemplate = new DataTemplate(LoadTemplate),
                Margin = 10 //TODO: Use DesignSystem
            };

            if (m_picker.HasSearchBar)
            {
                m_listView.Header = string.Empty;
                m_listView.HeaderTemplate = new DataTemplate(() => m_searchBar);
            }
            
            m_listView.SetBinding(ListView.ItemsSourceProperty, new Binding(nameof(Items), source:this)); 
            
            Content = m_listView;
            SubscribeToEvents();
        }

        private void SubscribeToEvents()
        {
            m_searchBar.TextChanged += FilterItems;
        }
        
        private void UnSubscribeFromEvents()
        {
            m_searchBar.TextChanged -= FilterItems;
        }


        public static readonly BindableProperty ItemsProperty = BindableProperty.Create(
            nameof(Items),
            typeof(List<SelectableItem>),
            typeof(PickerBottomSheet));

        public List<SelectableItem> Items
        {
            get => (List<SelectableItem>)GetValue(ItemsProperty);
            set => SetValue(ItemsProperty, value);
        }

        private object LoadTemplate()
        {
            var checkBox = new CheckBox();
            checkBox.SetBinding(CheckBox.TextProperty, new Binding() {Path = nameof(SelectableItem.DisplayName)});
            checkBox.SetBinding(CheckBox.IsSelectedProperty, new Binding() {Path = nameof(SelectableItem.IsSelected)});
            checkBox.SetBinding(CheckBox.CommandProperty,
                new Binding() {Path = nameof(SelectableItem.IsSelectedCommand)});
            return new ViewCell() {View = checkBox};
        }
        
        private void FilterItems(object sender, TextChangedEventArgs e)
        {
            var filterText = e.NewTextValue;
            if (string.IsNullOrEmpty(filterText))
            {
                Items = m_originalItems.ToList();
            }
            else
            {
                Items = m_originalItems.Where(p => p.DisplayName.ToLower().Contains(filterText.ToLower())).ToList();
            }
        }

        protected override void OnDidClose()
        {
            UnSubscribeFromEvents();
            base.OnDidClose();
        }
    }
}