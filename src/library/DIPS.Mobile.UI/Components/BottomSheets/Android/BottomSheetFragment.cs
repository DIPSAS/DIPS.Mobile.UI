using Android.App;
using Android.Content;
using Android.OS;
using Android.Views;
using Android.Widget;
using AndroidX.Core.View;
using DIPS.Mobile.UI.API.Library;
using DIPS.Mobile.UI.MemoryManagement;
using Google.Android.Material.BottomSheet;
using Microsoft.Maui.Platform;
using AView = Android.Views.View;

namespace DIPS.Mobile.UI.Components.BottomSheets.Android
{
    public class BottomSheetFragment : BottomSheetDialogFragment
    {
        private readonly BottomSheet m_bottomSheet;
        private TaskCompletionSource<bool> m_showTaskCompletionSource;
        private BottomSheetBehavior? m_bottomSheetBehavior;
        private TaskCompletionSource<bool> m_dismissTaskCompletionSource;

        private List<InputView> m_attachedInputViews = new();
        private Positioning m_bottomSheetPositioningBeforeFocusedInputView;

        public BottomSheetFragment(BottomSheet bottomSheet)
        {
            m_bottomSheet = bottomSheet;
            m_showTaskCompletionSource = new TaskCompletionSource<bool>();
            m_dismissTaskCompletionSource = new TaskCompletionSource<bool>();

            m_bottomSheet.OnPositioningChanged += OnBottomSheetPositioningChanged;
        }

        public override AView OnCreateView(LayoutInflater inflater, ViewGroup? container, Bundle? savedInstanceState)
        {
            var mauiContext = DUI.GetCurrentMauiContext;
            var errorView = new AView(Context);
            if (mauiContext == null) return errorView;

            if (Dialog is not BottomSheetDialog bottomSheetDialog) return errorView;
            if (m_bottomSheetBehavior == null) return errorView;
            if (Context == null) return errorView;

            var rootLayout = new RelativeLayout(Context)
            {
                LayoutParameters =
                    new RelativeLayout.LayoutParams(ViewGroup.LayoutParams.MatchParent,
                        ViewGroup.LayoutParams.WrapContent),
            };
            
            rootLayout.SetBackgroundColor(m_bottomSheet.BackgroundColor.ToPlatform());

            var bottomSheetLayout = new LinearLayout(Context)
            {
                LayoutParameters = new LinearLayout.LayoutParams(ViewGroup.LayoutParams.MatchParent,
                    ViewGroup.LayoutParams.WrapContent),
                Orientation = Orientation.Vertical
            };

            rootLayout.AddView(bottomSheetLayout);
            
            m_bottomSheet.RootLayout = rootLayout;
            m_bottomSheet.BottomSheetDialog = bottomSheetDialog;
            m_bottomSheet.BottomSheetBehavior = m_bottomSheetBehavior;

            var bottomSheetView = m_bottomSheet.ToPlatform(mauiContext); //Triggers handler creation
            
            if (m_bottomSheet.Handler is not BottomSheetHandler bottomSheetHandler) return errorView;
            if (m_bottomSheetBehavior == null) return errorView;

            return bottomSheetHandler.OnBeforeOpening(mauiContext, Context, bottomSheetView, rootLayout, bottomSheetLayout);
        }

        public override Dialog OnCreateDialog(Bundle? savedInstanceState)
        {
            var activity = Platform.CurrentActivity;
            var dialog = base.OnCreateDialog(savedInstanceState);

            if (activity is null) return dialog;

            if (dialog is BottomSheetDialog bottomSheetDialog)
            {
                m_bottomSheetBehavior = bottomSheetDialog.Behavior;
                
                var displayMetrics = Resources.DisplayMetrics;
                var screenHeight = displayMetrics.HeightPixels;

                // Calculate the peek height as 50% of the screen height
                var peekHeight = (int)(0.5 * screenHeight);
                m_bottomSheetBehavior.SetPeekHeight(peekHeight, false);
            }

            var window = activity.Window;
            if (window is
                {
                    Attributes: not null
                }) //Make sure the dialog inherits window flag from the activity, useful when the activity is set as secured.
            {
                var flags = window.Attributes.Flags;
                dialog.Window?.SetFlags(flags, flags);
            }

            // Fix status bar color by finding and coloring Android's statusBarBackground view (API 30+)
            if (OperatingSystem.IsAndroidVersionAtLeast(30) && dialog.Window is not null)
            {
                // Enable edge-to-edge mode
                WindowCompat.SetDecorFitsSystemWindows(dialog.Window, false);
                
                // Set status bar icons light/dark based on background luminosity
                var windowInsetsController = WindowCompat.GetInsetsController(dialog.Window, dialog.Window.DecorView);
                windowInsetsController.AppearanceLightStatusBars = m_bottomSheet.BackgroundColor.GetLuminosity() > 0.5;
                
                // Find and color Android's statusBarBackground view that Material creates
                dialog.SetOnShowListener(new DialogShowListener(dialog, m_bottomSheet.BackgroundColor));
            }

            return dialog;
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
            m_dismissTaskCompletionSource = new TaskCompletionSource<bool>();
            Show(fragmentManager, nameof(BottomSheetFragment));
            
            return m_showTaskCompletionSource.Task;
        }

