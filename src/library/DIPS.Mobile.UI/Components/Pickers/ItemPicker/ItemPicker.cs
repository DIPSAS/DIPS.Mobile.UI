using System.Collections.Specialized;
using System.Security.AccessControl;
using DIPS.Mobile.UI.Components.BottomSheets;
using DIPS.Mobile.UI.Components.Chips;
using DIPS.Mobile.UI.Components.ContextMenus;
using DIPS.Mobile.UI.Effects.Touch;
using DIPS.Mobile.UI.Internal;
using DIPS.Mobile.UI.Resources.Styles;
using DIPS.Mobile.UI.Resources.Styles.Chip;

namespace DIPS.Mobile.UI.Components.Pickers.ItemPicker;

public partial class ItemPicker : ContentView
{
    private readonly ContextMenu m_contextMenu = new();
    private readonly Chip m_chip = new(){AutomationId = "ItemPickerChip".ToDUIAutomationId<ItemPicker>()};
    
    private readonly ImageSource m_largeItemPickerRightIcon = Icons.GetIcon(IconName.chevron_down_line);
    
    public ItemPicker()
    {
        m_chip.SetBinding(IsEnabledProperty, static (ItemPicker itemPicker) => itemPicker.IsEnabled, source: this);
        m_chip.SetBinding(MaximumHeightRequestProperty, static (Chip chip) => chip.MaximumHeightRequest, source: this);
    }
    
    protected override void OnHandlerChanging(HandlerChangingEventArgs args)
    {
        base.OnHandlerChanging(args);
            
        if(args.NewHandler is not null)
            LayoutContent();
        else
            Dispose();
    }

    private void LayoutContent()
    {
        if (Size is PickerSize.Small)
        {
            MaximumWidthRequest = 200;
        }
        else
        {
            m_chip.InnerPadding = new Thickness(Sizes.GetSize(SizeName.size_3), Sizes.GetSize(SizeName.size_2));
            m_chip.TitleTextAlignment = TextAlignment.Start;
            m_chip.CustomRightIcon = m_largeItemPickerRightIcon;
        }
        
        Content = m_chip;

        if (CustomTapCommand is not null)
        {
            m_chip.Command = CustomTapCommand;
            m_chip.CommandParameter = CustomTapCommandParameter;
        }
        else
        {
            if (Mode == PickerMode.ContextMenu)
            {
                // If not enabled, no need to set context menu effect
                ContextMenuEffect.SetMenu(m_chip, m_contextMenu);
                m_contextMenu.ItemClickedCommand = new Command<ContextMenuItem>(SetSelectedItemBasedOnContextMenuItem);
                    
            }
            else if (Mode == PickerMode.BottomSheet)
            {
                m_chip.Command = OpenCommand;
            }
        }

        SelectedItemChanged();
    }

    private void UpdateChipTitle()
    {
        var title = SelectedItem?.GetPropertyValue(ItemDisplayProperty);
        
        m_chip.Title = string.IsNullOrEmpty(title) ? Placeholder : title!;
        SetStyle();
    }

    private void SetStyle()
    {
        if (IsReadOnly)
        {
            m_chip.Style = Styles.GetChipStyle(ChipStyle.ReadOnly);
        }
        else
        {
            m_chip.Style = SelectedItem is null ? EmptyInputStyle.Current : InputStyle.Current;
        }
    }

    public partial void Open()
    {
        if (!IsEnabled)
            return;
            
        if (Mode == PickerMode.BottomSheet)
        {
            OpenBottomSheet();
        }
    }

    private void SelectedItemChanged()
    {
        SelectedItemCommand?.Execute(SelectedItem);
        DidSelectItem?.Invoke(this, SelectedItem!);
        
        UpdateChipTitle();

        switch (Mode)
        {
            case PickerMode.ContextMenu:
                UpdateContextMenuItems(); //<-- Needed if the selected item was set programatically, and not by the user
                break;
            case PickerMode.BottomSheet:
                if (BottomSheetService.IsOpen())
                {
                    _ = BottomSheetService.CloseAll();
                }

                break;
            default:
                throw new ArgumentOutOfRangeException();
        }
    }

    private static void ItemsSourceChanged(BindableObject bindable, object oldValue, object newValue)
    {
        if (bindable is not ItemPicker picker)
        {
            return;
        }

        if (picker.Mode == PickerMode.ContextMenu)
        {
            picker.AddContextMenuItems();
        }

        if (newValue is INotifyCollectionChanged notifyCollectionChanged)
        {
            notifyCollectionChanged.CollectionChanged += picker.OnCollectionChanged;
        }

        //Make sure to remove selected item if its not a part of the new items source
        if (picker is {ItemsSource: not null, SelectedItem: not null})
        {
            var itemsSource = picker.ItemsSource.Cast<object?>();
            if (!itemsSource.Contains(picker.SelectedItem))
            {
                picker.SelectedItem = null;
            }
        }
    }

    private void OnCollectionChanged(object? sender,
        NotifyCollectionChangedEventArgs notifyCollectionChangedEventArgs)
    {
        AddContextMenuItems();
    }

    private object? GetItemFromDisplayProperty(string toCompare)
    {
        if (ItemsSource == null)
        {
            return null;
        }

        return ItemsSource.Cast<object?>().FirstOrDefault(item =>
            toCompare.Equals(item.GetPropertyValue(ItemDisplayProperty), StringComparison.InvariantCulture));
    }
        
    private void Dispose()
    {
        if (ItemsSource is INotifyCollectionChanged notifyCollectionChanged)
            notifyCollectionChanged.CollectionChanged -= OnCollectionChanged;
    }

    private void OnIsReadOnlyChanged()
    {
        if(Size is PickerSize.Large)
            m_chip.CustomRightIcon = IsReadOnly ? null : m_largeItemPickerRightIcon;
        
        IsEnabled = !IsReadOnly;
        SetStyle();
    }
}