using Android.Content;
using Android.OS;
using Android.Views;
using Google.Android.Material.BottomSheet;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;
using View = Xamarin.Forms.View;

namespace DIPS.Mobile.UI.Droid.Components.BottomSheet
{
    internal class BottomSheetFragment : BottomSheetDialogFragment
    {
        private readonly Context m_context;
        private readonly View m_view;

        public BottomSheetFragment(Context context, View view)
        {
            m_context = context;
            m_view = view;
        }

        public override global::Android.Views.View OnCreateView(LayoutInflater inflater, ViewGroup container,
            Bundle savedInstanceState)
        {
            //TODO: Check if we can use the entire content page as view, and not just ContentPage.Content
            return new ContainerView(m_context, m_view);
        }
    }
}