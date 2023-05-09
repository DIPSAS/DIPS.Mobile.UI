using DIPS.Mobile.UI.Components.BottomSheets;

namespace DIPS.Mobile.UI.Components.Pickers.ItemPicker
{
    public partial class ItemPicker
    {
        private void AttachBottomSheet()
        {
            GestureRecognizers.Add(new TapGestureRecognizer
            {
                Command = new Command(_ => BottomSheetService.OpenBottomSheet(new PickerBottomSheet(this)))
            });
        }
    }
}