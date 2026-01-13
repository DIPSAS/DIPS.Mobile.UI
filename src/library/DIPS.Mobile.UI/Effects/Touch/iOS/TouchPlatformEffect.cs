using System.ComponentModel;
using System.Reflection.Metadata;
using CoreGraphics;
using DIPS.Mobile.UI.Effects.Touch.iOS;
using DIPS.Mobile.UI.Platforms.iOS;
using Foundation;
using UIKit;
using Button = DIPS.Mobile.UI.Components.Buttons.Button;

// ReSharper disable once CheckNamespace
namespace DIPS.Mobile.UI.Effects.Touch;

public partial class TouchPlatformEffect
{
    private TouchEffectTapGestureRecognizer? m_tapGestureRecognizer;
    private TouchEffectLongPressGestureRecognizer? m_longPressGestureRecognizer;
    private Touch.TouchMode m_touchMode;

    private partial void Init()
    {
        if(Control is UIButton)
            return;
        
        m_touchMode = Touch.GetTouchMode(Element);

        if (m_touchMode is Touch.TouchMode.Tap or Touch.TouchMode.Both && m_tapGestureRecognizer is null)
        {
            m_tapGestureRecognizer = new TouchEffectTapGestureRecognizer(Control, OnTap);
            Control.AddGestureRecognizer(m_tapGestureRecognizer);
        }

        if (m_touchMode is Touch.TouchMode.LongPress or Touch.TouchMode.Both && m_longPressGestureRecognizer is null)
        {
            m_longPressGestureRecognizer = new TouchEffectLongPressGestureRecognizer(Control, OnLongPress);
            Control.AddGestureRecognizer(m_longPressGestureRecognizer);
        }
    }

    private partial void OnAccessibilityDescriptionSet()
    {
        // Append the AccessibilityTraits to `Button` if there is an accessibility description set
        if (!string.IsNullOrEmpty(Control.AccessibilityLabel) && Touch.GetIsButtonTraitEnabled(Element))
        {
            // Use | to append, to make sure we are not overwriting anything here
            Control.AccessibilityTraits |= UIAccessibilityTrait.Button;
        }
    }

    private void OnLongPress(UILongPressGestureRecognizer e)
    {
        if (e.State != UIGestureRecognizerState.Began)
            return;
        
        m_tapGestureRecognizer?.NotifyLongPress();

        if (Touch.GetLongPressCommand(Element)?.CanExecute(Touch.GetLongPressCommandParameter(Element)) ?? false)
            Touch.GetLongPressCommand(Element)?.Execute(Touch.GetLongPressCommandParameter(Element));
    }

    private void OnTap()
    {
        if (Touch.GetCommand(Element)?.CanExecute(Touch.GetCommandParameter(Element)) ?? false)
            Touch.GetCommand(Element)?.Execute(Touch.GetCommandParameter(Element));
    }

    private partial void Dispose(bool isDetaching)
    {
        if(Control is null)
            return;
        
        // If the Control was set to disabled after being pressed, we need to animate it back to the original state
        if (!isDetaching)
        {
            AnimateToOriginal(Control, 1);
        }
        
        if (m_touchMode is Touch.TouchMode.Tap or Touch.TouchMode.Both)
        {
            if (Control.GestureRecognizers != null && m_tapGestureRecognizer is not null)
            {
                Control.RemoveGestureRecognizer(m_tapGestureRecognizer);
            }
        }

        if (m_touchMode is Touch.TouchMode.LongPress or Touch.TouchMode.Both)
        {
            if(Control.GestureRecognizers != null && m_longPressGestureRecognizer is not null)
                Control.RemoveGestureRecognizer(m_longPressGestureRecognizer);
        }

        m_tapGestureRecognizer?.Dispose();
        m_longPressGestureRecognizer?.Dispose();
        
        m_tapGestureRecognizer = null;
        m_longPressGestureRecognizer = null;
    }

    internal static void HandleTouch(UIGestureRecognizerState state, ref UIGestureRecognizerState currentState,
        UIView? uiView)
    {
        if (uiView is null) return;
        switch (state)
        {
            case UIGestureRecognizerState.Began:
                Animate(uiView);
                break;
            case UIGestureRecognizerState.Cancelled:
                AnimateToOriginal(uiView, 1);
                break;
            case UIGestureRecognizerState.Ended:
                if (currentState != UIGestureRecognizerState.Cancelled)
                {
                    AnimateToOriginal(uiView, 1);
                }

                break;
        }

        currentState = state;
    }

    private static void AnimateToOriginal(UIView uiView, float originalOpacity)
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
}