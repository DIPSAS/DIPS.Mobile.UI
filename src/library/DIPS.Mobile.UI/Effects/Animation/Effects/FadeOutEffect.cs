using System.ComponentModel;
using Microsoft.Maui.Controls.Platform;

namespace DIPS.Mobile.UI.Effects.Animation.Effects;

[Obsolete("Does not work yet")]
public class FadeOutEffect : RoutingEffect;

public class FadeOutPlatformEffect : PlatformEffect
{
#nullable disable
    private AnimationConfig m_config;
#nullable enable

    protected override void OnAttached()
    {
        return;
        m_config = Animation.GetFadeOut(Element);
        Element.PropertyChanged += ElementOnPropertyChanged;
    }

    private async void ElementOnPropertyChanged(object? sender, PropertyChangedEventArgs e)
    {
        return;
        if (e.PropertyName != VisualElement.IsVisibleProperty.PropertyName)
            return;

        if (sender is not VisualElement { IsVisible: false } element)
            return;

        Console.WriteLine("Set to false");

        Element.PropertyChanged -= ElementOnPropertyChanged;

        element.IsVisible = true;
        await element.FadeTo(0, 1000, m_config.Easing);
        element.IsVisible = false;
            
        Element.PropertyChanged += ElementOnPropertyChanged;
    }

    protected override void OnDetached()
    {
        return;
        Element.PropertyChanged -= ElementOnPropertyChanged;
    }
}