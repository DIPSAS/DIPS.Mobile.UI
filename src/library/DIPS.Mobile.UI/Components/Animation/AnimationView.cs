using DIPS.Mobile.UI.Resources.Animations;
using SkiaSharp.Extended.UI.Controls;

namespace DIPS.Mobile.UI.Components.Animation;

public class AnimationView : SKLottieView
{
    public static readonly BindableProperty AnimationNameProperty = BindableProperty.Create(
        nameof(AnimationName),
        typeof(AnimationName),
        typeof(AnimationView), propertyChanged: (bindable, _, _) => ((AnimationView)bindable).OnAnimationNameChanged());

    private void OnAnimationNameChanged()
    {
        Source = Animations.GetAnimation(AnimationName);
    }

    /// <summary>
    /// To use the <see cref="AnimationName"/> as the source of the animation from the design system.
    /// </summary>
    public AnimationName AnimationName
    {
        get => (AnimationName)GetValue(AnimationNameProperty);
        set => SetValue(AnimationNameProperty, value);
    }
}