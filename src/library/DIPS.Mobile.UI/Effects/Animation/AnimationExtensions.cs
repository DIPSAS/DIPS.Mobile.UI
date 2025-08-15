using DIPS.Mobile.UI.Effects.Animation.Effects;

namespace DIPS.Mobile.UI.Effects.Animation;

public static class AnimationExtensions
{
    public static void Animate(this VisualElement element, AnimationType animationType, Action<AnimationConfig>? configurator = null)
    {
        if (element == null)
        {
            throw new ArgumentNullException(nameof(element));
        }

        var animationConfig = new AnimationConfig();
        configurator?.Invoke(animationConfig);
        switch (animationType)
        {
            case AnimationType.FadeIn:
                FadeInEffect.Animate(element, animationConfig);
                break;
            case AnimationType.ScaleIn:
                ScaleInEffect.Animate(element, animationConfig);
                break;
            default:
                throw new ArgumentOutOfRangeException(nameof(animationType), animationType, null);
        }
    }
}

public enum AnimationType
{
    FadeIn,
    ScaleIn
}