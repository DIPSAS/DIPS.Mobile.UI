using Android.App;
using Android.Content.Res;
using Android.OS;
using Android.Views;
using Android.Widget;
using DIPS.Mobile.UI.API.Library;
using Google.Android.Material.BottomSheet;
using Microsoft.Maui.Platform;
using Grid = Microsoft.Maui.Controls.Grid;
using AView = Android.Views.View;
using Colors = DIPS.Mobile.UI.Resources.Colors.Colors;
using Orientation = Android.Widget.Orientation;

namespace DIPS.Mobile.UI.Components.BottomSheets.Android
{
    internal class BottomSheetFragment : BottomSheetDialogFragment
    {
        private readonly BottomSheet m_bottomSheet;
        private TaskCompletionSource<bool> m_showTaskCompletionSource;
        private BottomSheetBehavior? m_bottomSheetBehavior;

        public BottomSheetFragment(BottomSheet bottomSheet)
        {
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

       public override AView OnCreateView(LayoutInflater inflater, ViewGroup? container, Bundle? savedInstanceState)
       {
           var mauiContext = DUI.GetCurrentMauiContext;

           var linearLayout = new LinearLayout(Context)
           {
               LayoutParameters =
                   new LinearLayout.LayoutParams(ViewGroup.LayoutParams.MatchParent,
                       ViewGroup.LayoutParams.MatchParent),
               Orientation = Orientation.Vertical
           };

           //Add a handle, with a innerGrid that works as a big hit box for the user to hit
           //Inspired by com.google.android.material.bottomsheet.BottomSheetDragHandleView , which will be added in Xamarin Android Material Design v1.7.0.  https://github.com/material-components/material-components-android/commit/ac7b761294808748df167b50b223b591ca9dac06
           if (m_bottomSheetBehavior!.Draggable)
           {
               var innerGrid = new Grid {Padding = new Thickness(0,  Sizes.GetSize(SizeName.size_2))};
               innerGrid.GestureRecognizers.Add(new TapGestureRecognizer()
               {
                   Command = new Command(ToggleBottomSheetIfPossible)
               });
               var handle = new BoxView()
               {
                   HeightRequest = 4,
                   WidthRequest = 32,
                   CornerRadius = 10,
                   BackgroundColor = Colors.GetColor(ColorName.color_neutral_40),
                   HorizontalOptions = LayoutOptions.Center,
                   VerticalOptions = LayoutOptions.Center
               };
               innerGrid.Add(handle);

               linearLayout.AddView(innerGrid.ToPlatform(mauiContext!));

           }

           if (m_bottomSheet.HasSearchBar)
           {
               linearLayout.AddView(m_bottomSheet.SearchBar.ToPlatform(mauiContext!));
           }

           linearLayout.AddView(m_bottomSheet.ToPlatform(mauiContext!));

           if (!m_bottomSheet.ShouldFitToContent)
           {
                // Add an empty view, where its dimensions is set to always match the parent so that the LinearLayout will always take up available space
                linearLayout.AddView(new AView(Context)
                {
                    LayoutParameters = new ViewGroup.LayoutParams(ViewGroup.LayoutParams.MatchParent, ViewGroup.LayoutParams.MatchParent)
                });
           }
           
            return linearLayout;
        }

        private void ToggleBottomSheetIfPossible()
        {
            if (Dialog is BottomSheetDialog bottomSheetDialog)
            {
                var bottomSheetBehavior = bottomSheetDialog.Behavior;
                var collapsed = bottomSheetDialog.Behavior.State == BottomSheetBehavior.StateCollapsed;
                bottomSheetBehavior.State =
                    collapsed ? BottomSheetBehavior.StateExpanded : BottomSheetBehavior.StateCollapsed;
            }
        }

        public override Dialog OnCreateDialog(Bundle? savedInstanceState)
        {
            var context = Platform.AppContext;
            var activity = Platform.CurrentActivity;
            var dialog = base.OnCreateDialog(savedInstanceState);
            
            if (activity is null) return dialog;
            
            if (dialog is BottomSheetDialog bottomSheetDialog)
            {
                m_bottomSheetBehavior = bottomSheetDialog.Behavior;
                bottomSheetDialog.Behavior.FitToContents = m_bottomSheet.ShouldFitToContent;
                
                if (!m_bottomSheet.ShouldFitToContent)
                {
                    var fullScreenHeight = context.Resources?.DisplayMetrics?.HeightPixels;
                    if (fullScreenHeight != null)
                    {
                        bottomSheetDialog.Behavior.PeekHeight = fullScreenHeight.Value / 2;
                    }    
                }
            }

            var window = activity.Window;
            if (window is {Attributes: not null}) //Make sure the dialog inherits window flag from the activity, useful when the activity is set as secured.
            {
                var flags = window.Attributes.Flags;
                dialog.Window?.SetFlags(flags, flags);
            }

            dialog.Window?.SetSoftInputMode(SoftInput.AdjustResize);

            return dialog;
        }

        private void Close(object? sender, EventArgs? e)
        {
            Close();
        }

        public void Close()
        {
            if (m_bottomSheetBehavior != null)
            {
                m_bottomSheetBehavior.State = BottomSheetBehavior.StateHidden;
            }

            Dismiss();
        }

        public override void OnCreate(Bundle? savedInstanceState)
        {
            m_showTaskCompletionSource.SetResult(true);
            base.OnCreate(savedInstanceState);
        }

        public Task Show()
        {
            var activity = Platform.CurrentActivity;
            var fragmentManager = activity?.GetFragmentManager();
            if (fragmentManager == null) return Task.CompletedTask;
            
            m_showTaskCompletionSource = new TaskCompletionSource<bool>();
            Show(fragmentManager, nameof(BottomSheetFragment));
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