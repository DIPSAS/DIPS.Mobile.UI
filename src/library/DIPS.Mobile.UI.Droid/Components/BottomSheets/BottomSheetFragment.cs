using System;
using System.Threading.Tasks;
using Android.App;
using Android.Content;
using Android.OS;
using Android.Views;
using Android.Widget;
using AndroidX.CoordinatorLayout.Widget;
using AndroidX.Core.Widget;
using DIPS.Mobile.UI.Components.BottomSheets;
using DIPS.Mobile.UI.Resources.Colors;
using Google.Android.Material.BottomSheet;
using Google.Android.Material.Card;
using Google.Android.Material.Shape;
using Java.Util;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;
using Application = Xamarin.Forms.Application;
using FragmentManager = AndroidX.Fragment.App.FragmentManager;
using View = Android.Views.View;

namespace DIPS.Mobile.UI.Droid.Components.BottomSheets
{
    internal class BottomSheetFragment : BottomSheetDialogFragment
    {
        private readonly Context m_context;
        private readonly BottomSheet m_bottomSheet;
        private TaskCompletionSource<bool> m_showTaskCompletionSource;

        public BottomSheetFragment(BottomSheet bottomSheet)
        {
            m_context = DUI.Context;
            m_bottomSheet = bottomSheet;
            SubscribeEvents();
        }

        private void SubscribeEvents()
        {
            m_bottomSheet.WillClose += Close;
        }

        private void UnSubscribeEvents()
        {
            m_bottomSheet.WillClose -= Close;
        }

        public override View OnCreateView(LayoutInflater inflater, ViewGroup container,
            Bundle savedInstanceState)
        {
            var height = 0;
            var coordinatedLayout = new CoordinatorLayout(m_context);
            if (m_context.Resources?.DisplayMetrics != null)
            {
                height = m_context.Resources.DisplayMetrics.HeightPixels;
            }
            var nestedScrollView =
                new NestedScrollView(
                    m_context); //Required to make sure the sheet scrolls when there is a scrollable content added to it.
            nestedScrollView.SetMinimumHeight(height);
            nestedScrollView.AddView(new ContainerView(m_context, m_bottomSheet));
            return nestedScrollView;
        }

        public override Dialog OnCreateDialog(Bundle savedInstanceState)
        {
            var dialog = base.OnCreateDialog(savedInstanceState);
            if (dialog is BottomSheetDialog bottomSheetDialog)
            {
                bottomSheetDialog.Behavior.Draggable = true;
                if (m_context.Resources?.DisplayMetrics != null)
                {
                    var fullScreenHeight = m_context.Resources.DisplayMetrics.HeightPixels;
                    bottomSheetDialog.Behavior.PeekHeight = fullScreenHeight / 2;
                }
            }
            return dialog;
        }

        private void Close(object sender, EventArgs e) => Dismiss();

        public override void OnCreate(Bundle savedInstanceState)
        {
            m_showTaskCompletionSource.SetResult(true);
            base.OnCreate(savedInstanceState);
        }

        public Task Show()
        {
            m_showTaskCompletionSource = new TaskCompletionSource<bool>();
            Show(m_context.GetFragmentManager(), nameof(BottomSheetFragment));
            return m_showTaskCompletionSource.Task;
        }

        public override void OnDestroy()
        {
            UnSubscribeEvents();
            base.OnDestroy();
            m_bottomSheet.SendDidClose();
        }
    }
}