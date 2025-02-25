using System.Windows.Input;
using DIPS.Mobile.UI.Components.BottomSheets;

namespace DIPS.Mobile.UI.Components.Pickers.ItemPicker
{
    public partial class ItemPicker
    {
        public BottomSheetPickerConfiguration BottomSheetPickerConfiguration { get; set; } = new();

        internal void OpenBottomSheet() => _ = BottomSheetService.Open(new ItemPickerBottomSheet(this));
    }
}