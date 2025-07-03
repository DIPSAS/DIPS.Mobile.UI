using System.Windows.Input;
using DIPS.Mobile.UI.Components.BottomSheets;

namespace DIPS.Mobile.UI.Components.Pickers.ItemPicker
{
    public partial class ItemPicker
    {
        private BottomSheetPickerConfiguration m_bottomSheetPickerConfiguration = new();

        public BottomSheetPickerConfiguration BottomSheetPickerConfiguration
        {
            get => m_bottomSheetPickerConfiguration;
            set
            {
                m_bottomSheetPickerConfiguration = value;
                m_bottomSheetPickerConfiguration.SetBinding(BindingContextProperty, static (ItemPicker itemPicker) => itemPicker.BindingContext, source: this);
            }
        }

        internal void OpenBottomSheet() => _ = BottomSheetService.Open(new ItemPickerBottomSheet(this));
    }
}