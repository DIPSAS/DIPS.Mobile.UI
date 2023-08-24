using SkiaSharp.Extended.UI.Controls;
using SkiaSharp.Extended.UI.Controls.Converters;

namespace DIPS.Mobile.UI.Resources.Animations;

public class Animations
{
    public static SKLottieImageSource GetAnimation(AnimationName animationName)
    {
        if (!AnimationResources.Animations.TryGetValue(animationName.ToString(), out var value))
        {
            return new SKLottieImageSource();
        }

        if (new SKLottieImageSourceConverter().ConvertFromInvariantString(value) is SKLottieImageSource
            skLottieImageSource)
        {
            return skLottieImageSource;
        }

        return new SKLottieImageSource();
    }
}