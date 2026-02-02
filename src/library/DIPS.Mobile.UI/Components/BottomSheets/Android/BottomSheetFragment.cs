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

namespace DIPS.Mobile.UI.Components.BottomSheets.Android;

public class BottomSheetFragment : BottomSheetDialogFragment
{
    private readonly BottomSheet m_bottomSheet;
    private TaskCompletionSource<bool> m_showTaskCompletionSource;
    private BottomSheetBehavior? m_bottomSheetBehavior;
    private TaskCompletionSource<bool> m_dismissTaskCompletionSource;
    private AView? m_rootLayout;
    private LinearLayout? m_bottomSheetLayout;
    private int m_statusBarHeight;

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
        m_rootLayout = rootLayout;
            
        // Get status bar height from window insets
        ViewCompat.SetOnApplyWindowInsetsListener(rootLayout, new OnApplyWindowInsetsListener(this));

        var bottomSheetLayout = new LinearLayout(Context)
        {
            LayoutParameters = new LinearLayout.LayoutParams(ViewGroup.LayoutParams.MatchParent,
                ViewGroup.LayoutParams.WrapContent),
            Orientation = Orientation.Vertical
        };
        m_bottomSheetLayout = bottomSheetLayout;

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
            // Add callback to handle edge-to-edge padding during slide
            m_bottomSheetBehavior.AddBottomSheetCallback(new EdgeToEdgeBottomSheetCallback(this));
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
            
        // Fix for edge-to-edge: Remove top insets so BottomSheet can draw behind status bar
        // See: https://github.com/material-components/material-components-android/issues/3389
        Dialog?.Window?.AddFlags(WindowManagerFlags.LayoutNoLimits);
            
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

            if (inputView.IsFocused)
                inputView.Unfocus();
        }
    }

    private class OnApplyWindowInsetsListener : Java.Lang.Object, IOnApplyWindowInsetsListener
    {
        private readonly BottomSheetFragment _fragment;
        
        public OnApplyWindowInsetsListener(BottomSheetFragment fragment)
        {
            _fragment = fragment;
        }
        
        public WindowInsetsCompat OnApplyWindowInsets(AView v, WindowInsetsCompat insets)
        {
            var statusBarInsets = insets.GetInsets(WindowInsetsCompat.Type.StatusBars());
            _fragment.m_statusBarHeight = statusBarInsets.Top;
            return insets;
        }
    }
    
    private class EdgeToEdgeBottomSheetCallback : BottomSheetBehavior.BottomSheetCallback
    {
        private readonly BottomSheetFragment _fragment;
        
        public EdgeToEdgeBottomSheetCallback(BottomSheetFragment fragment)
        {
            _fragment = fragment;
        }
        
        public override void OnSlide(AView bottomSheet, float slideOffset)
        {
            if (_fragment.m_bottomSheetLayout == null || _fragment.m_statusBarHeight == 0)
                return;
            
            // Get the current location of the bottom sheet
            var location = new int[2];
            bottomSheet.GetLocationOnScreen(location);
            var bottomSheetTop = location[1];
            
            // Calculate how much the sheet overlaps with the status bar
            // If bottomSheetTop is less than statusBarHeight, we're behind the status bar
            var overlap = Math.Max(0, _fragment.m_statusBarHeight - bottomSheetTop);
            
            // Apply equal top and bottom padding to maintain total height and prevent VerticalOptions.End from being pushed
            _fragment.m_bottomSheetLayout.SetPadding(0, overlap, 0, overlap);
        }
        
        public override void OnStateChanged(AView bottomSheet, int newState)
        {
            // No action needed on state change
        }
    }
}