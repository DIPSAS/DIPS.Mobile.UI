using CoreGraphics;
using DIPS.Mobile.UI.Effects.Touch;
using Foundation;
using UIKit;

namespace DIPS.Mobile.UI.Platforms.iOS
{
    internal class UIButtonWithExtraTappableArea : UIButton
    {
        private UIGestureRecognizerState m_currentState;

        public Thickness AdditionalHitBoxSize { get; set; }

        public override bool PointInside(CGPoint point, UIEvent? uievent)
        {
            return Bounds.Inset((nfloat)(-AdditionalHitBoxSize.HorizontalThickness), (nfloat)(-(AdditionalHitBoxSize.VerticalThickness))).Contains(point);
        }

        public override void TouchesBegan(NSSet touches, UIEvent? evt)
        {
            base.TouchesBegan(touches, evt);
            
            TouchPlatformEffect.HandleTouch(UIGestureRecognizerState.Began, ref m_currentState, this);
        }

        public override void TouchesEnded(NSSet touches, UIEvent? evt)
        {
            base.TouchesEnded(touches, evt);
            
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
}