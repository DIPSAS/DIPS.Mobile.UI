using Android.Views.Accessibility;
using Microsoft.Maui.Controls.Platform;
using Microsoft.Maui.Platform;
using View = Microsoft.Maui.Controls.View;

namespace DIPS.Mobile.UI.Effects.Accessibility.Effects;

internal class TraitPlatformEffect : PlatformEffect
{
    private TraitAccessibilityDelegate? m_accessibilityDelegate;

    protected override void OnAttached()
    {
        ApplyTrait();

        if (Element is View view)
        {
            view.PropertyChanged += OnElementPropertyChanged;
        }
    }

    private void OnElementPropertyChanged(object? sender, System.ComponentModel.PropertyChangedEventArgs e)
    {
        if (e.PropertyName == Accessibility.TraitProperty.PropertyName)
        {
            ApplyTrait();
        }
    }

    private void ApplyTrait()
    {
        m_accessibilityDelegate = TraitAccessibilityDelegate.ApplyTrait(Control, Accessibility.GetTrait(Element));
    }

    protected override void OnDetached()
    {
        if (Element is View view)
        {
            view.PropertyChanged -= OnElementPropertyChanged;
        }
    }
}

internal class TraitAccessibilityDelegate : Android.Views.View.AccessibilityDelegate
{
    private Trait m_trait;
    private readonly Android.Views.View.AccessibilityDelegate? m_existingDelegate;

    public TraitAccessibilityDelegate(Trait trait, Android.Views.View.AccessibilityDelegate? existingDelegate)
    {
        m_trait = trait;
        m_existingDelegate = existingDelegate;
    }

    internal static TraitAccessibilityDelegate ApplyTrait(Android.Views.View view, Trait trait)
    {
        var existingDelegate = view.GetAccessibilityDelegate();
        if (existingDelegate is TraitAccessibilityDelegate traitAccessibilityDelegate)
        {
            traitAccessibilityDelegate.m_trait = trait;
            return traitAccessibilityDelegate;
        }

        var newDelegate = new TraitAccessibilityDelegate(trait, existingDelegate);
        view.SetAccessibilityDelegate(newDelegate);
        return newDelegate;
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
