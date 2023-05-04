using DIPS.Mobile.UI.Components.BottomSheets;
using DIPS.Mobile.UI.Extensions;
using DIPS.Mobile.UI.Sizes.Sizes;
using CheckBox = DIPS.Mobile.UI.Components.CheckBoxes.CheckBox;
using SearchBar = DIPS.Mobile.UI.Components.Searching.SearchBar;
using CollectionView = DIPS.Mobile.UI.Components.Lists.CollectionView;
using System.Collections.ObjectModel;

namespace DIPS.Mobile.UI.Components.Pickers
{
    public class PickerBottomSheet : BottomSheet
    {
        private readonly ItemPicker m_itemPicker;
        private List<SelectableItem> m_items;
        private readonly List<SelectableItem> m_originalItems;
        private readonly SearchBar m_searchBar;
        private readonly CollectionView m_collectionView;

        public PickerBottomSheet(ItemPicker itemPicker)
        {
            m_itemPicker = itemPicker;
            m_originalItems = new List<SelectableItem>();
            if (m_itemPicker.ItemsSource != null)
            {
                foreach (var item in m_itemPicker.ItemsSource)
                {
                    m_originalItems.Add(new SelectableItem(item.GetPropertyValue(m_itemPicker.ItemDisplayProperty),
                        item == m_itemPicker.SelectedItem,
                        new Command(() =>
                        {
                            if (m_itemPicker.SelectedItem ==
                                item) //Reason we have to guard is because the items get recreated if people use the search bar, the item that was selected will get re-selected and it will close the sheet if that happens.
                            {
                                return;
                            }

                            m_itemPicker.SelectedItem = item;
                            Close();
                        })));
                }
            }

            Items = new ObservableCollection<SelectableItem>(m_originalItems);

            var vStack = new VerticalStackLayout();
            m_searchBar = new SearchBar() { HasCancelButton = false, BackgroundColor = Colors.Transparent };
            m_collectionView = new CollectionView()
            {
                ItemTemplate = new DataTemplate(CreateCheckBox),
                Margin = UI.Resources.Sizes.Sizes.GetSize(SizeName.size_2)
            };

            if (m_itemPicker.HasSearchBar)
            {
                vStack.Add(m_searchBar);
            }

            m_collectionView.SetBinding(ItemsView.ItemsSourceProperty, new Binding(nameof(Items), source: this));

            vStack.Add(m_collectionView);
            Content = vStack;
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
            typeof(ObservableCollection<SelectableItem>),
            typeof(PickerBottomSheet));

        public ObservableCollection<SelectableItem> Items
        {
            get => (ObservableCollection<SelectableItem>)GetValue(ItemsProperty);
            set => SetValue(ItemsProperty, value);
        }

        private IView CreateCheckBox()
        {
            var checkBox = new CheckBox();
            checkBox.SetBinding(CheckBox.TextProperty, new Binding() { Path = nameof(SelectableItem.DisplayName) });
            checkBox.SetBinding(CheckBox.IsSelectedProperty, new Binding() { Path = nameof(SelectableItem.IsSelected) });
            checkBox.SetBinding(CheckBox.CommandProperty,
                new Binding() { Path = nameof(SelectableItem.IsSelectedCommand) });
            return checkBox;
        }

        private void FilterItems(object sender, TextChangedEventArgs e)
        {
            var filterText = e.NewTextValue;
            if (string.IsNullOrEmpty(filterText))
            {
                Items.Clear();
                foreach (var item in m_originalItems)
                {
                    Items.Add(item);
                }
            }
            else
            {
                var filteredItems = m_originalItems.Where(p => p.DisplayName.ToLower().Contains(filterText.ToLower())).ToList();
                Items.Clear();
                foreach (var item in filteredItems)
                {
                    Items.Add(item);
                }
            }
        }

        protected override void OnDidClose()
        {
            UnSubscribeFromEvents();
            base.OnDidClose();
        }
    }
}