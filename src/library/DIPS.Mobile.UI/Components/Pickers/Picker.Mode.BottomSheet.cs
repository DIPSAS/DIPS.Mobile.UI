using DIPS.Mobile.UI.Components.BottomSheets;
using Xamarin.Forms;

namespace DIPS.Mobile.UI.Components.Pickers
{
    public partial class Picker
    {
        private void AttachBottomSheet()
        {
            GestureRecognizers.Add(new TapGestureRecognizer() {Command = (new Command(() =>  _ = Application.Current.PushBottomSheet(new PickerBottomSheet(this))))});
        }
    }
}