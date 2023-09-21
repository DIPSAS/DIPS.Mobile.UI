using System.Collections.ObjectModel;
using DIPS.Mobile.UI.Components.BottomSheets;
using DIPS.Mobile.UI.Components.ListItems;
using DIPS.Mobile.UI.Components.ListItems.Extensions;
using DIPS.Mobile.UI.Components.Pickers.ItemPicker;
using DIPS.Mobile.UI.Effects.Touch;
using CollectionView = DIPS.Mobile.UI.Components.Lists.CollectionView;
using CheckBox = DIPS.Mobile.UI.Components.CheckBoxes.CheckBox;

namespace DIPS.Mobile.UI.Components.Pickers.MultiItemsPicker;

internal class MultiItemsPickerBottomSheet : BottomSheet
{
    private readonly List<SelectableItemViewModel> m_originalItems;

    public MultiItemsPickerBottomSheet(MultiItemsPicker multiItemsPicker)
    {
        m_multiItemsPicker = multiItemsPicker;
        m_originalItems = new List<SelectableItemViewModel>();

        if (m_multiItemsPicker.ItemsSource != null)
        {
            foreach (var item in m_multiItemsPicker.ItemsSource)
            {
                var isSelected = false;
                if (multiItemsPicker.SelectedItems != null)
                {
                    isSelected = multiItemsPicker.SelectedItems.Cast<object?>().ToList().Contains(item);
                }
                
                m_originalItems.Add(new SelectableItemViewModel(
                    (item.GetPropertyValue(m_multiItemsPicker.ItemDisplayProperty) ?? item.ToString()) ?? string.Empty,
                    isSelected, item));
            }
        }

        Items = new ObservableCollection<SelectableItemViewModel>(m_originalItems);
        
        SetBinding(HasSearchBarProperty, new Binding(){Source = m_multiItemsPicker.BottomSheetPickerConfiguration, Path = nameof(BottomSheetPickerConfiguration.HasSearchBar)});

        var collectionView = new CollectionView()
        {
            ItemTemplate = new DataTemplate(LoadTemplate), Margin = Sizes.GetSize(SizeName.size_2)
        };
        
        collectionView.SetBinding(ItemsView.ItemsSourceProperty, new Binding(nameof(Items), source: this));
        
        Content = ItemPickerBottomSheet.CreateContentControlForActivityIndicator(collectionView, m_multiItemsPicker.BottomSheetPickerConfiguration);
    }
    
    private object LoadTemplate()
    {
        if (m_multiItemsPicker.BottomSheetPickerConfiguration is
            {SelectableItemTemplate: not null} bottomSheetConfiguration)
        {
            return CreateConsumerView(bottomSheetConfiguration.SelectableItemTemplate);
        }

        return CreateDefaultView();
    }
    
    private IView CreateConsumerView(ControlTemplate selectableItemTemplate)
    {
        var contentView = new SelectableItemContentView(){ ControlTemplate = selectableItemTemplate};
        contentView.SetBinding(SelectableItemContentView.ItemProperty, new Binding(){Path = nameof(SelectableItemViewModel.Item)});
        contentView.SetBinding(SelectableItemContentView.IsSelectedProperty, new Binding(){Path = nameof(SelectableItemViewModel.IsSelected)});
        Touch.SetCommand(contentView, new Command(() => ItemWasTapped(contentView)));
        return contentView;
    }

    public static readonly BindableProperty ItemsProperty = BindableProperty.Create(
        nameof(Items),
        typeof(ObservableCollection<SelectableItemViewModel>),
        typeof(ItemPickerBottomSheet));
    
    private readonly MultiItemsPicker m_multiItemsPicker;

    public ObservableCollection<SelectableItemViewModel> Items
    {
        get => (ObservableCollection<SelectableItemViewModel>)GetValue(ItemsProperty);
        set => SetValue(ItemsProperty, value);
    }

    private IView CreateDefaultView()
    {
        var checkmarkListItem = new CheckmarkListItem() {HasBottomDivider = true};
        checkmarkListItem.SetBinding(ListItem.TitleProperty,
            new Binding() {Path = nameof(SelectableItemViewModel.DisplayName)});
        checkmarkListItem.SetBinding(CheckmarkListItem.IsSelectedProperty,
            new Binding() {Path = nameof(SelectableItemViewModel.IsSelected)});
        checkmarkListItem.HasHaptics = m_multiItemsPicker.HasHaptics;
        
        checkmarkListItem.SelectedCommand = new Command(() => ItemWasTapped(checkmarkListItem));
        return checkmarkListItem;
    }

    private void ItemWasTapped(BindableObject bindableObject)
    {
        if (bindableObject.BindingContext is not SelectableItemViewModel selectableListItem) return;
        ToggleItem(selectableListItem.IsSelected = !selectableListItem.IsSelected, selectableListItem);
        
    }
    
    private void ItemWasTapped(CheckmarkListItem checkBox)
    {
        if (checkBox.BindingContext is not SelectableItemViewModel selectableListItem) return;
        ToggleItem(checkBox.IsSelected, selectableListItem);
    }

    private void ToggleItem(bool isSelected, SelectableItemViewModel selectableItemViewModel)
    {
        if (!isSelected)
        {
            m_multiItemsPicker.DeSelectItem(selectableItemViewModel.Item);
        }
        else
        {
            m_multiItemsPicker.SelectItem(selectableItemViewModel.Item);
        }
    }

    protected override void OnSearchTextChanged(string value)
    {
        base.OnSearchTextChanged(value);

        FilterItems(value);
    }

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
            if (m_multiItemsPicker.SelectedItems == null)
            {
                continue;
            }

            if (m_multiItemsPicker.SelectedItems.Cast<object?>().ToList().Contains(selectableListItem.Item))
            {
                selectableListItem.IsSelected = true;    
            }
        }
    }
}