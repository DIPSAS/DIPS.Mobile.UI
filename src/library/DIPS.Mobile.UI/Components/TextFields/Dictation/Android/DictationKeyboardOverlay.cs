using Android.App;
using Android.Graphics.Drawables;
using Android.OS;
using Android.Views;
using Android.Widget;
using AndroidX.Core.View;
using DIPS.Mobile.UI.API.Library;
using AView = Android.Views.View;
using Drawable = Android.Graphics.Drawables.Drawable;
using ImageButton = DIPS.Mobile.UI.Components.Images.ImageButton.ImageButton;
using ShapeableImageView = Google.Android.Material.ImageView.ShapeableImageView;

namespace DIPS.Mobile.UI.Components.TextFields.Dictation;

/// <summary>
/// Shows the dictation microphone in its own window just above the keyboard. It follows the window of the focused
/// field, so it stays on top when the field is in a modal or a bottom sheet.
/// </summary>
internal sealed partial class DictationKeyboardOverlay
{
    public static DictationKeyboardOverlay Current { get; } = new();

    private Activity? m_activity;
    private ViewGroup? m_hostWindowContentView;
    private AView? m_hostWindowDecorView;
    private PopupWindow? m_popup;
    private ImageButton? m_microphoneImageButton;
    private ShapeableImageView? m_imageView;
    private FrameLayout? m_containerView;
    private GradientDrawable? m_stopSquareDrawable;
    private Drawable? m_microphoneIconDrawable;
    private int m_stopSquareSizePixels;
    private KeyboardInsetsListener? m_insetsListener;
    private ActivityDestroyedWatcher? m_activityWatcher;
    private Handler? m_mainThreadHandler;
    private Java.Lang.Runnable? m_beginFadeOutRunnable;
    private Java.Lang.Runnable? m_dismissRunnable;

    private int m_bottomMarginPixels;
    private int m_rightMarginPixels;
    private int m_keyboardBottomInset;
    private int m_navigationBarInset;
    private bool m_isKeyboardVisible;
    private int m_lastYOffset = int.MinValue;
    private bool m_isDictationActive;

    private DictationKeyboardOverlay()
    {
    }

    /// <summary>Shows the microphone in the window of the field that just gained focus.</summary>
    public void AttachToFocusedField(AView? focusedFieldView)
    {
        if (focusedFieldView is null)
            return;

        var activity = Platform.CurrentActivity;
        var mauiContext = DUI.GetCurrentMauiContext;
        if (activity is null || mauiContext is null)
            return;

        var hostWindowDecorView = focusedFieldView.RootView;
        var hostWindowContentView = hostWindowDecorView?.FindViewById<ViewGroup>(global::Android.Resource.Id.Content);
        if (hostWindowDecorView is null || hostWindowContentView is null)
            return;

        if (!ReferenceEquals(m_activity, activity))
            RebuildForActivity(activity, mauiContext);

        BindToHostWindow(hostWindowContentView, hostWindowDecorView);
    }

    private void RebuildForActivity(Activity activity, IMauiContext mauiContext)
    {
        if (m_activity is not null)
            DetachFromActivity();

        var density = activity.Resources?.DisplayMetrics?.Density ?? 1f;

        m_activity = activity;
        m_bottomMarginPixels = (int)(BottomMarginDp * density);
        m_rightMarginPixels = (int)(RightMarginDp * density);

        CreateMicrophonePopup(activity, mauiContext, density);

        CreateKeyboardTracking();

        StartWatchingForActivityDestroyed(activity);

        ObserveDictationTarget();
    }

    // A popup stays on the window it opened on, so moving to a new window means releasing the old one and reopening.
    private void BindToHostWindow(ViewGroup hostWindowContentView, AView hostWindowDecorView)
    {
        if (ReferenceEquals(m_hostWindowContentView, hostWindowContentView))
            return;

        ReleaseHostWindow();

        m_hostWindowContentView = hostWindowContentView;
        m_hostWindowDecorView = hostWindowDecorView;

        if (m_insetsListener is not null)
            ViewCompat.SetOnApplyWindowInsetsListener(hostWindowContentView, m_insetsListener);

        // Refresh now in case the keyboard is already open.
        ViewCompat.RequestApplyInsets(hostWindowContentView);
    }

    private void ReleaseHostWindow()
    {
        CancelPendingFade();

        if (m_hostWindowContentView is not null)
            ViewCompat.SetOnApplyWindowInsetsListener(m_hostWindowContentView, null);

        if (m_popup is { IsShowing: true })
            TryDismiss(m_popup);

        m_hostWindowContentView = null;
        m_hostWindowDecorView = null;
        m_lastYOffset = int.MinValue;
        m_isKeyboardVisible = false;
    }

    private void ObserveDictationTarget()
    {
        DictationSessionCoordinator.Current.CurrentConsumerChanged += OnCurrentConsumerChanged;
    }

    /// <summary>Removes the overlay, its listeners and popup, and disconnects the hosted button's handler.</summary>
    public void DetachFromActivity()
    {
        ReleaseHostWindow();
        DictationSessionCoordinator.Current.CurrentConsumerChanged -= OnCurrentConsumerChanged;
        StopWatchingForActivityDestroyed();

        m_microphoneImageButton?.Handler?.DisconnectHandler();

        m_popup = null;
        m_microphoneImageButton = null;
        m_imageView = null;
        m_containerView = null;
        m_stopSquareDrawable = null;
        m_microphoneIconDrawable = null;
        m_insetsListener = null;
        m_activity = null;
        m_mainThreadHandler = null;
        m_beginFadeOutRunnable = null;
        m_dismissRunnable = null;
        m_isDictationActive = false;
    }

    private void StartWatchingForActivityDestroyed(Activity activity)
    {
        var watcher = new ActivityDestroyedWatcher(this);
        activity.Application?.RegisterActivityLifecycleCallbacks(watcher);
        m_activityWatcher = watcher;
    }

    private void StopWatchingForActivityDestroyed()
    {
        if (m_activityWatcher is null)
            return;

        m_activity?.Application?.UnregisterActivityLifecycleCallbacks(m_activityWatcher);
        m_activityWatcher = null;
    }

    private sealed class ActivityDestroyedWatcher : Java.Lang.Object, Android.App.Application.IActivityLifecycleCallbacks
    {
        private readonly DictationKeyboardOverlay m_overlay;

        public ActivityDestroyedWatcher(DictationKeyboardOverlay overlay)
        {
            m_overlay = overlay;
        }

        public void OnActivityDestroyed(Activity activity)
        {
            if (ReferenceEquals(activity, m_overlay.m_activity))
                m_overlay.DetachFromActivity();
        }

        public void OnActivityCreated(Activity activity, Bundle? savedInstanceState) { }
        public void OnActivityStarted(Activity activity) { }
        public void OnActivityResumed(Activity activity) { }
        public void OnActivityPaused(Activity activity) { }
        public void OnActivityStopped(Activity activity) { }
        public void OnActivitySaveInstanceState(Activity activity, Bundle outState) { }
    }
}
