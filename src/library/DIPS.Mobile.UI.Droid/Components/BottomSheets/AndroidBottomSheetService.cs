using System.Threading.Tasks;
using Android.App;
using DIPS.Mobile.UI.Components.BottomSheets;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;

namespace DIPS.Mobile.UI.Droid.Components.BottomSheets
{
    internal class AndroidBottomSheetService : IBottomSheetService
    {
        internal const string BottomSheetFragmentTag = nameof(BottomSheetFragment);
        public Task OpenBottomSheet(BottomSheet bottomSheet) => new BottomSheetFragment(bottomSheet).Show();
        public Task CloseCurrentBottomSheet()
        {
            var currentBottomSheetFragment = CurrentBottomSheetFragment();
            currentBottomSheetFragment?.Dismiss();
            return Task.CompletedTask;
        }

        public bool IsBottomSheetOpen()
        {
            return CurrentBottomSheetFragment() != null;
        }

        private static BottomSheetFragment? CurrentBottomSheetFragment()
        {
            var fragment = ((Activity)DUI.Context).GetFragmentManager().FindFragmentByTag(BottomSheetFragmentTag);
            if (fragment is BottomSheetFragment bottomSheetFragment)
            {
                return bottomSheetFragment;
            }

            return null;
        }
    }
}