using System.Collections.Specialized;
using DIPS.Mobile.UI.Components.BottomSheets;
using DIPS.Mobile.UI.Components.Chips;
using DIPS.Mobile.UI.Components.ContextMenus;
using DIPS.Mobile.UI.Internal;
using DIPS.Mobile.UI.Resources.Styles.Chip;
using Colors = Microsoft.Maui.Graphics.Colors;

namespace DIPS.Mobile.UI.Components.Pickers.ItemPicker
{
    public partial class ItemPicker : ContentView
    {
        private readonly ContextMenu m_contextMenu = new();
        private readonly Chip m_chip = new(){AutomationId = "ItemPickerChip".ToDUIAutomationId<ItemPicker>()};

        public ItemPicker()
        {
            m_chip.SetBinding(IsEnabledProperty, new Binding() {Path = nameof(IsEnabledProperty), Source = this});
            m_chip.SetBinding(MaximumHeightRequestProperty,
                new Binding() {Path = nameof(MaximumHeightRequest), Source = this});
            MaximumWidthRequest = 200;
            
            Loaded += OnLoaded;
            Unloaded += OnUnLoaded;
        }

        private void OnUnLoaded(object? sender, EventArgs e)
        {
            Unloaded -= OnLoaded;
            Dispose();
        }

        private void Dispose()
        {
            if (ItemsSource is INotifyCollectionChanged notifyCollectionChanged)
                notifyCollectionChanged.CollectionChanged -= OnCollectionChanged;
        }

        private void OnLoaded(object? sender, EventArgs e)
        {
            Loaded -= OnLoaded;
            LayoutContent();
        }

        private void LayoutContent()
        {
            Content = m_chip;

            if (Mode == PickerMode.ContextMenu)
            {
                ContextMenuEffect.SetMenu(m_chip, m_contextMenu);
                m_contextMenu.ItemClickedCommand = new Command<ContextMenuItem>(SetSelectedItemBasedOnContextMenuItem);
            }
            else if (Mode == PickerMode.BottomSheet)
            {
                m_chip.Command = OpenCommand;
            }

            SelectedItemChanged();
        }

        internal void UpdateChipTitle(string? title)
        {
            var hasPlaceHolder = title == null || string.IsNullOrEmpty(title);
            m_chip.Title = hasPlaceHolder ? Placeholder : title!;
            m_chip.Style = hasPlaceHolder ? EmptyInputStyle.Current : InputStyle.Current;
        }

        public partial void Open()
        {
            if (Mode == PickerMode.BottomSheet)
            {
                OpenBottomSheet();
            }
        }

        private void SelectedItemChanged()
        {
            SelectedItemCommand?.Execute(SelectedItem);
            DidSelectItem?.Invoke(this, SelectedItem!);

            var displayName = SelectedItem?.GetPropertyValue(ItemDisplayProperty) ?? null;
            UpdateChipTitle(displayName);

            switch (Mode)
            {
                case PickerMode.ContextMenu:
                    UpdateContextMenuItems(); //<-- Needed if the selected item was set programatically, and not by the user
                    break;
                case PickerMode.BottomSheet:
                    if (BottomSheetService.IsOpen())
                    {
                        BottomSheetService.CloseAll();
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
    }
}