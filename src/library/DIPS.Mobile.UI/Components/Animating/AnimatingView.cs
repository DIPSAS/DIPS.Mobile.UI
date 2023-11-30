using SkiaSharp.Extended.UI.Controls;

namespace DIPS.Mobile.UI.Components.Animating;

public partial class AnimatingView : SKLottieView
{
    public AnimatingView()
    {
        this.SetBinding(SourceProperty, new Binding(nameof(Animation), source: this));
        this.SetBinding(IsAnimationEnabledProperty, new Binding(nameof(Animation), source: this));
    }

    private void IsPlayingChanged()
    {
        if (IsPlaying)
        {
            
        }
    }
}