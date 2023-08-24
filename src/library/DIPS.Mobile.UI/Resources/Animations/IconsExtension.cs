using SkiaSharp.Extended.UI.Controls;

namespace DIPS.Mobile.UI.Resources.Animations;

[ContentProperty(nameof(IconName))]
public class AnimationsExtension : IMarkupExtension<SKLottieImageSource>
{
    public AnimationName AnimationName { get; set; }

    public SKLottieImageSource ProvideValue(IServiceProvider serviceProvider) => Resources.Animations.Animations.GetAnimation(AnimationName);

    object IMarkupExtension.ProvideValue(IServiceProvider serviceProvider)
    {
        return ProvideValue(serviceProvider);
    }
}