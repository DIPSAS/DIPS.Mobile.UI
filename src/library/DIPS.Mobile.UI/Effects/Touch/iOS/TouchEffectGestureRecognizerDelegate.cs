using UIKit;

namespace DIPS.Mobile.UI.Effects.Touch.iOS;

internal class TouchEffectGestureRecognizerDelegate : UIGestureRecognizerDelegate
{
    private readonly TouchPlatformEffect m_touchPlatformEffect;

    public TouchEffectGestureRecognizerDelegate(TouchPlatformEffect touchPlatformEffect)
    {
        m_touchPlatformEffect = touchPlatformEffect;
    }
    
    public override bool ShouldReceiveTouch(UIGestureRecognizer recognizer, UITouch touch)
    {
        if (touch.View.IsDescendantOfView(recognizer.View))
        {
            if (touch.View.Equals(recognizer.View))
                return true;

            if (touch.View is UIControl)
                return false;

            return CheckRecognizers(touch.View, recognizer.View);
        }

        return false;
    }

    private bool CheckRecognizers(UIView view, UIView superViewThatContainsRecognizer)
    {
        var gestureRecognizers = view.GestureRecognizers;

        if (view.Equals(superViewThatContainsRecognizer))
            return true;
        
        if (gestureRecognizers is not null && gestureRecognizers.Any())
        {
            return false;
        }

        return CheckRecognizers(view.Superview, superViewThatContainsRecognizer);
    }

    public override bool ShouldRecognizeSimultaneously(UIGestureRecognizer gestureRecognizer,
        UIGestureRecognizer otherGestureRecognizer)
    {
        if (gestureRecognizer is TouchEffectTapGestureRecognizer touchGesture &&
            otherGestureRecognizer is UIPanGestureRecognizer &&
            otherGestureRecognizer.State == UIGestureRecognizerState.Began)
        {
            m_touchPlatformEffect.HandleTouch(UIGestureRecognizerState.Cancelled, ref touchGesture.m_currentState);
        }

        return true;
    }
}