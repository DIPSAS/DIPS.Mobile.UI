using DIPS.Mobile.UI.Effects.Accessibility.Effects;

namespace DIPS.Mobile.UI.Effects.Accessibility;

/// <summary>
/// Provides accessibility features for views. <br/>
/// For more information, see <see href="https://github.com/DIPSAS/DIPS.Mobile.UI/wiki/Accessibility">Accessibility in DIPS.Mobile.UI</see>.
/// </summary>
public partial class Accessibility
{
    public static Mode GetMode(BindableObject view)
    {
        return (Mode)view.GetValue(ModeProperty);
    }

    /// <summary>
    /// Attached property that can simplify common accessibility scenarios for screen readers. <br/>
    /// For more information, see <see href="https://github.com/DIPSAS/DIPS.Mobile.UI/wiki/Accessibility#dipsmobileui-accessibility-mode">DIPS.Mobile.UI Accessibility Mode</see>.
    /// </summary>
    public static void SetMode(BindableObject view, Mode mode)
    {
        view.SetValue(ModeProperty, mode);
    }
    
    public static Trait GetTrait(BindableObject view)
    {
        return (Trait)view.GetValue(TraitProperty);
    }

    /// <summary>
    /// Attached property to set accessibility traits on a view.
    /// Traits can be combined using bitwise OR (|) operator.
    /// </summary>
    /// <example>
    /// <code>
    /// // Single trait
    /// Accessibility.SetTrait(myView, Trait.Button);
    /// 
    /// // Multiple traits
    /// Accessibility.SetTrait(myView, Trait.Button | Trait.Selected);
    /// </code>
    /// </example>
    public static void SetTrait(BindableObject view, Trait trait)
    {
        view.SetValue(TraitProperty, trait);
    }
    
    private static void OnModeChanged(BindableObject bindable, object oldValue, object newValue)
    {
        if (bindable is not View view || newValue is not Mode mode)
            return;

        switch (mode)
        {
            case Mode.GroupChildren:
                view.Behaviors.Add(new GroupEffect());
                break;
            case Mode.None:
                break;
            default:
                throw new ArgumentOutOfRangeException();
        }
    }
    
    private static void OnTraitChanged(BindableObject bindable, object oldValue, object newValue)
    {
        if (bindable is not View view || newValue is not Trait trait)
            return;

        if (trait == Trait.None)
            return;

        view.Effects.Add(new TraitEffect());
    }
}
