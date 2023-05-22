using System.Windows.Input;

namespace DIPS.Mobile.UI.Effects.AwesomeTouchEffect;

public partial class AwesomeTouchEffect
{
    public static readonly BindableProperty CommandProperty = BindableProperty.CreateAttached("Command",
        typeof(ICommand),
        typeof(AwesomeTouchEffect),
        null,
        propertyChanged: OnCommandChanged);
    
    public static readonly BindableProperty CommandParameterProperty = BindableProperty.CreateAttached("Command",
        typeof(object),
        typeof(AwesomeTouchEffect),
        null);
    
    public static readonly BindableProperty AccessibilityContentDescriptionProperty = BindableProperty.CreateAttached("AccessibilityContentDescription",
        typeof(string),
        typeof(AwesomeTouchEffect),
        null);
}