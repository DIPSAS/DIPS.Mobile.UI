using System;
using System.Threading.Tasks;
using Android.App;
using Android.Content;
using Android.OS;
using Android.Views;
using AndroidX.Core.Widget;
using DIPS.Mobile.UI.Components.BottomSheets;
using DIPS.Mobile.UI.Resources.Colors;
using Google.Android.Material.BottomSheet;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;
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
            m_showTaskCompletionSource = new TaskCompletionSource<bool>();
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
            if (m_context.Resources?.DisplayMetrics != null)
            {
                height = m_context.Resources.DisplayMetrics.HeightPixels;
            }

            var nestedScrollView =
                new NestedScrollView(
                    m_context); //Required to make sure the sheet scrolls when there is a scrollable content added to it.
            nestedScrollView.SetMinimumHeight(height);
            var grid = new Grid()
            {
                RowSpacing = 0,
                RowDefinitions = new RowDefinitionCollection()
                {
                    new() {Height = GridLength.Auto}, new() {Height = GridLength.Star}
                }
            };

            //Add a handle, with a innerGrid that works as a big hit box for the user to hit
            //Inspired by com.google.android.material.bottomheet.BottomSheetDragHandleView , which will be added in Xamarin Android Material Design v1.7.0.  https://github.com/material-components/material-components-android/commit/ac7b761294808748df167b50b223b591ca9dac06
            var innerGrid = new Grid(){Padding = new Thickness(0,10)};
            innerGrid.GestureRecognizers.Add(new TapGestureRecognizer(){Command = new Command(ToggleBottomSheetIfPossible)});
            var handle = new BoxView()
            {
                HeightRequest = 4,
                WidthRequest = 32,
                CornerRadius = 10,
                BackgroundColor = Colors.GetColor(ColorName.color_neutral_40),
                HorizontalOptions = LayoutOptions.Center,
                VerticalOptions = LayoutOptions.Center
            };
            innerGrid.Children.Add(handle);
            grid.Children.Add(innerGrid, 0, 0);
            grid.Children.Add(m_bottomSheet, 0, 1);
            
            nestedScrollView.AddView(new ContainerView(m_context, grid));
            
            return nestedScrollView;
        }

        private void ToggleBottomSheetIfPossible()
        {
            if (Dialog is BottomSheetDialog bottomSheetDialog)
            {
                var bottomSheetBehavior = bottomSheetDialog.Behavior;
                var  collapsed = bottomSheetDialog.Behavior.State == BottomSheetBehavior.StateCollapsed;
                bottomSheetBehavior.State =
                    collapsed ? BottomSheetBehavior.StateExpanded : BottomSheetBehavior.StateCollapsed;
            }
            
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
            
            dialog.Window?.SetSoftInputMode(SoftInput.AdjustResize);

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