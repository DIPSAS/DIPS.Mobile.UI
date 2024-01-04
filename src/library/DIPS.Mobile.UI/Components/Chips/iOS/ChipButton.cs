using CoreGraphics;
using DIPS.Mobile.UI.Effects.Touch;
using Foundation;
using UIKit;

namespace DIPS.Mobile.UI.Components.Chips;

public class ChipButton : UIButton
{
    private UIGestureRecognizerState m_currentState;

    public Action<NSSet>? OnTapped { get; set; }

    public override void TouchesBegan(NSSet touches, UIEvent? evt)
    {
        base.TouchesBegan(touches, evt);
            
        TouchPlatformEffect.HandleTouch(UIGestureRecognizerState.Began, ref m_currentState, this);
    }

    public override void TouchesEnded(NSSet touches, UIEvent? evt)
    {
        base.TouchesEnded(touches, evt);

        if (m_currentState is not UIGestureRecognizerState.Cancelled)
        {
            OnTapped?.Invoke(touches);
        }
            
        TouchPlatformEffect.HandleTouch(UIGestureRecognizerState.Ended, ref m_currentState, this);
    }

    public override void TouchesCancelled(NSSet touches, UIEvent? evt)
    {
        base.TouchesCancelled(touches, evt);
            
        TouchPlatformEffect.HandleTouch(UIGestureRecognizerState.Cancelled, ref m_currentState, this);
    }
        
    public override void TouchesMoved(NSSet touches, UIEvent evt)
    {
        base.TouchesMoved(touches, evt);
        
        var touchPoint = GetTouchPoint(touches);

        if (touchPoint == null || !Bounds.Contains(touchPoint.Value))
        {
            TouchPlatformEffect.HandleTouch(UIGestureRecognizerState.Cancelled, ref m_currentState, this);
        }
    }
    
    private CGPoint? GetTouchPoint(NSSet touches) =>
        (touches.AnyObject as UITouch)?.LocationInView(this);
}