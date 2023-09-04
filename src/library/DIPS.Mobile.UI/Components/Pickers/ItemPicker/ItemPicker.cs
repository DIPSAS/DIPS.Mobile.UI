using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Windows.Input;
using DIPS.Mobile.UI.Components.BottomSheets;
using DIPS.Mobile.UI.Components.Chips;
using DIPS.Mobile.UI.Components.ContextMenus;

namespace DIPS.Mobile.UI.Components.Pickers.ItemPicker
{
    public partial class ItemPicker : ContentView
    {
        private readonly ContextMenu m_contextMenu = new ();
        private readonly Chip m_chip = new()
        {
            VerticalOptions = LayoutOptions.Center
        };

        public ItemPicker()
        {
            m_chip.SetBinding(MaximumHeightRequestProperty, new Binding(){Path = nameof(MaximumWidthRequest), Source = this});
            MaximumWidthRequest = 200;
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
        }

        internal void UpdateChipTitle(string? title)
        {
            m_chip.Title = string.IsNullOrEmpty(title) ? Placeholder : title;
        }
        
        public partial void Open()
        {
            if (Mode == PickerMode.BottomSheet)
            {
                OpenBottomSheet();
            }
        }

        protected override void OnHandlerChanging(HandlerChangingEventArgs args)
        {
            base.OnHandlerChanging(args);
            
            LayoutContent();

            if (args.NewHandler == null)
            {
                if (ItemsSource is INotifyCollectionChanged notifyCollectionChanged)
                    notifyCollectionChanged.CollectionChanged -= OnCollectionChanged;
            }
        }

        private static void SelectedItemChanged(BindableObject bindable, object oldValue, object newValue)
        {
            if (bindable is not ItemPicker picker)
            {
                return;
            }
            
            picker.SelectedItemCommand?.Execute(picker.SelectedItem);
            picker.DidSelectItem?.Invoke(picker, picker.SelectedItem);

            var displayName = picker.SelectedItem?.GetPropertyValue(picker.ItemDisplayProperty) ?? null;
            picker.UpdateChipTitle(displayName);
            
            switch (picker.Mode)
            {
                case PickerMode.ContextMenu:
                    UpdateContextMenuItems(picker); //<-- Needed if the selected item was set programatically, and not by the user
                    break;
                case PickerMode.BottomSheet:
                    if (BottomSheetService.IsBottomSheetOpen())
                    {
                        BottomSheetService.CloseCurrentBottomSheet();    
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
        }

        private void OnCollectionChanged(object? sender, NotifyCollectionChangedEventArgs notifyCollectionChangedEventArgs)
        {
            AddContextMenuItems();
        }

        private object? GetItemFromDisplayProperty(string toCompare)
        {
            if (ItemsSource == null)
            {
                return null;
            }

            return ItemsSource.Cast<object?>().FirstOrDefault(item => toCompare.Equals(item.GetPropertyValue(ItemDisplayProperty), StringComparison.InvariantCulture));
        }
    }
}