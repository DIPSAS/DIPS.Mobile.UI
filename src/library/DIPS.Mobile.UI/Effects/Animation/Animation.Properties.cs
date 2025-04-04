using DIPS.Mobile.UI.Effects.Animation.Effects;

namespace DIPS.Mobile.UI.Effects.Animation;

public partial class Animation
{
    public static readonly BindableProperty FadeInProperty = BindableProperty.CreateAttached("FadeIn",
        typeof(AnimationConfig),
        typeof(Animation),
        null,
        propertyChanged: (bindable, _, _) =>
        {
            if (bindable is VisualElement visualElement)
            {
                visualElement.Effects.Add(new FadeInEffect());
            }
        });

    public static readonly BindableProperty FadeOutProperty = BindableProperty.CreateAttached("FadeOut",
        typeof(AnimationConfig),
        typeof(Animation),
        null,
        propertyChanged: (bindable, _, _) =>
        {
            if (bindable is VisualElement visualElement)
            {
                visualElement.Effects.Add(new FadeOutEffect());
            }
        });
    
    public static AnimationConfig GetFadeIn(BindableObject view)
    {
        return (AnimationConfig)view.GetValue(FadeInProperty);
    }

    /// <summary>
    /// Fades in the view 
    /// </summary>
    /// <remarks>Will also fade in the view if <see cref="VisualElement.IsVisible"/> is set to true</remarks>
    public static void SetFadeIn(BindableObject view, AnimationConfig fadeIn)
    {
        view.SetValue(FadeInProperty, fadeIn);
    }

    public static AnimationConfig GetFadeOut(BindableObject view)
    {
        return (AnimationConfig)view.GetValue(FadeOutProperty);
    }

    /// <summary>
    /// Fades out the view
    /// </summary>
    public static void SetFadeOut(BindableObject view, AnimationConfig name)
    {
        view.SetValue(FadeOutProperty, name);
    }
}

public class AnimationConfig : BindableObject
{
    public uint Duration
    {
        get => (uint)GetValue(DurationProperty);
        set => SetValue(DurationProperty, value);
    }

    public Easing Easing
    {
        get => (Easing)GetValue(EasingProperty);
        set => SetValue(EasingProperty, value);
    }

    public static readonly BindableProperty DurationProperty = BindableProperty.Create(
        nameof(Duration),
        typeof(uint),
        typeof(AnimationConfig),
        250U);

    public static readonly BindableProperty EasingProperty = BindableProperty.Create(
        nameof(Easing),
        typeof(Easing),
        typeof(AnimationConfig),
        Easing.Default);
}