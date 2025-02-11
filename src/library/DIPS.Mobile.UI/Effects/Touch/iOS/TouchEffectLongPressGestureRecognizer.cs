using CoreGraphics;
using Foundation;
using UIKit;

namespace DIPS.Mobile.UI.Effects.Touch.iOS;

internal sealed class TouchEffectLongPressGestureRecognizer : UILongPressGestureRecognizer
{
    private UIView? m_uiView;
    internal UIGestureRecognizerState m_currentState = UIGestureRecognizerState.Possible;

    public TouchEffectLongPressGestureRecognizer(UIView uiView, Action<UILongPressGestureRecognizer> onLongPressed) : base(
        action: onLongPressed)
    {
        m_uiView = uiView;

        MinimumPressDuration = .5;
        Delegate = new TouchEffectGestureRecognizerDelegate();
    }

    public override void TouchesBegan(NSSet touches, UIEvent evt)
    {
        base.TouchesBegan(touches, evt);

        TouchPlatformEffect.HandleTouch(UIGestureRecognizerState.Began, ref m_currentState, m_uiView);
    }

    public override void TouchesEnded(NSSet touches, UIEvent evt)
    {
        base.TouchesEnded(touches, evt);

        TouchPlatformEffect.HandleTouch(UIGestureRecognizerState.Ended, ref m_currentState, m_uiView);
    }

    public override void TouchesCancelled(NSSet touches, UIEvent evt)
    {
        base.TouchesCancelled(touches, evt);

        TouchPlatformEffect.HandleTouch(UIGestureRecognizerState.Cancelled, ref m_currentState, m_uiView);
    }

    public override void TouchesMoved(NSSet touches, UIEvent evt)
    {
        var touchPoint = GetTouchPoint(touches);
        
        if (m_uiView != null && (touchPoint == null || m_uiView.Bounds.Contains(touchPoint.Value)))
        {
            TouchPlatformEffect.HandleTouch(UIGestureRecognizerState.Cancelled, ref m_currentState, m_uiView);
        }
    }

    private CGPoint? GetTouchPoint(NSSet touches) =>
        (touches.AnyObject as UITouch)?.LocationInView(m_uiView);

    protected override void Dispose(bool disposing)
    {
        base.Dispose(disposing);
        m_uiView = null;
    }
}