using System.Threading.Tasks;
using DIPS.Mobile.UI.Components.BottomSheets;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;

namespace DIPS.Mobile.UI.Droid.Components.BottomSheets
{
    internal class AndroidBottomSheetService : IBottomSheetService
    {
        public Task PushBottomSheet(BottomSheet bottomSheet) => new BottomSheetFragment(bottomSheet).Show();
    }
}