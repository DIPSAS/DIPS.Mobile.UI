using System.Windows.Input;

namespace DIPS.Mobile.UI.Effects.DUITouchEffect;

public partial class DUITouchEffect
{
    public static readonly BindableProperty CommandProperty = BindableProperty.CreateAttached("Command",
        typeof(ICommand),
        typeof(DUITouchEffect),
        null,
        propertyChanged: OnCommandChanged);
    
    public static readonly BindableProperty CommandParameterProperty = BindableProperty.CreateAttached("Command",
        typeof(object),
        typeof(DUITouchEffect),
        null);
    
    public static readonly BindableProperty AccessibilityContentDescriptionProperty = BindableProperty.CreateAttached("AccessibilityContentDescription",
        typeof(string),
        typeof(DUITouchEffect),
        null);
}