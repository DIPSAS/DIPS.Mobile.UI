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
    private bool m_isEnabled;

    protected override partial void OnAttached()
    {
        if(Control is UIButton)
            return;

        m_isEnabled = Touch.GetIsEnabled(Element);
        m_touchMode = Touch.GetTouchMode(Element);

        if (m_touchMode is Touch.TouchMode.Tap or Touch.TouchMode.Both && m_isEnabled)
        {
            m_tapGestureRecognizer = new TouchEffectTapGestureRecognizer(Control, OnTap);
            Control.AddGestureRecognizer(m_tapGestureRecognizer);
        }

        if (m_touchMode is Touch.TouchMode.LongPress or Touch.TouchMode.Both && m_isEnabled)
        {
            m_longPressGestureRecognizer = new TouchEffectLongPressGestureRecognizer(Control, OnLongPress);
            Control.AddGestureRecognizer(m_longPressGestureRecognizer);
        }
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
            if(Control.GestureRecognizers != null)
                Control.RemoveGestureRecognizer(m_tapGestureRecognizer!);
        }

        if (m_touchMode is Touch.TouchMode.LongPress or Touch.TouchMode.Both)
        {
            if(Control.GestureRecognizers != null)
                Control.RemoveGestureRecognizer(m_longPressGestureRecognizer!);
        }
    }

    internal static void HandleTouch(UIGestureRecognizerState state, ref UIGestureRecognizerState currentState,
        UIView uiView)
    {
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