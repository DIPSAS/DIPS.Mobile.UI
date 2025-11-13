using Microsoft.Maui.Controls.Platform;
using Microsoft.Maui.Platform;
using UIKit;

namespace DIPS.Mobile.UI.Effects.Accessibility.Effects;

internal class TraitPlatformEffect : PlatformEffect
{
    protected override void OnAttached()
    {
        var trait = Accessibility.GetTrait(Element);

        var traits = Control.AccessibilityTraits;

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

        Control.AccessibilityTraits = traits;
    }

    protected override void OnDetached()
    {
        
    }
}
