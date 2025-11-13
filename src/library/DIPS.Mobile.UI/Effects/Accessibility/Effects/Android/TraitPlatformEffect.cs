using Android.Views.Accessibility;
using Microsoft.Maui.Controls.Platform;
using Microsoft.Maui.Platform;

namespace DIPS.Mobile.UI.Effects.Accessibility.Effects;

internal class TraitPlatformEffect : PlatformEffect
{
    private TraitAccessibilityDelegate? m_accessibilityDelegate;

    protected override void OnAttached()
    {
        var trait = Accessibility.GetTrait(Element);

        m_accessibilityDelegate = new TraitAccessibilityDelegate(trait, Control.GetAccessibilityDelegate());
        Control.SetAccessibilityDelegate(m_accessibilityDelegate);
    }

    protected override void OnDetached()
    {
    }
}

internal class TraitAccessibilityDelegate : Android.Views.View.AccessibilityDelegate
{
    private readonly Trait m_trait;
    private readonly Android.Views.View.AccessibilityDelegate? m_existingDelegate;

    public TraitAccessibilityDelegate(Trait trait, Android.Views.View.AccessibilityDelegate? existingDelegate)
    {
        m_trait = trait;
        m_existingDelegate = existingDelegate;
    }

    public override void OnInitializeAccessibilityNodeInfo(Android.Views.View host, AccessibilityNodeInfo info)
    {
        base.OnInitializeAccessibilityNodeInfo(host, info);
        
        m_existingDelegate?.OnInitializeAccessibilityNodeInfo(host, info);

        if (m_trait.HasFlag(Trait.Button))
        {
            info.ClassName = "android.widget.Button";
        }

        if (m_trait.HasFlag(Trait.Selected))
        {
            info.Checked = true;
            info.Checkable = true;
        }
        else if (m_trait.HasFlag(Trait.NotSelected))
        {
            info.Checked = false;
            info.Checkable = true;
        }
    }
}
