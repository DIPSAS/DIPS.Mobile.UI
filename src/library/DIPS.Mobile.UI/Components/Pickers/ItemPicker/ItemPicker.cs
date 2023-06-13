using DIPS.Mobile.UI.Components.BottomSheets;
using DIPS.Mobile.UI.Components.Chips;
using DIPS.Mobile.UI.Components.ContextMenus;

namespace DIPS.Mobile.UI.Components.Pickers.ItemPicker
{
    public partial class ItemPicker : ContentView
    {
        private readonly ContextMenu m_contextMenu = new ();

        private void LayoutContent()
        {
            var chip = new Chip();
            chip.SetBinding(Chip.TitleProperty, new Binding(){Path = nameof(Placeholder), Source = this});
            Content = chip;
            
            if (Mode == PickerMode.ContextMenu)
            {
                ContextMenuEffect.SetMenu(chip, m_contextMenu);
                m_contextMenu.ItemClickedCommand = new Command<ContextMenuItem>(SetSelectedItemBasedOnContextMenuItem);
            }
            else if (Mode == PickerMode.BottomSheet)
            {
                chip.Command = new Command(() => BottomSheetService.OpenBottomSheet(new ItemPickerBottomSheet(this)));
            }
        }

        protected override void OnHandlerChanging(HandlerChangingEventArgs args)
        {
            base.OnHandlerChanging(args);
            
            LayoutContent();
        }

        private static void SelectedItemChanged(BindableObject bindable, object oldValue, object newValue)
        {
            if (bindable is not ItemPicker picker)
            {
                return;
            }

            if (picker.SelectedItem == null)
            {
                return;
            }

            picker.Placeholder = picker.SelectedItem.GetPropertyValue(picker.ItemDisplayProperty)!;
            picker.SelectedItemCommand?.Execute(picker.SelectedItem);
            picker.DidSelectItem?.Invoke(picker, picker.SelectedItem);

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
        }

        private object? GetItemFromDisplayProperty(string toCompare)
        {
            if (ItemsSource == null)
            {
                return null;
            }

            var theItem = ItemsSource.FirstOrDefault(i =>
                toCompare.Equals(i.GetPropertyValue(ItemDisplayProperty), StringComparison.InvariantCulture));
            return theItem ?? null;
        }
    }
}