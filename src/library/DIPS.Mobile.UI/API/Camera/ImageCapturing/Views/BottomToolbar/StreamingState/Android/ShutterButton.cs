using Android.Views;
using DIPS.Mobile.UI.API.Library;
using Microsoft.Maui.Platform;
using View = Android.Views.View;

namespace DIPS.Mobile.UI.API.Camera.ImageCapturing.Views.BottomToolbar.StreamingState;

internal partial class ShutterButton
{
    private bool m_isCancelled;
    private View? m_nativeView;

    private partial void AddPlatformGestureRecognizer()
    {
        m_nativeView = this.ToPlatform(DUI.GetCurrentMauiContext!);
        m_nativeView.Touch -= OnTappedShutterButton;
        m_nativeView.Touch += OnTappedShutterButton;
    }

    private void OnTappedShutterButton(object? sender, Android.Views.View.TouchEventArgs e)
    {
        if (e.Event is null || sender is not View view)
            return;

        switch (e.Event.Action)
        {
            case MotionEventActions.Down:
                m_isCancelled = false;
                m_shutterContentWhiteOverlay.ScaleTo(.85f, 150);
                break;
            case MotionEventActions.Up:
                if (!m_isCancelled)
                    m_onTappedShutterButton?.Invoke();
                m_shutterContentWhiteOverlay.ScaleTo(1);
                break;
            case MotionEventActions.Move:
                if (!IsInside(view, e.Event) && !m_isCancelled)
                {
                    m_isCancelled = true;
                    m_shutterContentWhiteOverlay.ScaleTo(1);
                }
                break;
        }
    }

    private static bool IsInside(View v, MotionEvent e)
    {
        return !(e.GetX() < 0 || e.GetY() < 0
                              || e.GetX() > v.MeasuredWidth
                              || e.GetY() > v.MeasuredHeight);
    }

    public partial void Dispose()
    {
        if (m_nativeView is not null)
        {
            m_nativeView.Touch -= OnTappedShutterButton;
        }
    }
}