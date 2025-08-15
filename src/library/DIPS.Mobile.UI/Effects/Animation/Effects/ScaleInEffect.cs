using System.ComponentModel;
using Microsoft.Maui.Controls.Platform;

namespace DIPS.Mobile.UI.Effects.Animation.Effects;

public class ScaleInEffect : RoutingEffect
{
    internal static void Animate(VisualElement visualElement, AnimationConfig config)
    {
        visualElement.Scale = config.StartingValue;
        visualElement.ScaleTo(1, config.Duration, easing: config.Easing);
    }
}

internal class ScaleInPlatformEffect : PlatformEffect
{
#nullable disable
    private AnimationConfig m_config;
#nullable enable

    protected override void OnAttached()
    {
        Element.PropertyChanged += ElementOnPropertyChanged;

        m_config = Animation.GetScaleIn(Element);
        if (Element is not VisualElement visualElement)
            return;

        visualElement.Scale = m_config.StartingValue;
        
        if (visualElement.IsVisible)
        {
            visualElement.ScaleTo(1, m_config.Duration, easing: m_config.Easing);
        }
    }

    private void ElementOnPropertyChanged(object? sender, PropertyChangedEventArgs e)
    {
        if (e.PropertyName != VisualElement.IsVisibleProperty.PropertyName)
            return;

        if (Element is VisualElement { IsVisible: true } visualElement)
        {
            ScaleInEffect.Animate(visualElement, m_config);
        }
    }

    protected override void OnDetached()
    {
        Element.PropertyChanged -= ElementOnPropertyChanged;
    }
}