        public override void OnStart()
        {
            m_bottomSheet.SendOpen();
            base.OnStart();
        }

        public override void OnDestroy()
        {
            base.OnDestroy();
            
            
            m_bottomSheet.SendClose();
            BottomSheetService.RemoveFromStack(m_bottomSheet);
            m_bottomSheet.DisconnectHandlers();
            _ = GCCollectionMonitor.Instance.CheckIfObjectIsAliveAndTryResolveLeaks(m_bottomSheet.ToCollectionContentTarget());

            foreach (var inputView in m_attachedInputViews)
            {
                inputView.Focused -= OnInputViewFocused;
            }

            m_dismissTaskCompletionSource.SetResult(true);
            m_bottomSheet.OnPositioningChanged -= OnBottomSheetPositioningChanged;
        }

        public Task Close(bool animated)
        {
            Dismiss();
            return m_dismissTaskCompletionSource.Task;
        }

        public void AttachInputView(InputView inputView)
        {
            m_attachedInputViews.Add(inputView);

            inputView.Focused += OnInputViewFocused;
        }

        private void OnInputViewFocused(object? sender, FocusEventArgs e)
        {
            m_bottomSheetPositioningBeforeFocusedInputView = m_bottomSheet.Positioning;
            m_bottomSheet.Positioning = Positioning.Large;

            ((sender as InputView)!).Unfocused += OnInputViewUnFocused;
        }

        private void OnInputViewUnFocused(object? sender, FocusEventArgs e)
        {
            m_bottomSheet.Positioning = m_bottomSheetPositioningBeforeFocusedInputView;

            ((sender as InputView)!).Unfocused -= OnInputViewUnFocused;
        }
        
        private void OnBottomSheetPositioningChanged(Positioning positioning)
        {
            if (positioning != Positioning.Medium)
                return;

            foreach (var inputView in m_attachedInputViews)
            {
                inputView.Unfocused -= OnInputViewUnFocused;
                
                if(inputView.IsFocused)
                    inputView.Unfocus();
            }
        }
    }

    /// <summary>
    /// Listener that colors Android's statusBarBackground view and removes bottom padding
    /// </summary>
    internal class DialogShowListener : Java.Lang.Object, IDialogInterfaceOnShowListener
    {
        private readonly Dialog _dialog;
        private readonly Microsoft.Maui.Graphics.Color _backgroundColor;

        public DialogShowListener(Dialog dialog, Microsoft.Maui.Graphics.Color backgroundColor)
        {
            _dialog = dialog;
            _backgroundColor = backgroundColor;
        }

        public void OnShow(IDialogInterface? dialog)
        {
            if (_dialog.Window is null) return;

            var platformColor = _backgroundColor.ToPlatform();

            // Material sets window.setStatusBarColor(0) in onCreate, making it transparent.
            // So we must color Android's statusBarBackground view (created by DecorView) instead.
            // This is the view that shows through the transparent status bar in edge-to-edge mode.
            var statusBarBackground = _dialog.Window.DecorView.FindViewById(global::Android.Resource.Id.StatusBarBackground);
            statusBarBackground?.SetBackgroundColor(platformColor);

            // Find design_bottom_sheet and remove bottom padding
            var bottomSheet = _dialog.FindViewById(Resource.Id.design_bottom_sheet);
            if (bottomSheet is not null)
            {
                bottomSheet.SetPadding(
                    bottomSheet.PaddingLeft,
                    bottomSheet.PaddingTop,
                    bottomSheet.PaddingRight,
                    0
                );
            }
        }
    }
}