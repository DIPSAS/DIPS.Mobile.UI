using SkiaSharp.Extended.UI.Controls;

namespace DIPS.Mobile.UI.Components.Animating;

public partial class AnimatingView
{
    public static readonly BindableProperty AnimationProperty = BindableProperty.Create(
        nameof(Animation),
        typeof(SKLottieImageSource),
        typeof(AnimatingView));

    public SKLottieImageSource Animation
    {
        get => (SKLottieImageSource)GetValue(AnimationProperty);
        set => SetValue(AnimationProperty, value);
    }

    public static readonly BindableProperty IsPlayingProperty = BindableProperty.Create(
        nameof(IsPlaying),
        typeof(bool),
        typeof(AnimatingView),
        propertyChanged: (bindable, _, _) => ((AnimatingView)bindable).IsPlayingChanged());

    public bool IsPlaying
    {
        get => (bool)GetValue(IsPlayingProperty);
        set => SetValue(IsPlayingProperty, value);
    }
}