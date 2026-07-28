using Android.OS;
using Android.Views;
using Android.Widget;
using AndroidX.Core.View;
using AView = Android.Views.View;

namespace DIPS.Mobile.UI.Components.TextFields.Dictation;

internal sealed partial class DictationKeyboardOverlay
{
    private const long FadeInMilliseconds = 160;
    private const long FadeOutMilliseconds = 40;
    private const long HideDebounceMilliseconds = 10;
    private const int BottomMarginDp = 12; // gap between the microphone and the top of the keyboard
    private const int RightMarginDp = 20; // gap between the microphone and the right edge of the screen
    
    private void CreateKeyboardTracking()
    {
        m_mainThreadHandler = new Handler(Looper.MainLooper!);
        m_beginFadeOutRunnable = new Java.Lang.Runnable(BeginMicrophoneFadeOut);
        m_dismissRunnable = new Java.Lang.Runnable(DismissMicrophonePopup);
        m_insetsListener = new KeyboardInsetsListener(this);
    }

    private void ReadKeyboardInsets(WindowInsetsCompat insets)
    {
        var keyboard = insets.GetInsets(WindowInsetsCompat.Type.Ime());
        var navigationBar = insets.GetInsets(WindowInsetsCompat.Type.NavigationBars());

        m_keyboardBottomInset = keyboard.Bottom;
        m_navigationBarInset = navigationBar.Bottom;
        m_isKeyboardVisible = insets.IsVisible(WindowInsetsCompat.Type.Ime());
    }

    private void RefreshMicrophoneVisibility()
    {
        var shouldShowMicrophone = ShouldShowMicrophone();
        
        if (shouldShowMicrophone)
        {
            CancelPendingFade();
            ShowMicrophoneAboveKeyboard();
        }
        else
        {
            ScheduleMicrophoneHide();
        }
    }

    private bool ShouldShowMicrophone() =>
        m_isKeyboardVisible && DictationSessionCoordinator.Current.CurrentConsumer is not null;

    private void ShowMicrophoneAboveKeyboard()
    {
        var popup = m_popup;
        var buttonView = m_containerView;
        var decorView = m_hostWindowDecorView;

        if (popup is null || buttonView is null || decorView is null)
            return;

        if (!decorView.IsAttachedToWindow)
            return;

        // The keyboard inset already spans the navigation bar when the keyboard is open, so take the larger of the two.
        var offsetAboveKeyboard = Math.Max(m_keyboardBottomInset, m_navigationBarInset) + m_bottomMarginPixels;

        if (popup.IsShowing)
        {
            FadeIn(buttonView);
            RepositionMicrophoneIfMoved(popup, offsetAboveKeyboard);
            return;
        }

        ShowMicrophoneFadedIn(popup, buttonView, decorView, offsetAboveKeyboard);
    }

    private void ShowMicrophoneFadedIn(PopupWindow popup, AView buttonView, AView decorView, int offsetAboveKeyboard)
    {
        buttonView.Alpha = 0f;

        var shown = TryShowAtBottomEnd(popup, decorView, offsetAboveKeyboard);
        if (!shown)
            return;

        m_lastYOffset = offsetAboveKeyboard;
        FadeIn(buttonView);
    }

    private void RepositionMicrophoneIfMoved(PopupWindow popup, int offsetAboveKeyboard)
    {
        var movedNoticeably = Math.Abs(offsetAboveKeyboard - m_lastYOffset) > 2;
        if (!movedNoticeably)
            return;

        popup.Update(m_rightMarginPixels, offsetAboveKeyboard, -1, -1);
        m_lastYOffset = offsetAboveKeyboard;
    }

    private static void FadeIn(AView view) =>
        view.Animate()?.Alpha(1f)?.SetDuration(FadeInMilliseconds)?.Start();

    private bool TryShowAtBottomEnd(PopupWindow popup, AView decorView, int yOffset)
    {
        try
        {
            popup.ShowAtLocation(decorView, GravityFlags.Bottom | GravityFlags.End, m_rightMarginPixels, yOffset);
            return true;
        }
        catch (WindowManagerBadTokenException)
        {
            return false;
        }
    }

    private void ScheduleMicrophoneHide()
    {
        if (m_popup is null || !m_popup.IsShowing)
            return;

        if (m_mainThreadHandler is null || m_beginFadeOutRunnable is null)
            return;

        CancelPendingFade();
        m_mainThreadHandler.PostDelayed(m_beginFadeOutRunnable, HideDebounceMilliseconds);
    }

    private void BeginMicrophoneFadeOut()
    {
        if (ShouldShowMicrophone()) // keyboard or target came back within the debounce window
            return;

        // The keyboard is confirmed gone (for example the person tapped the keyboard's dismiss button, which does
        // not blur the field on Android). Stop dictation so it does not keep running with no keyboard up.
        StopDictationIfActive();

        var popup = m_popup;
        var buttonView = m_containerView;
        if (popup is null || buttonView is null || !popup.IsShowing)
            return;

        buttonView.Animate()?.Alpha(0f)?.SetDuration(FadeOutMilliseconds)?.Start();

        if (m_mainThreadHandler is not null && m_dismissRunnable is not null)
            m_mainThreadHandler.PostDelayed(m_dismissRunnable, FadeOutMilliseconds);
    }

    private void DismissMicrophonePopup()
    {
        if (ShouldShowMicrophone()) // came back during the fade
            return;

        if (m_popup is { IsShowing: true })
            TryDismiss(m_popup);

        m_lastYOffset = int.MinValue;
    }

    private void CancelPendingFade()
    {
        if (m_mainThreadHandler is null)
            return;

        if (m_beginFadeOutRunnable is not null)
            m_mainThreadHandler.RemoveCallbacks(m_beginFadeOutRunnable);

        if (m_dismissRunnable is not null)
            m_mainThreadHandler.RemoveCallbacks(m_dismissRunnable);
    }

    private void OnCurrentConsumerChanged(object? sender, EventArgs e)
    {
        // A new field (or none) is targeted. The coordinator has already stopped any previous session.
        m_isDictationActive = false;
        SetMicrophoneActive(false);
        RefreshMicrophoneVisibility();
    }

    private static void TryDismiss(PopupWindow popup)
    {
        try
        {
            popup.Dismiss();
        }
        catch (Exception)
        {
            // Nothing left to dismiss.
        }
    }

    private sealed class KeyboardInsetsListener : Java.Lang.Object, IOnApplyWindowInsetsListener
    {
        private readonly DictationKeyboardOverlay m_overlay;

        public KeyboardInsetsListener(DictationKeyboardOverlay overlay)
        {
            m_overlay = overlay;
        }

        public WindowInsetsCompat OnApplyWindowInsets(AView view, WindowInsetsCompat insets)
        {
            m_overlay.ReadKeyboardInsets(insets);
            m_overlay.RefreshMicrophoneVisibility();
            
            return insets; // do not consume
        }
    }
}
