using System.Threading.Tasks;
using DIPS.Mobile.UI.Components.BottomSheet;
using Xamarin.Forms.Platform.Android;

namespace DIPS.Mobile.UI.Droid.Components.BottomSheet
{
    public class BottomSheet : IBottomSheet
    {
        private readonly BottomSheetView m_bottomSheetView;
        private BottomSheetFragment m_modalBottomSheet;

        public BottomSheet(BottomSheetView bottomSheetView)
        {
            m_bottomSheetView = bottomSheetView;
        }

        public Task Open()
        {
            var context = DUI.Context;
            m_modalBottomSheet = new BottomSheetFragment(context, m_bottomSheetView);
            m_modalBottomSheet.Show(context.GetFragmentManager(), nameof(BottomSheetFragment));
            return Task.CompletedTask;
        }

        public Task Close()
        {
            m_modalBottomSheet.Dismiss();
            return Task.CompletedTask;
        }
    }
}