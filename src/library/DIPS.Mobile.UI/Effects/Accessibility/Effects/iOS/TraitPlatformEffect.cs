using Microsoft.Maui.Controls.Platform;
using Microsoft.Maui.Platform;
using UIKit;
using View = Microsoft.Maui.Controls.View;

namespace DIPS.Mobile.UI.Effects.Accessibility.Effects;

internal class TraitPlatformEffect : PlatformEffect
{
    protected override void OnAttached()
    {
        ApplyTraits();
        MainThread.BeginInvokeOnMainThread(ApplyTraits);

        if (Element is View view)
        {
            view.PropertyChanged += OnElementPropertyChanged;
        }
    }

    private void OnElementPropertyChanged(object? sender, System.ComponentModel.PropertyChangedEventArgs e)
    {
        if (e.PropertyName == Accessibility.TraitProperty.PropertyName ||
            e.PropertyName == SemanticProperties.DescriptionProperty.PropertyName)
        {
            MainThread.BeginInvokeOnMainThread(ApplyTraits);
        }
    }

    private void ApplyTraits()
    {
        if (Control is null)
            return;

        var trait = Accessibility.GetTrait(Element);

        Control.AccessibilityTraits = ApplyTraits(Control.AccessibilityTraits, trait);
    }

    internal static UIAccessibilityTrait ApplyTraits(UIAccessibilityTrait traits, Trait trait)
    {
        traits &= ~UIAccessibilityTrait.Button;

        if (trait.HasFlag(Trait.Button))
        {
            traits |= UIAccessibilityTrait.Button;
        }

        if (trait.HasFlag(Trait.Selected))
        {
            traits |= UIAccessibilityTrait.Selected;
        }
        else if (trait.HasFlag(Trait.NotSelected))
        {
            // Remove selected trait if it exists
            traits &= ~UIAccessibilityTrait.Selected;
        }

        return traits;
    }

    protected override void OnDetached()
    {
        if (Element is View view)
        {
            view.PropertyChanged -= OnElementPropertyChanged;
        }
    }
}
