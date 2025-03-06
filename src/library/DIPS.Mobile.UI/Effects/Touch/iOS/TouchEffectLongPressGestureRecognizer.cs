using CoreGraphics;
using Foundation;
using UIKit;

namespace DIPS.Mobile.UI.Effects.Touch.iOS;

internal sealed class TouchEffectLongPressGestureRecognizer : UILongPressGestureRecognizer
{
    private TouchPlatformEffect? m_touchPlatformEffect;
    internal UIGestureRecognizerState m_currentState = UIGestureRecognizerState.Possible;

    public TouchEffectLongPressGestureRecognizer(TouchPlatformEffect touchPlatformEffect, Action<UILongPressGestureRecognizer> onLongPressed) : base(
        action: onLongPressed)
    {
        m_touchPlatformEffect = touchPlatformEffect;
        MinimumPressDuration = .5;
        Delegate = new TouchEffectGestureRecognizerDelegate(touchPlatformEffect);
    }

    public override void TouchesBegan(NSSet touches, UIEvent evt)
    {
        base.TouchesBegan(touches, evt);

        m_touchPlatformEffect?.HandleTouch(UIGestureRecognizerState.Began, ref m_currentState);
    }

    public override void TouchesEnded(NSSet touches, UIEvent evt)
    {
        base.TouchesEnded(touches, evt);

        m_touchPlatformEffect?.HandleTouch(UIGestureRecognizerState.Ended, ref m_currentState);
    }

    public override void TouchesCancelled(NSSet touches, UIEvent evt)
    {
        base.TouchesCancelled(touches, evt);

        m_touchPlatformEffect?.HandleTouch(UIGestureRecognizerState.Cancelled, ref m_currentState);
    }

    public override void TouchesMoved(NSSet touches, UIEvent evt)
    {
        var touchPoint = GetTouchPoint(touches);
        
        if (m_touchPlatformEffect?.Control != null && (touchPoint == null || m_touchPlatformEffect.Control.Bounds.Contains(touchPoint.Value)))
        {
            m_touchPlatformEffect?.HandleTouch(UIGestureRecognizerState.Cancelled, ref m_currentState);
        }
    }

    private CGPoint? GetTouchPoint(NSSet touches) =>
        (touches.AnyObject as UITouch)?.LocationInView(m_touchPlatformEffect?.Control);

    protected override void Dispose(bool disposing)
    {
        base.Dispose(disposing);
        m_touchPlatformEffect = null;
    }
}