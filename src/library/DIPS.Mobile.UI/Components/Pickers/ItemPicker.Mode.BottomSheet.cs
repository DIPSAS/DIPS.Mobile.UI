using DIPS.Mobile.UI.Components.BottomSheets;

namespace DIPS.Mobile.UI.Components.Pickers
{
    public partial class ItemPicker
    {
        private void AttachBottomSheet()
        {
            var tapGestureRecognizer = new TapGestureRecognizer();
            tapGestureRecognizer.Tapped += delegate
            {
                _ = BottomSheetService.OpenBottomSheet(new PickerBottomSheet(this));
            };
            GestureRecognizers.Add(tapGestureRecognizer);
        }
    }
}