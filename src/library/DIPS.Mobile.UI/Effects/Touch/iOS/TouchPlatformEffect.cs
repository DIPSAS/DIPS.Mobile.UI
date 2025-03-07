using DIPS.Mobile.UI.API.Animations;
using DIPS.Mobile.UI.Effects.Touch.iOS;
using Microsoft.Maui.Platform;
using UIKit;
using Colors = Microsoft.Maui.Graphics.Colors;

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
        if (Control is UIButton)
            return;

        m_isEnabled = Touch.GetIsEnabled(Element);
        m_touchMode = Touch.GetTouchMode(Element);

        if (Element is not VisualElement visualElement) return;
        VisualElement = visualElement;
        
        OriginalBackgroundColor = visualElement.BackgroundColor ?? Colors.Transparent;
        if (m_touchMode is Touch.TouchMode.Tap or Touch.TouchMode.Both && m_isEnabled)
        {
            m_tapGestureRecognizer = new TouchEffectTapGestureRecognizer(this);
            Control.AddGestureRecognizer(m_tapGestureRecognizer);
        }

        if (m_touchMode is Touch.TouchMode.LongPress or Touch.TouchMode.Both && m_isEnabled)
        {
            m_longPressGestureRecognizer = new TouchEffectLongPressGestureRecognizer(this, OnLongPress);
            Control.AddGestureRecognizer(m_longPressGestureRecognizer);
        }
    }

    public Color? OriginalBackgroundColor { get; set; }

    public VisualElement? VisualElement { get; set; }

    private void OnLongPress(UILongPressGestureRecognizer e)
    {
        if (e.State != UIGestureRecognizerState.Began)
            return;

        if (Touch.GetLongPressCommand(Element)?.CanExecute(Touch.GetLongPressCommandParameter(Element)) ?? false)
            Touch.GetLongPressCommand(Element)?.Execute(Touch.GetLongPressCommandParameter(Element));
    }

    internal void OnTap()
    {
        if (Touch.GetCommand(Element)?.CanExecute(Touch.GetCommandParameter(Element)) ?? false)
            Touch.GetCommand(Element)?.Execute(Touch.GetCommandParameter(Element));
    }

    protected override partial void OnDetached()
    {
        if (Control is null)
            return;

        if (m_touchMode is Touch.TouchMode.Tap or Touch.TouchMode.Both)
        {
            if (Control.GestureRecognizers != null && m_tapGestureRecognizer is not null)
            {
                Control.RemoveGestureRecognizer(m_tapGestureRecognizer!);
            }
        }

        if (m_touchMode is Touch.TouchMode.LongPress or Touch.TouchMode.Both)
        {
            if (Control.GestureRecognizers != null && m_longPressGestureRecognizer is not null)
                Control.RemoveGestureRecognizer(m_longPressGestureRecognizer!);
        }

        m_tapGestureRecognizer?.Dispose();
    }

    internal void HandleTouch(UIGestureRecognizerState state, ref UIGestureRecognizerState currentState) =>
        HandleTouch(state, ref currentState, VisualElement, OriginalBackgroundColor);

    internal static void HandleTouch(UIGestureRecognizerState state, ref UIGestureRecognizerState currentState,
        VisualElement? visualElement, Color? originalColor)
    {
        if (visualElement == null || originalColor == null) return;
        HandleTouch(state, ref currentState, () => visualElement?.BackgroundColorTo(ColorToAnimateTo),
            () => visualElement?.BackgroundColorTo(originalColor));
    }

    internal static void HandleTouch(UIGestureRecognizerState state, ref UIGestureRecognizerState currentState,
        UIView? uiView, Color? originalColor)
    {
        if (uiView == null || originalColor == null) return;

#pragma warning disable CA1416
        HandleTouch(state, ref currentState, () => UIView.Animate(0.3, 0, 0, 0,
                UIViewAnimationOptions.AllowUserInteraction, () =>
                {
                    uiView.BackgroundColor = ColorToAnimateTo.ToPlatform();
                }, null),

            () => UIView.Animate(0.3, 0, 0, 0, UIViewAnimationOptions.AllowUserInteraction, () =>
            {
                uiView.BackgroundColor = originalColor.ToPlatform();
            }, null));
#pragma warning restore CA1416
    }

    internal static void HandleTouch(UIGestureRecognizerState state, ref UIGestureRecognizerState currentState,
        Action whenBegan, Action whenCancelledOrEnded)
    {
        switch (state)
        {
            case UIGestureRecognizerState.Began:
                whenBegan.Invoke();
                break;
            case UIGestureRecognizerState.Cancelled:
                whenCancelledOrEnded.Invoke();
                break;
            case UIGestureRecognizerState.Ended:
                if (currentState != UIGestureRecognizerState.Cancelled)
                {
                    whenCancelledOrEnded.Invoke();
                }

                break;
        }

        currentState = state;
    }
}