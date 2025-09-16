using System.Collections.ObjectModel;
using System.ComponentModel;
using DIPS.Mobile.UI.Components.BottomSheets;
using DIPS.Mobile.UI.Components.ListItems;
using DIPS.Mobile.UI.Components.ListItems.Extensions;
using DIPS.Mobile.UI.Components.Pickers.ItemPicker;
using DIPS.Mobile.UI.Converters.ValueConverters;
using DIPS.Mobile.UI.Effects.Touch;
using DIPS.Mobile.UI.MVVM.Commands;
using DIPS.Mobile.UI.Resources.LocalizedStrings.LocalizedStrings;
using DIPS.Mobile.UI.Resources.Styles;
using DIPS.Mobile.UI.Resources.Styles.Button;
using Button = DIPS.Mobile.UI.Components.Buttons.Button;
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

        LoadItemsFromPicker(multiItemsPicker);

        this.SetBinding(TitleProperty, static (BottomSheetPickerConfiguration configuration) => configuration.Title, source: m_multiItemsPicker.BottomSheetPickerConfiguration);
        this.SetBinding(HasSearchBarProperty, static (BottomSheetPickerConfiguration bottomSheetPickerConfiguration) => bottomSheetPickerConfiguration.HasSearchBar, source: m_multiItemsPicker.BottomSheetPickerConfiguration);
        BottomSheetHeaderBehavior.SetBinding(BottomSheets.Header.BottomSheetHeaderBehavior.IsCloseButtonVisibleProperty, static (MultiItemsPicker multiItemsPicker) => multiItemsPicker.HasDoneButton, source: m_multiItemsPicker, converter: new InvertedBoolConverter());
        
        var collectionView = new CollectionView()
        {
            ItemTemplate = new DataTemplate(LoadTemplate), Margin = Sizes.GetSize(SizeName.content_margin_small)
        };
        
        collectionView.SetBinding(ItemsView.ItemsSourceProperty, static (MultiItemsPickerBottomSheet multiItemsPickerBottomSheet) => multiItemsPickerBottomSheet.Items, source: this);
        
        if (m_multiItemsPicker.BottomSheetPickerConfiguration.FooterTemplate is not null)
        {
            collectionView.SetBinding(StructuredItemsView.FooterProperty, static (MultiItemsPicker itemPicker) => itemPicker.BindingContext, source: m_multiItemsPicker);
            collectionView.FooterTemplate = m_multiItemsPicker.BottomSheetPickerConfiguration.FooterTemplate;
        }
        
        Content = ItemPickerBottomSheet.CreateContentControlForActivityIndicator(collectionView, m_multiItemsPicker.BottomSheetPickerConfiguration);

        AddBottomBarButtons();
    }

    private void AddBottomBarButtons()
    {
        TryAddResetButton();

        AddDoneButton();
    }

    private void TryAddResetButton()
    {
        if (m_multiItemsPicker.ResetBehaviour is null) return;

        m_multiItemsPicker.ResetBehaviour.SetBinding(BindingContextProperty, static (MultiItemsPicker picker) => picker.BindingContext, source: m_multiItemsPicker);
            
        var button = new Button
        {
            Style = Styles.GetButtonStyle(ButtonStyle.SecondaryLarge), 
            Text = DUILocalizedStrings.Reset,
            VerticalOptions = LayoutOptions.End,
            HorizontalOptions = LayoutOptions.Center,
            Command = new Command(ClearSelectedItems)
        };
            
        BottombarButtons.Add(button);
    }

    private void LoadItemsFromPicker(MultiItemsPicker multiItemsPicker)
    {
        m_originalItems.Clear();
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
    }

    private void AddDoneButton()
    {
        if (!m_multiItemsPicker.HasDoneButton) return;
        
        var doneButton = new Button
        {
            Style = Styles.GetButtonStyle(ButtonStyle.PrimaryLarge),
            Text = DUILocalizedStrings.Done,
            VerticalOptions = LayoutOptions.End,
            HorizontalOptions = LayoutOptions.Center,
            Command = new AsyncCommand(() => Close())
        };
        
        BottombarButtons.Add(doneButton);
    }

    private void ClearSelectedItems()
    {
        if (m_multiItemsPicker.ResetBehaviour?.Command is not null)
        {
            // Do custom user action, then reload the list
            m_multiItemsPicker.ResetBehaviour.Command.Execute(null);
            
            LoadItemsFromPicker(m_multiItemsPicker);
        }
        else
        {
            foreach (var item in Items)
            {
                item.IsSelected = false;
            }   
        
            m_multiItemsPicker.ClearSelectedItems();
        }
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
        contentView.SetBinding(SelectableItemContentView.ItemProperty, static (SelectableItemViewModel selectableItemViewModel) => selectableItemViewModel.Item);
        contentView.SetBinding(SelectableItemContentView.IsSelectedProperty, static (SelectableItemViewModel selectableItemViewModel) => selectableItemViewModel.IsSelected);
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
        checkmarkListItem.SetBinding(ListItem.TitleProperty, static (SelectableItemViewModel selectableItemViewModel) => selectableItemViewModel.DisplayName);
        checkmarkListItem.SetBinding(CheckmarkListItem.IsSelectedProperty, static (SelectableItemViewModel selectableItemViewModel) => selectableItemViewModel.IsSelected);
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