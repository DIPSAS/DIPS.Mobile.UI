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
/// Shows the dictation microphone in a separate non-focusable window pinned just above the keyboard, so the
/// activity's adjustPan cannot drag it and the keyboard stays open when it is tapped. One overlay serves every
/// text field. The coordinator decides which field the microphone dictates into. Lives for the current activity
/// and tears itself down when that activity is destroyed.
/// </summary>
internal sealed partial class DictationKeyboardOverlay
{
    public static DictationKeyboardOverlay Current { get; } = new();

    private Activity? m_activity;
    private ViewGroup? m_contentView;
    private AView? m_decorView;
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

    /// <summary>Prepares the overlay for the given activity. Idempotent per activity. Rebuilds if the activity changes.</summary>
    public void AttachToActivity(Activity? activity)
    {
        if (activity is null)
            return;

        if (ReferenceEquals(m_activity, activity))
            return;

        if (m_activity is not null)
            DetachFromActivity();

        var mauiContext = DUI.GetCurrentMauiContext;
        var contentView = activity.FindViewById<ViewGroup>(global::Android.Resource.Id.Content);
        var decorView = activity.Window?.DecorView;
        if (mauiContext is null || contentView is null || decorView is null)
            return;

        var density = activity.Resources?.DisplayMetrics?.Density ?? 1f;

        RegisterCurrentActivity(activity, contentView, decorView, density);
        
        CreateMicrophonePopup(activity, mauiContext, density);
        
        StartTrackingKeyboardHeight(contentView);
        
        StartWatchingForActivityDestroyed(activity);
        
        ObserveDictationTarget();

        // Read the current insets now, so the microphone appears immediately if the keyboard is already open.
        ViewCompat.RequestApplyInsets(contentView);
    }

    private void RegisterCurrentActivity(Activity activity, ViewGroup contentView, AView decorView, float density)
    {
        m_activity = activity;
        m_contentView = contentView;
        m_decorView = decorView;
        m_bottomMarginPixels = (int)(BottomMarginDp * density);
        m_rightMarginPixels = (int)(RightMarginDp * density);
    }

    private void ObserveDictationTarget()
    {
        DictationSessionCoordinator.Current.CurrentConsumerChanged += OnCurrentConsumerChanged;
    }

    /// <summary>Removes the overlay, its listeners and popup, and disconnects the hosted button's handler.</summary>
    public void DetachFromActivity()
    {
        CancelPendingFade();
        DictationSessionCoordinator.Current.CurrentConsumerChanged -= OnCurrentConsumerChanged;
        StopWatchingForActivityDestroyed();

        if (m_contentView is not null)
            ViewCompat.SetOnApplyWindowInsetsListener(m_contentView, null);

        if (m_popup is { IsShowing: true })
            TryDismiss(m_popup);

        m_microphoneImageButton?.Handler?.DisconnectHandler();

        m_popup = null;
        m_microphoneImageButton = null;
        m_imageView = null;
        m_containerView = null;
        m_stopSquareDrawable = null;
        m_microphoneIconDrawable = null;
        m_insetsListener = null;
        m_contentView = null;
        m_decorView = null;
        m_activity = null;
        m_mainThreadHandler = null;
        m_beginFadeOutRunnable = null;
        m_dismissRunnable = null;
        m_lastYOffset = int.MinValue;
        m_isDictationActive = false;
        m_isKeyboardVisible = false;
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
