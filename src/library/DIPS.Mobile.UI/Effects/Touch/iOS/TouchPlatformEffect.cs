using CoreGraphics;
using Foundation;
using UIKit;

// ReSharper disable once CheckNamespace
namespace DIPS.Mobile.UI.Effects.Touch;

public partial class TouchPlatformEffect
{
    private DUITapGestureRecognizer? m_tapGestureRecognizer;
    private DUILongPressGestureRecognizer? m_longPressGestureRecognizer;
    private Touch.TouchMode m_touchMode;

    protected override partial void OnAttached()
    {
        if (Control is UIButton)
            return;

        m_touchMode = Touch.GetTouchMode(Element);

        if (m_touchMode is Touch.TouchMode.Tap or Touch.TouchMode.Both)
        {
            m_tapGestureRecognizer = new DUITapGestureRecognizer(Control, OnTap);
            Control.AddGestureRecognizer(m_tapGestureRecognizer);
        }

        if (m_touchMode is Touch.TouchMode.LongPress or Touch.TouchMode.Both)
        {
            m_longPressGestureRecognizer = new DUILongPressGestureRecognizer(Control, OnLongPress);
            Control.AddGestureRecognizer(m_longPressGestureRecognizer);
        }

        Control.UserInteractionEnabled = true;
    }

    private void OnLongPress(UILongPressGestureRecognizer e)
    {
        if (e.State != UIGestureRecognizerState.Began)
            return;

        if (Touch.GetLongPressCommand(Element).CanExecute(Touch.GetLongPressCommandParameter(Element)))
            Touch.GetLongPressCommand(Element).Execute(Touch.GetLongPressCommandParameter(Element));
    }

    private void OnTap()
    {
        if (Touch.GetCommand(Element).CanExecute(Touch.GetCommandParameter(Element)))
            Touch.GetCommand(Element).Execute(Touch.GetCommandParameter(Element));
    }

    protected override partial void OnDetached()
    {
        if (m_touchMode is Touch.TouchMode.Tap or Touch.TouchMode.Both)
        {
            Control.RemoveGestureRecognizer(m_tapGestureRecognizer!);
        }

        if (m_touchMode is Touch.TouchMode.LongPress or Touch.TouchMode.Both)
        {
            Control.RemoveGestureRecognizer(m_longPressGestureRecognizer!);
        }

        Control.UserInteractionEnabled = false;
    }

    private static void HandleTouch(UIGestureRecognizerState state, ref UIGestureRecognizerState currentState,
        UIView uiView)
    {
        switch (state)
        {
            case UIGestureRecognizerState.Began:
                Animate(uiView);
                break;
            case UIGestureRecognizerState.Cancelled:
                SetToOriginalOpacity(uiView, 1);
                break;
            case UIGestureRecognizerState.Ended:
                if (currentState != UIGestureRecognizerState.Cancelled)
                {
                    SetToOriginalOpacity(uiView, 1);
                }

                break;
        }

        currentState = state;
    }

    private static void SetToOriginalOpacity(UIView uiView, float originalOpacity)
    {
        UIViewPropertyAnimator.CreateRunningPropertyAnimator(0.2, 0,
            UIViewAnimationOptions.AllowUserInteraction | UIViewAnimationOptions.CurveEaseIn,
            () =>
            {
                uiView.Alpha = originalOpacity;
            }, null);
    }

    private static void Animate(UIView uiView)
    {
        UIViewPropertyAnimator.CreateRunningPropertyAnimator(0.3, 0,
            UIViewAnimationOptions.AllowUserInteraction | UIViewAnimationOptions.CurveEaseIn,
            () =>
            {
                uiView.Alpha = 0.5f;
            }, null);
    }

    public sealed class DUILongPressGestureRecognizer : UILongPressGestureRecognizer
    {
        private readonly UIView m_uiView;
        private UIGestureRecognizerState m_currentState = UIGestureRecognizerState.Possible;

        public DUILongPressGestureRecognizer(UIView uiView, Action<UILongPressGestureRecognizer> action) : base(
            action: action)
        {
            m_uiView = uiView;

            MinimumPressDuration = .5;
            Delegate = new GestureRecognizerDelegate();
        }

        public override void TouchesBegan(NSSet touches, UIEvent evt)
        {
            base.TouchesBegan(touches, evt);

            HandleTouch(UIGestureRecognizerState.Began, ref m_currentState, m_uiView);
        }

        public override void TouchesEnded(NSSet touches, UIEvent evt)
        {
            base.TouchesEnded(touches, evt);

            HandleTouch(UIGestureRecognizerState.Ended, ref m_currentState, m_uiView);
        }

        public override void TouchesCancelled(NSSet touches, UIEvent evt)
        {
            base.TouchesCancelled(touches, evt);

            HandleTouch(UIGestureRecognizerState.Cancelled, ref m_currentState, m_uiView);
        }

        public override void TouchesMoved(NSSet touches, UIEvent evt)
        {
            var touchPoint = GetTouchPoint(touches);

            if (touchPoint == null || !m_uiView.Bounds.Contains(touchPoint.Value))
            {
                HandleTouch(UIGestureRecognizerState.Cancelled, ref m_currentState, m_uiView);
            }
        }

        private CGPoint? GetTouchPoint(NSSet touches) =>
            (touches.AnyObject as UITouch)?.LocationInView(m_uiView);
    }

    public class DUITapGestureRecognizer : UITapGestureRecognizer
    {
        private readonly UIView m_uiView;
        private readonly BindableObject m_element;

        private UIGestureRecognizerState m_currentState = UIGestureRecognizerState.Possible;

        public DUITapGestureRecognizer(UIView uiView, Action action) : base(action)
        {
            m_uiView = uiView;

            Delegate = new GestureRecognizerDelegate();
        }

        public override void TouchesBegan(NSSet touches, UIEvent evt)
        {
            base.TouchesBegan(touches, evt);

            HandleTouch(UIGestureRecognizerState.Began, ref m_currentState, m_uiView);
        }

        public override void TouchesEnded(NSSet touches, UIEvent evt)
        {
            base.TouchesEnded(touches, evt);

            HandleTouch(UIGestureRecognizerState.Ended, ref m_currentState, m_uiView);
        }

        public override void TouchesCancelled(NSSet touches, UIEvent evt)
        {
            base.TouchesCancelled(touches, evt);

            HandleTouch(UIGestureRecognizerState.Cancelled, ref m_currentState, m_uiView);
        }

        public override void TouchesMoved(NSSet touches, UIEvent evt)
        {
            var touchPoint = GetTouchPoint(touches);

            if (touchPoint == null || !m_uiView.Bounds.Contains(touchPoint.Value))
            {
                HandleTouch(UIGestureRecognizerState.Cancelled, ref m_currentState, m_uiView);
            }
        }

        private CGPoint? GetTouchPoint(NSSet touches) =>
            (touches.AnyObject as UITouch)?.LocationInView(m_uiView);
    }

    private class GestureRecognizerDelegate : UIGestureRecognizerDelegate
    {
        public GestureRecognizerDelegate()
        {
        }

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
    }
}