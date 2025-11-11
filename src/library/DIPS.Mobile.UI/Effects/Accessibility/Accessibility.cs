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
}