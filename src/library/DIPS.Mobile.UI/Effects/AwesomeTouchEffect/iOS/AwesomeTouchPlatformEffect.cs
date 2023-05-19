using System.Windows.Input;
using CoreGraphics;
using Foundation;
using Microsoft.Maui.Controls.Compatibility.Platform.iOS;
using Microsoft.Maui.Platform;
using UIKit;
using ContentView = Microsoft.Maui.Platform.ContentView;

// ReSharper disable once CheckNamespace
namespace DIPS.Mobile.UI.Effects.AwesomeTouchEffect;

public partial class AwesomeTouchPlatformEffect
{
    
#nullable disable
    private AwesomeUITapGestureRecognizer m_gestureRecognizer;
#nullable restore

    protected override partial void OnAttached()
    {
        if(Control is UIButton)
            return;

        m_gestureRecognizer = new AwesomeUITapGestureRecognizer(Control, Element);
        
        Control.AddGestureRecognizer(m_gestureRecognizer);
        Control.UserInteractionEnabled = true;
    }

    protected override partial void OnDetached()
    {
        Control.RemoveGestureRecognizer(m_gestureRecognizer);
        Control.UserInteractionEnabled = false;
    }

    public class AwesomeUITapGestureRecognizer : UITapGestureRecognizer
    {
        private readonly UIView m_uiView;
        
        private readonly float m_originalOpacity;
        
        private readonly ICommand m_command;
        private readonly object m_commandParameter;

        private UIGestureRecognizerState m_currentState = UIGestureRecognizerState.Possible;
        
        public AwesomeUITapGestureRecognizer(UIView uiView, BindableObject element)
        {
            m_uiView = uiView;
            m_originalOpacity = uiView.Layer.Opacity;
            
            m_command = AwesomeTouchEffect.GetCommand(element);
            m_commandParameter = AwesomeTouchEffect.GetCommandParameter(element);

            Delegate = new TapGestureRecognizerDelegate();
        }
        
        public override void TouchesBegan(NSSet touches, UIEvent evt)
        {
            base.TouchesBegan(touches, evt);
            
            HandleTouch(UIGestureRecognizerState.Began);
        }

        public override void TouchesEnded(NSSet touches, UIEvent evt)
        {
            base.TouchesEnded(touches, evt);

            HandleTouch(UIGestureRecognizerState.Ended);
        }

        public override void TouchesCancelled(NSSet touches, UIEvent evt)
        {
            base.TouchesCancelled(touches, evt);
            
            HandleTouch(UIGestureRecognizerState.Cancelled);
        }

        public override void TouchesMoved(NSSet touches, UIEvent evt)
        {
            var touchPoint = GetTouchPoint(touches);

            if (touchPoint == null || !m_uiView.Bounds.Contains(touchPoint.Value))
            {
                HandleTouch(UIGestureRecognizerState.Cancelled);
            }
        }
        
        private CGPoint? GetTouchPoint(NSSet touches) =>
            (touches.AnyObject as UITouch)?.LocationInView(m_uiView);

        private void HandleTouch(UIGestureRecognizerState state)
        {
            switch (state)
            {
                case UIGestureRecognizerState.Began:
                    Animate();
                    break;
                case UIGestureRecognizerState.Cancelled:
                    SetToOriginalOpacity();
                    break;
                case UIGestureRecognizerState.Ended:
                    if (m_currentState != UIGestureRecognizerState.Cancelled)
                    {
                        if(m_command.CanExecute(m_commandParameter))
                            m_command.Execute(m_commandParameter);
                        
                        SetToOriginalOpacity();
                    }
                    break;
            }
            
            m_currentState = state;
        }

        private void SetToOriginalOpacity()
        {
            UIView.Animate(duration: 0.2, 0, UIViewAnimationOptions.CurveEaseIn,delegate
            {
                m_uiView.Layer.Opacity = m_originalOpacity;
            }, null);
        }

        private void Animate()
        {
            UIView.Animate(duration: 0.3, 0, UIViewAnimationOptions.CurveEaseOut,delegate
            {
                m_uiView.Layer.Opacity = 0.5f;
            }, null);
        }
        
        private class TapGestureRecognizerDelegate : UIGestureRecognizerDelegate
        {
            public TapGestureRecognizerDelegate()
            {
            }
            
            public override bool ShouldReceiveTouch(UIGestureRecognizer recognizer, UITouch touch)
            {
                if (touch.View.IsDescendantOfView(recognizer.View))
                {
                    return !touch.View.GestureRecognizers?.Any() ?? true;
                }
                
                return false;
            }
        }

    }
}