using CoreGraphics;
using Foundation;
using UIKit;

namespace DIPS.Mobile.UI.Effects.Touch.iOS;

public class TouchEffectTapGestureRecognizer : UITapGestureRecognizer
{
    private readonly UIView m_uiView;

    internal UIGestureRecognizerState m_currentState = UIGestureRecognizerState.Possible;

    public TouchEffectTapGestureRecognizer(UIView uiView, Action onTapped) : base(onTapped)
    {
        m_uiView = uiView;
        
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
}