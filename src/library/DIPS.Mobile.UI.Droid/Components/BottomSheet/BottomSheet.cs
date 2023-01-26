using System.Threading.Tasks;
using DIPS.Mobile.UI.Components.BottomSheet;
using Google.Android.Material.BottomSheet;
using Java.Lang;
using Xamarin.Forms.Platform.Android;

namespace DIPS.Mobile.UI.Droid.Components.BottomSheet
{
    public class BottomSheet : IBottomSheet
    {
        private readonly BottomSheetView m_bottomSheetView;
        private BottomSheetFragment m_modalBottomSheetFragment;

        public BottomSheet(BottomSheetView bottomSheetView)
        {
            m_bottomSheetView = bottomSheetView;
        }

        public Task Open()
        {
            var context = DUI.Context;
            m_modalBottomSheetFragment = new BottomSheetFragment(context, m_bottomSheetView);
            m_modalBottomSheetFragment.Show(context.GetFragmentManager(), nameof(BottomSheetFragment));
            return Task.CompletedTask;
        }

        public Task Close()
        {
            m_modalBottomSheetFragment.Dismiss();
            return Task.CompletedTask;
        }
    }
}