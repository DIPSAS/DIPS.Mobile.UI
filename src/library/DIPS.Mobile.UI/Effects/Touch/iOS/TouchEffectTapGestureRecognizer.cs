using System.Diagnostics;
using CoreGraphics;
using Foundation;
using UIKit;

namespace DIPS.Mobile.UI.Effects.Touch.iOS;

public class TouchEffectTapGestureRecognizer : UIGestureRecognizer
{
    private UIView? m_uiView;
    private readonly Action m_onTap;

    internal UIGestureRecognizerState m_currentState = UIGestureRecognizerState.Possible;

    private bool m_isCancelled;
    private readonly NSObject m_didBecomeActiveObserver;

    public TouchEffectTapGestureRecognizer(UIView view, Action onTap)
    {
        m_onTap = onTap;
        m_uiView = view;
        
        Delegate = new TouchEffectGestureRecognizerDelegate();
        m_didBecomeActiveObserver = NSNotificationCenter.DefaultCenter.AddObserver(
        UIApplication.DidBecomeActiveNotification,
        _ => ForceGestureRecognition()
    );

}
    //Because of an issue where our Delegate do not recieve ShouldRecognizeSimultaneously. Which prevents the user from scrolling if the view is used in a scrolling context. This was introduced in iOS 18.3.x
    private void ForceGestureRecognition()
    {
        this.Enabled = false;
        this.Enabled = true;
    }

    public override void TouchesBegan(NSSet touches, UIEvent evt)
    {
        base.TouchesBegan(touches, evt);
        
        m_isCancelled = false;

        TouchPlatformEffect.HandleTouch(UIGestureRecognizerState.Began, ref m_currentState, m_uiView);
    }
    
    public override void TouchesEnded(NSSet touches, UIEvent evt)
    {
        base.TouchesEnded(touches, evt);
        
        if(m_currentState is not UIGestureRecognizerState.Cancelled)
            m_onTap.Invoke();
        
        TouchPlatformEffect.HandleTouch(UIGestureRecognizerState.Ended, ref m_currentState, m_uiView);
    }

    public override void TouchesCancelled(NSSet touches, UIEvent evt)
    {
        base.TouchesCancelled(touches, evt);

        TouchPlatformEffect.HandleTouch(UIGestureRecognizerState.Cancelled, ref m_currentState, m_uiView);
    }
    
    public override void TouchesMoved(NSSet touches, UIEvent evt)
    {
        if (m_isCancelled)
        {
            return;
        }
        
        var touchPoint = GetTouchPoint(touches);

        if (m_uiView != null && (touchPoint == null || m_uiView.Bounds.Contains(touchPoint.Value)))
        {
            TouchPlatformEffect.HandleTouch(UIGestureRecognizerState.Cancelled, ref m_currentState, m_uiView);
            m_isCancelled = true;
        }
        
        base.TouchesMoved(touches, evt);
    }
    
    private CGPoint? GetTouchPoint(NSSet touches) =>
        (touches.AnyObject as UITouch)?.LocationInView(m_uiView);

    protected override void Dispose(bool disposing)
    {
        base.Dispose(disposing);
        NSNotificationCenter.DefaultCenter.RemoveObserver(m_didBecomeActiveObserver);
        m_uiView = null;
    }
}