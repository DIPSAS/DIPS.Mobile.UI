using DIPS.Mobile.UI.Components.ContextMenus;
using DIPS.Mobile.UI.Extensions;
using Picker = DIPS.Mobile.UI.Components.Pickers.Base.Picker;

namespace DIPS.Mobile.UI.Components.Pickers
{
    //TODO: Make sure its accessable
    public partial class ItemPicker : Picker
    {
        private bool m_layedOut;
        private readonly ContextMenu m_contextMenu = new ();

        private void LayoutContent()
        {
            if (Mode == PickerMode.ContextMenu)
            {
                Content!.InputTransparent = true;
                ContextMenuEffect.SetMenu(this, m_contextMenu);
                m_contextMenu.ItemClickedCommand = new Command<ContextMenuItem>(SetSelectedItemBasedOnContextMenuItem);
            }
            else if (Mode == PickerMode.BottomSheet)
            {
                AttachBottomSheet();
            }

            m_layedOut = true;
        }

        protected override void OnPropertyChanged(string propertyName = null)
        {
            if (propertyName.Equals(nameof(Parent)))
            {
                if (!m_layedOut)
                {
                    LayoutContent();
                }
            }

            base.OnPropertyChanged(propertyName);
        }

        private static void SelectedItemChanged(BindableObject bindable, object oldvalue, object newvalue)
        {
            if (bindable is not ItemPicker picker)
            {
                return;
            }

            if (picker.SelectedItem == null)
            {
                return;
            }

            picker.m_pickedItemLabel.Text =
                picker.SelectedItem.GetPropertyValue(picker.ItemDisplayProperty);
            picker.SelectedItemCommand?.Execute(picker.SelectedItem);
            picker.DidSelectItem?.Invoke(picker, picker.SelectedItem);

            if (picker.Mode == PickerMode.ContextMenu)
            {
                UpdateContextMenuItems(
                    picker); //<-- Needed if the selected item was set programatically, and not by the user    
            }
        }

        private static void ItemsSourceChanged(BindableObject bindable, object oldvalue, object newvalue)
        {
            if (bindable is not ItemPicker picker)
            {
                return;
            }

            if (picker.Mode == PickerMode.ContextMenu)
            {
                AddContextMenuItems(picker);
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