using SkiaSharp.Extended.UI.Controls;
using SkiaSharp.Extended.UI.Controls.Converters;

namespace DIPS.Mobile.UI.Resources.Animations;

public class Animations
{
    private static string RawAnimationsLocationAndroid = "Resources/Animations/";
    private static string RawAnimationsLocationiOS = "Animations\\";

    public static SKLottieImageSource? GetAnimation(AnimationName animationName)
    {
        if (!AnimationResources.Animations.TryGetValue(animationName.ToString(), out var value))
        {
            return null;
        }

        var valueToLookFor = RawAnimationsLocationAndroid + value;
#if __IOS__
         valueToLookFor = RawAnimationsLocationiOS + value;
#endif
        if (new SKLottieImageSourceConverter().ConvertFromInvariantString(valueToLookFor) is SKLottieImageSource
            skLottieImageSource)
        {
            return skLottieImageSource;
        }

        return null;
    }
}