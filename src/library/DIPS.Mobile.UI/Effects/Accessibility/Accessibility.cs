using DIPS.Mobile.UI.Effects.Accessibility.Effects;

namespace DIPS.Mobile.UI.Effects.Accessibility;

public partial class Accessibility
{
    public static Mode GetMode(BindableObject view)
    {
        return (Mode)view.GetValue(ModeProperty);
    }

    /// <summary>
    /// Sets the mode for accessibility
    /// <remarks><see cref="AutomationProperties"/></remarks>
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