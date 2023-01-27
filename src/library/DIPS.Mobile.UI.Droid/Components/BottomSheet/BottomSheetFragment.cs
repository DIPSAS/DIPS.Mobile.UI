using System;
using Android.App;
using Android.Content;
using Android.OS;
using Android.Views;
using Android.Widget;
using AndroidX.CoordinatorLayout.Widget;
using AndroidX.Core.Widget;
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
            var nestedScrollView = new NestedScrollView(m_context); //Required to make sure the sheet scrolls when there is a scrollable content added to it.
            nestedScrollView.AddView(new ContainerView(m_context, m_bottomSheetView));
            return nestedScrollView;
        }

        public override Dialog OnCreateDialog(Bundle savedInstanceState)
        {
            var dialog = base.OnCreateDialog(savedInstanceState);

            if (dialog is BottomSheetDialog bottomSheetDialog)
            {
                bottomSheetDialog.Behavior.Draggable = true;
                var fullScreenHeight = m_context.Resources.DisplayMetrics.HeightPixels;
                bottomSheetDialog.Behavior.PeekHeight = fullScreenHeight/2;
            }

            return dialog;
        }
    }
}