using System.ComponentModel;
using Microsoft.Maui.Controls.Platform;

namespace DIPS.Mobile.UI.Effects.Animation.Effects;

internal class FadeInEffect : RoutingEffect;

internal class FadeInPlatformEffect : PlatformEffect
{
#nullable disable
    private AnimationConfig m_config;
#nullable enable

    protected override void OnAttached()
    {
        Element.PropertyChanged += ElementOnPropertyChanged;

        m_config = Animation.GetFadeIn(Element);
        if (Element is not VisualElement visualElement)
            return;

        visualElement.Opacity = 0;
        
        if (visualElement.IsVisible)
        {
            visualElement.FadeTo(1, m_config.Duration, easing: m_config.Easing);
        }
    }
    
    private void ElementOnPropertyChanged(object? sender, PropertyChangedEventArgs e)
    {
        if (e.PropertyName != VisualElement.IsVisibleProperty.PropertyName)
            return;

        if (Element is VisualElement { IsVisible: true } visualElement)
        {
            visualElement.Opacity = 0;
            visualElement.FadeTo(1, m_config.Duration, easing: m_config.Easing);
        }
    }
    
    protected override void OnDetached()
    {
        Element.PropertyChanged -= ElementOnPropertyChanged;
    }
}