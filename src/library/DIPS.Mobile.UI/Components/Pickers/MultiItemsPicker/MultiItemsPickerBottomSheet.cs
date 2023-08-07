using System.Collections.ObjectModel;
using DIPS.Mobile.UI.Components.BottomSheets;
using DIPS.Mobile.UI.Components.Pickers.ItemPicker;
using CheckBox = DIPS.Mobile.UI.Components.CheckBoxes.CheckBox;

namespace DIPS.Mobile.UI.Components.Pickers.MultiItemsPicker;

internal class MultiItemsPickerBottomSheet : BottomSheet
{
    private readonly MultiItemsPicker m_multiItemsPicker;
    private readonly List<SelectableListItem> m_originalItems;

    public MultiItemsPickerBottomSheet(MultiItemsPicker multiItemsPicker)
    {
        m_multiItemsPicker = multiItemsPicker;
        m_originalItems = new List<SelectableListItem>();

        //TODO: Move out
        if (m_multiItemsPicker.ItemsSource != null)
        {
            foreach (var item in m_multiItemsPicker.ItemsSource)
            {
                m_originalItems.Add(new SelectableListItem(
                    item.GetPropertyValue(m_multiItemsPicker.ItemDisplayProperty)!,
                    m_multiItemsPicker.SelectedItems != null && m_multiItemsPicker.SelectedItems.Contains(item), item));
            }
        }

        Items = new ObservableCollection<SelectableListItem>(m_originalItems);

        HasSearchBar = m_multiItemsPicker.HasSearchBar;

        var collectionView = new CollectionView()
        {
            ItemTemplate = new DataTemplate(CreateDefaultView), Margin = Sizes.GetSize(SizeName.size_2)
        };

        collectionView.SetBinding(ItemsView.ItemsSourceProperty, new Binding(nameof(Items), source: this));

        Content = collectionView;
    }

    public static readonly BindableProperty ItemsProperty = BindableProperty.Create(
        nameof(Items),
        typeof(ObservableCollection<SelectableListItem>),
        typeof(ItemPickerBottomSheet));

    private bool m_isSelecting;

    //TODO: Move out
    public ObservableCollection<SelectableListItem> Items
    {
        get => (ObservableCollection<SelectableListItem>)GetValue(ItemsProperty);
        set => SetValue(ItemsProperty, value);
    }

    //TODO: Move out
    private IView CreateDefaultView()
    {
        var checkBox = new CheckBox();
        checkBox.SetBinding(CheckBox.TextProperty, new Binding() {Path = nameof(SelectableListItem.DisplayName)});
        checkBox.SetBinding(CheckBox.IsSelectedProperty,
            new Binding() {Path = nameof(SelectableListItem.IsSelected)});
        checkBox.Command = new Command(() => ItemWasTapped(checkBox));
        return checkBox;
    }

    //TODO: Move?
    private void ItemWasTapped(CheckBox checkBox)
    {
        if (checkBox.BindingContext is not SelectableListItem selectableListItem) return;
        if (!checkBox.IsSelected)
        {
            m_multiItemsPicker.DeSelectItem(selectableListItem.Item);
        }
        else
        {
            m_multiItemsPicker.SelectItem(selectableListItem.Item);    
        }
    }

    protected override void OnSearchTextChanged(string value)
    {
        base.OnSearchTextChanged(value);

        FilterItems(value);
    }

    //TODO: Move out
    private void FilterItems(string value)
    {
        var filterText = value;
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

        foreach (var selectableListItem in Items)
        {
            if (m_multiItemsPicker.SelectedItems != null && m_multiItemsPicker.SelectedItems.Contains(selectableListItem.Item))
            {
                selectableListItem.IsSelected = true;
            }
        }
    }
}