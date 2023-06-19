using UIKit;

namespace DIPS.Mobile.UI.Effects.Touch.iOS;

internal class TouchEffectGestureRecognizerDelegate : UIGestureRecognizerDelegate
{
    public override bool ShouldReceiveTouch(UIGestureRecognizer recognizer, UITouch touch)
    {
        if (touch.View.IsDescendantOfView(recognizer.View))
        {
            if (touch.View.Equals(recognizer.View))
                return true;

            return !touch.View.GestureRecognizers?.Any() ?? true;
        }

        return false;
    }

    public override bool ShouldRecognizeSimultaneously(UIGestureRecognizer gestureRecognizer,
        UIGestureRecognizer otherGestureRecognizer)
    {
        if (gestureRecognizer is TouchEffectTapGestureRecognizer touchGesture &&
            otherGestureRecognizer is UIPanGestureRecognizer &&
            otherGestureRecognizer.State == UIGestureRecognizerState.Began)
        {
            TouchPlatformEffect.HandleTouch(UIGestureRecognizerState.Cancelled, ref touchGesture.m_currentState,
                gestureRecognizer.View);
        }

        return true;
    }
}