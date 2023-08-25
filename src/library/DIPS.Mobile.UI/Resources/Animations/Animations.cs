using SkiaSharp.Extended.UI.Controls;
using SkiaSharp.Extended.UI.Controls.Converters;

namespace DIPS.Mobile.UI.Resources.Animations;

public class Animations
{
    private static string RawAnimationsLocationAndroid = "Resources/Animations/";
    private static string RawAnimationsLocationiOS = "Animations\\";

    public static SKLottieImageSource GetAnimation(AnimationName animationName)
    {
        if (!AnimationResources.Animations.TryGetValue(animationName.ToString(), out var value))
        {
            return new SKLottieImageSource();
        }

#if __ANDROID__
        var valueToLookFor = RawAnimationsLocationAndroid + value;
#elif __IOS__
        var valueToLookFor = RawAnimationsLocationiOS + value;
#endif
        if (new SKLottieImageSourceConverter().ConvertFromInvariantString(valueToLookFor) is SKLottieImageSource
            skLottieImageSource)
        {
            return skLottieImageSource;
        }

        return new SKLottieImageSource();
    }
}