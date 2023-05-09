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
        private readonly List<SelectableListItem> m_originalItems;
        private readonly SearchBar? m_searchBar;

        public PickerBottomSheet(ItemPicker itemPicker)
        {
            m_itemPicker = itemPicker;
            m_originalItems = new List<SelectableListItem>();
            if (m_itemPicker.ItemsSource != null)
            {
                var itemSource = m_itemPicker.ItemsSource.ToList();
                for (var i = 0; i < itemSource.Count; i++)
                {
                    var item = itemSource[i];
                    m_originalItems.Add(new SelectableListItem( item.GetPropertyValue(m_itemPicker.ItemDisplayProperty),
                        item == m_itemPicker.SelectedItem));
                }
            }

            Items = new ObservableCollection<SelectableListItem>(m_originalItems);

            var grid = new Grid() {RowDefinitions = new() {new(GridLength.Auto), new(GridLength.Star)}};

            var collectionView = new CollectionView()
            {
                ItemTemplate = new DataTemplate(CreateCheckBox),
                Margin = UI.Resources.Sizes.Sizes.GetSize(SizeName.size_2)
            };

            if (m_itemPicker.HasSearchBar)
            {
                m_searchBar = new SearchBar() {HasCancelButton = false, BackgroundColor = Colors.Transparent};
                grid.Add(m_searchBar);
            }

            collectionView.SetBinding(ItemsView.ItemsSourceProperty, new Binding(nameof(Items), source: this));

            grid.Add(collectionView, 0, (m_searchBar != null) ? 1 : 0);
            Content = grid;
            SubscribeToEvents();
        }

        private void SubscribeToEvents()
        {
            if (m_searchBar != null)
            {
                m_searchBar.TextChanged += FilterItems;
            }
        }

        private void UnSubscribeFromEvents()
        {
            if (m_searchBar != null)
            {
                m_searchBar.TextChanged -= FilterItems;
            }
        }


        public static readonly BindableProperty ItemsProperty = BindableProperty.Create(
            nameof(Items),
            typeof(ObservableCollection<SelectableListItem>),
            typeof(PickerBottomSheet));

        public ObservableCollection<SelectableListItem> Items
        {
            get => (ObservableCollection<SelectableListItem>)GetValue(ItemsProperty);
            set => SetValue(ItemsProperty, value);
        }

        private IView CreateCheckBox()
        {
            var checkBox = new CheckBox();
            checkBox.SetBinding(CheckBox.TextProperty, new Binding() {Path = nameof(SelectableListItem.DisplayName)});
            checkBox.SetBinding(CheckBox.IsSelectedProperty, new Binding() {Path = nameof(SelectableListItem.IsSelected)});
            checkBox.Command = new Command(() => ItemWasPicked(checkBox));
            return checkBox;
        }

        private void ItemWasPicked(CheckBox checkBox)
        {
            if (checkBox.IsSelected)
            {
                var displayName = checkBox.Text;
                var theSelectedItem = m_itemPicker.ItemsSource?.FirstOrDefault(i =>
                    i.GetPropertyValue(m_itemPicker.ItemDisplayProperty) == displayName);
                if (theSelectedItem ==
                    m_itemPicker
                        .SelectedItem) //This happens the first time the list of items was drawn or when people use the search bar to redraw the items
                {
                    return;
                }

                m_itemPicker.SelectedItem = theSelectedItem;
                Close();
            }
        }

        private void FilterItems(object? sender, TextChangedEventArgs e)
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
                var filteredItems = m_originalItems.Where(p => p.DisplayName.ToLower().Contains(filterText.ToLower()))
                    .ToList();
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