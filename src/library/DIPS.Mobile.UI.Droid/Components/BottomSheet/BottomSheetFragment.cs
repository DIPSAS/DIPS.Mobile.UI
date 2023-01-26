using System;
using Android.App;
using Android.Content;
using Android.OS;
using Android.Views;
using Android.Widget;
using AndroidX.CoordinatorLayout.Widget;
using DIPS.Mobile.UI.Components.BottomSheet;
using Google.Android.Material.BottomSheet;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;

namespace DIPS.Mobile.UI.Droid.Components.BottomSheet
{
    internal class BottomSheetFragment : BottomSheetDialogFragment
    {
        private readonly Context m_context;
        private readonly BottomSheetView m_bottomSheetView;

        public BottomSheetFragment(Context context, BottomSheetView bottomSheetView)
        {
            m_context = context;
            m_bottomSheetView = bottomSheetView;
        }

        public override Android.Views.View OnCreateView(LayoutInflater inflater, ViewGroup container,
            Bundle savedInstanceState)
        {
            var resourceId = DUI.GetResourceId("bottom_sheet");
            if (resourceId != null)
            {
                return inflater.Inflate((int)resourceId, container, false);
            }
            return new TextView(m_context) {Text = "Asd"};
        }

        public override Dialog OnCreateDialog(Bundle savedInstanceState)
        {
            var dialog = base.OnCreateDialog(savedInstanceState);

            if (dialog is BottomSheetDialog bottomSheetDialog)
            {
                bottomSheetDialog.Behavior.Draggable = true;
                bottomSheetDialog.Behavior.PeekHeight = 500;
            }

            return dialog;
        }
    }
}